using Telegram.Bot.Types;

namespace  Application.Interfaces;

public interface ICommand
{
    //Dictionary<string, Telegram.Bot.Types> Arguments { get; }
    Task HandleCommand(Update update);
}

