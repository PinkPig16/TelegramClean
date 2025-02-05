using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Application.Interfaces;
using Infrastructure.Service;

namespace Infrastructure.Command;

public class AddMember : IMember
{
    public ChatMemberStatus Status => ChatMemberStatus.Member;

    private readonly AppUserService _appUserService;

    public AddMember(AppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    public async Task HandleCommand(Update update)
    {
        User? user = update.MyChatMember.From;
        await _appUserService.AddAppUser(user);
    }
}

