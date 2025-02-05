using Application.Interfaces;
using Application.Service;
using Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.Command;

public class MessageOperation : IMessage
{
    private readonly ITelegramBotClient _client;
    private readonly ICallbackFactory _callbackFactory;
    public MessageOperation(ITelegramBotClient client, ICallbackFactory callbackFactory)
    {
        _client = client;
        _callbackFactory = callbackFactory;
    }

    public async Task Send(string text, long id)
    {
        await _client.SendMessage(id, text);
    }
    public async Task EditMessage(ChatId chat_id, int message_id, string text)
    {
        await _client.EditMessageText(chatId: chat_id, message_id, text);

    }
    public async Task SendMenu(string text, Subscriptions vacancies)
    {
        var inlineButton1 = new InlineKeyboardButton("Да");
        var inlineButton2 = new InlineKeyboardButton("Нет");
        List<InlineKeyboardButton> rowInline = new List<InlineKeyboardButton>();
        rowInline.Add(inlineButton1);
        rowInline.Add(inlineButton2);
        inlineButton1.CallbackData = _callbackFactory.CreateAddMethod(vacancies.CallBackId, "yes");
        inlineButton2.CallbackData = _callbackFactory.CreateAddMethod(vacancies.CallBackId, "no");
        await _client.SendMessage(vacancies.AppUser.Id, text, replyMarkup: new InlineKeyboardMarkup(rowInline));

    }
}
