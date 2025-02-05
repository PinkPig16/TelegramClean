using Telegram.Bot.Types;

namespace Application.Interfaces;
public interface IMessageCommand : ICommand 
{
    string CommandName { get; }
}

