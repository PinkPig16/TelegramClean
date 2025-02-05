using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Infrastructure.Parse;

public class ParseWebSite : IParse
{
    private readonly HttpClient _client;
    private readonly HtmlWeb _htmlWeb;
    private readonly IConfiguration _config;
    private readonly IMessage _message;
    private readonly ICityRepository _cityRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IVacanciesService _vacanciesService;   

    public ParseWebSite(HttpClient httpClient, 
        HtmlWeb htmlWeb, IConfiguration configuration, 
        IMessage message,IVacanciesService vacanciesService, 
        ICityRepository cityRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _client = httpClient;
        _htmlWeb = htmlWeb;
        _config = configuration;
        _message = message;
        _cityRepository = cityRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _vacanciesService = vacanciesService;   
    }

    public async Task TestParse()
    {
        string url = _config["UrlWork"] + Uri.EscapeDataString("jobs-c#+developer");
        HttpResponseMessage response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string html = await response.Content.ReadAsStringAsync();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var jobTitles = document.DocumentNode.SelectSingleNode("//div[@id='pjax-jobs-list']");
            var jobList = jobTitles.SelectNodes("./div[@tabindex]");
            foreach (var job in jobList)
            {
                var jobName = job.SelectSingleNode(".//h2[@class='my-0']").InnerText;
                var location = job.SelectSingleNode(".//span[@class='']").InnerText;
                var company = job.SelectSingleNode(".//span[@class='strong-600']").InnerText;
                var text = job.SelectSingleNode(".//p[@class='ellipsis ellipsis-line ellipsis-line-3 text-default-7 mb-0']").InnerText.Trim();
                await _message.Send($"{jobName}\n {location} {company}\n {text}", 236322076L);
            }

        }
    }

    public async Task GetVacancies()
    {
        var subscriptions = await _subscriptionsRepository.GetSubscriptionsBySub();
        var vacancies = await _vacanciesService.GetVacancies(subscriptions);
    }

    public async Task JobCount(Subscriptions subscriptions)
    {
        HttpResponseMessage response = await _client.GetAsync(subscriptions.Url);
        if (response.IsSuccessStatusCode)
        {
            string html = await response.Content.ReadAsStringAsync();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var jobCountDocument = document.DocumentNode.SelectSingleNode("//div[@class='mt-8 text-default-7']");
            if (jobCountDocument != null)
            {
                string jobCount = jobCountDocument.InnerText.Trim().Split(' ')[0];
                await _message.SendMenu($"Найдено {jobCount} вакансий с запросом {subscriptions.Name}. Добавить подписку ?", subscriptions);
            }
        }
    }
}
