using Application.Interfaces;
using Infrastructure.Service;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Command;

public class DeleteMember : IMember
{
    public string CommandName => "DeleteUser";

    public ChatMemberStatus Status => ChatMemberStatus.Kicked;

    private readonly AppUserService _appUserService;

    public DeleteMember(AppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    public async Task HandleCommand(Update update)
    {
        User? user = update.MyChatMember.From;
        await _appUserService.DeleteAppUser(user.Id);
    }
}
