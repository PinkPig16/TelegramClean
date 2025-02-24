using Application.Interfaces;
using Infrastructure.Logging;
using Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Command;

public class DeleteMember : IMember
{
    public string CommandName => "DeleteUser";

    public ChatMemberStatus Status => ChatMemberStatus.Kicked;

    private readonly AppUserService _appUserService;
    private readonly ILogger<DeleteMember> _logger;

    public DeleteMember(AppUserService appUserService, ILogger<DeleteMember> logger)
    {
        _appUserService = appUserService;
        _logger = logger;
    }

    public async Task HandleCommand(Update update)
    {
        User? user = update.MyChatMember.From;
        if (user == null)
        {
            _logger.ChatMemberIsNull(update.Id);
            return;
        }
        await _appUserService.DeleteAppUser(user.Id);
    }
}
