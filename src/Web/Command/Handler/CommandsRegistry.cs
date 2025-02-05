using Application.Interfaces;
using Telegram.Bot.Types.Enums;

namespace Web.Command.Handler;
public class CommandsRegistry
{
    private readonly Dictionary<string, IMessageCommand> _commandMessage = new();
    private readonly Dictionary<ChatMemberStatus, IMember> _commandMember = new();


    public CommandsRegistry(IEnumerable<IMessageCommand> commandMessage, IEnumerable<IMember> commandMember)
    {
        //TODO заменить на рефлексию
        foreach (var handler in commandMessage)
        {
            _commandMessage[handler.CommandName] = handler;
        }
        foreach (var handler in commandMember)
        {
            _commandMember[handler.Status] = handler;
        }
    }

    public IMessageCommand? GetCommandHandler(string commandName)
    {
        _commandMessage.TryGetValue(commandName, out var handler);
        return handler;
    }
    public IMember? GetCommandHandler(ChatMemberStatus commandName)
    {
        _commandMember.TryGetValue(commandName, out var handler);
        return handler;
    }
}
