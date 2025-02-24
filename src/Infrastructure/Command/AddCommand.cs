using System.Text.RegularExpressions;
using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using Infrastructure.Builder;
using Infrastructure.Logging;
using Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;


namespace Infrastructure.Command;

public class AddCommand : IMessageCommand
{
    private readonly ParseMessage _parseMessage;
    private readonly AppUserService _AppUserService;
    private readonly IParse _parse;
    private readonly ICityRepository _cityRepository;
    private readonly SubscriptionsBuilder _subscriptionsBuilder;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly ILogger _logger;

    public AddCommand(ParseMessage parseMessage, AppUserService appUserService,
        ICityRepository cityRepository, IParse parse, IConfiguration configuration,
        SubscriptionsBuilder subscriptionBuilder, ISubscriptionsRepository subscriptionsRepository,
        ILogger<AddCommand> logger)
    {
        _parseMessage = parseMessage;
        _AppUserService = appUserService;
        _cityRepository = cityRepository;
        _parse = parse;
        _subscriptionsBuilder = subscriptionBuilder;
        _subscriptionsRepository = subscriptionsRepository;
        _logger = logger;
    }

    public string CommandName => "/add";


    public async Task HandleCommand(Update update)
    {
        if (update.Message == null)
        {
            _logger.MessageIsNull(update.Id);
            return;
        }
        User user = update.Message.From;
        if (user == null)
        {
            _logger.UserIsNull(update.Id);
            return;
        }
        string messageText = update.Message.Text.Replace(CommandName, "").Trim();

        var words = _parseMessage.SubscriptionsParse(update);

        var appUserTask  = _AppUserService.HandleUserAsync(user);
        var cityTask = _cityRepository.GetAsyncByNameArray(words);

        await Task.WhenAll(appUserTask, cityTask);
        var city = await cityTask; 
        if (city != null)
        {
            messageText = Regex.Replace(messageText, city.Name, "", RegexOptions.IgnoreCase);
        }
        var appUser = await appUserTask;
        Subscriptions subscriptions = _subscriptionsBuilder
            .setAppUser(appUser)
            .setName(messageText)
            .setCity(city)
            .setCallBack(update.Message.MessageId)
            .setUrl(messageText)
            .Build();

        var subscriptionAddTask = _subscriptionsRepository.Add(subscriptions);
        var jobCountTask = _parse.JobCount(subscriptions);
        var updateAppUserTask =  _AppUserService.UpdateAppUserAsync(appUser);

        await Task.WhenAll(subscriptionAddTask, jobCountTask, updateAppUserTask);
    }
}
