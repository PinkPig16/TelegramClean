using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Application.Interfaces;
using Infrastructure.Logging;
using Infrastructure.Service;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Command;

public class AddMember : IMember
{
    public ChatMemberStatus Status => ChatMemberStatus.Member;

    private readonly AppUserService _appUserService;
    private readonly ILogger<AddMember> _logger;
    public AddMember(AppUserService appUserService, ILogger<AddMember> logger)
    {
        _logger = logger;
        _appUserService = appUserService;
    }

    public async Task HandleCommand(Update update)
    {
        User? user = update.MyChatMember.From;
        if (user == null)
        {
            _logger.ChatMemberIsNull(update.Id);
            return;
        }
        await _appUserService.AddAppUser(user);
    }
}

