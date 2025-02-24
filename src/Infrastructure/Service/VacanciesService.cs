using System.Net;
using Application.Interfaces;
using Domain.Entities;
using HtmlAgilityPack;
using Infrastructure.Builder;
using Telegram.Bot.Types;

namespace Infrastructure.Service;

public class VacanciesService : IVacanciesService
{
    private readonly HttpClient _client;
    private readonly VacanciesBuilder _vacanciesBuilder;

    public VacanciesService(HttpClient client, VacanciesBuilder vacanciesBuilder)
    {
        _client = client;
        _vacanciesBuilder = vacanciesBuilder;
    }   
    public async Task<List<Vacancies>> GetVacancies(IEnumerable<Subscriptions> subscriptions)
    {
        foreach (var subscription in subscriptions)
        {
            var index = 1;
            int jobCountDocument = 1;
            var vacanciesUrls = new List<string>();
            while (jobCountDocument > 0)
            {   
                var document = await GetPage(subscription, index);

                if (!Int32.TryParse(document.DocumentNode.SelectSingleNode("//div[@class='mt-8 text-default-7']").InnerText.Trim().Split(' ')[0], 
                    out jobCountDocument))
                {
                    jobCountDocument = 0;
                    break;
                }
                var jobDoc = document.DocumentNode.SelectSingleNode("//div[@id='pjax-jobs-list']");
                var jobLists = jobDoc?.ChildNodes
                        .Where(cn => cn.Attributes
                        .Any(x => x.OriginalName == "tabindex")).ToList();

                foreach(var job in jobLists)
                {
                    var jobTitle = job.Descendants("h2")
                        .FirstOrDefault()?
                        .Descendants("a")
                        .FirstOrDefault()?
                        .InnerText.Trim();
                    var jobSalary = job.Descendants("span")
                        .FirstOrDefault(c => c.Attributes["class"]?.Value == "strong-600")?
                        .InnerText.Trim();
                    var jobSalaryFormatted = WebUtility.HtmlDecode(jobSalary);

                    var jobCompany = job.Descendants()
                        .FirstOrDefault(c => c.Attributes["class"]?.Value == "mr-xs")?
                        .Descendants("span")
                        .FirstOrDefault()?
                        .InnerText.Trim();
                    
                    var jobDescription = job.Descendants("p")
                        .FirstOrDefault(p => p.Attributes["class"]?.Value == "ellipsis ellipsis-line ellipsis-line-3 text-default-7 mb-0")?
                        .InnerText.Trim();
                    
                    var vacancies = _vacanciesBuilder
                        .SetDescription(jobDescription)
                        .SetSalary(jobSalaryFormatted)
                        .Build();
                    jobCountDocument--;
                }
                index++;
            }
        }
        return new List<Vacancies>();
    }
    public async Task<HtmlDocument> GetPage(Subscriptions subscription, int index)
    {
        var response = await _client.GetAsync($"{subscription.Url}/?page={index}");

        if (!response.IsSuccessStatusCode) throw new Exception($"Failed to load page {index} from {subscription.Url}. Status code: {response.StatusCode}");

        string html = await response.Content.ReadAsStringAsync();
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(html);
        return document;
    }   
}
