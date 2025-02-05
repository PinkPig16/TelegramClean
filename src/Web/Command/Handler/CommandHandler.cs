using Application.Interfaces;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Web.Command.Handler;


public class CommandHadler 
{
    private readonly CommandsRegistry _commandsRegistry;

    public CommandHadler(CommandsRegistry commandsRegistry)
    {
        _commandsRegistry = commandsRegistry;
    }

    public ICommand? GetCommand(Update upd)
    {
        if (upd.MyChatMember != null 
            && upd.Type == UpdateType.MyChatMember)
        {
            return _commandsRegistry.GetCommandHandler(upd.MyChatMember.NewChatMember.Status);
        }
        else if (upd.Message != null 
            && upd.Type == UpdateType.Message 
            &&  upd.Message.Entities.Any(x => x.Type == MessageEntityType.BotCommand))
        {
            var command = upd.Message.Entities.Where(x => x.Type == MessageEntityType.BotCommand).First();
            return _commandsRegistry.GetCommandHandler(upd.Message.Text.Substring(command.Offset,command.Length));
        }
        else if (upd.CallbackQuery != null
            && upd.Type == UpdateType.CallbackQuery)
        {
            return _commandsRegistry.GetCommandHandler("callBack");
        }
        return null;
    }
}
