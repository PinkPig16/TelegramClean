using Application.Interfaces;
using Application.IRepository;
using Application.Service;
using Domain.Entities;
using Infrastructure.DTO;
using System.Text.Json;
using Telegram.Bot.Types;


namespace Infrastructure.Command;

public class CallbackCommand : IMessageCommand
{
    public readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IMessage _message;
    private readonly CallbackBase _callback;

    public CallbackCommand(ISubscriptionsRepository subscriptionsRepository, 
        IMessage message, CallbackBase callback)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _message = message;
        _callback = callback;
    }

    public string CommandName => "callBack";

    public async Task HandleCommand(Update update)
    {
        //TODO: throw exp
        var callbackDTO = JsonSerializer.Deserialize<CallbackDTO>(update.CallbackQuery.Data);
        var subscriptions = await _subscriptionsRepository.GetSubscriptionsByCallBackId(update.CallbackQuery.From.Id, callbackDTO.MessageId);
        if (subscriptions == null) return;

        if (callbackDTO.Action == "yes")
        {
            subscriptions.sub = true;
            var editMessageTask = _message.EditMessage(update.CallbackQuery.Message.Chat, update.CallbackQuery.Message.MessageId, $"Добавлен \n {subscriptions.Url}");
            var subscriptionUpdateTask = _subscriptionsRepository.UpdateAsync(subscriptions);
            await Task.WhenAll(editMessageTask, subscriptionUpdateTask);
        }
        else if (callbackDTO.Action == "no")
        {
            var editMessageTask = _message.EditMessage(update.CallbackQuery.Message.Chat, update.CallbackQuery.Message.MessageId, "Ок, не добавлен");
            var subscriptionUpdateTask = _subscriptionsRepository.DeleteAsync(subscriptions);
            await Task.WhenAll(editMessageTask, subscriptionUpdateTask);
        }
    }
}
