using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Interfaces;

public interface IMember : ICommand
{
    ChatMemberStatus Status { get; }
}

