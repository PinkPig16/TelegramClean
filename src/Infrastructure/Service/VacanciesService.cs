using Application.Interfaces;
using Domain.Entities;
using HtmlAgilityPack;
using Telegram.Bot.Types;

namespace Infrastructure.Service;

public class VacanciesService : IVacanciesService
{
    private readonly HttpClient _client;
    public VacanciesService(HttpClient client) 
    {
        _client = client;
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
                
                var jobLinkList = document.DocumentNode.SelectSingleNode("//div[@id='pjax-jobs-list']").ChildNodes.Where(x => x.OriginalName == "a").ToList();
                foreach(var jobLink in jobLinkList)
                {
                    vacanciesUrls.Add(jobLink.Attributes.First().Value);
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
