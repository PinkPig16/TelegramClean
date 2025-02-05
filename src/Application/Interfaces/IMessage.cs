using Domain.Entities;
using Telegram.Bot.Types;


namespace Application.Interfaces;
public interface IMessage
{
    Task Send(string text, long id);
    Task SendMenu(string text, Subscriptions vacancies);
    Task EditMessage(ChatId chat_id, int message_id, string text);
}

