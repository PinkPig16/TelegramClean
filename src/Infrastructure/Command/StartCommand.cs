using Application.Interfaces;
using Infrastructure.Service;
using Telegram.Bot.Types;


namespace Infrastructure.Command;

public class StartCommand : IMessageCommand
{
    private readonly IMessage _message;
    private readonly AppUserService _appUserService;

    public StartCommand(IMessage message, AppUserService appUserService)
    {
        _message = message;
        _appUserService = appUserService;

    }


    public string CommandName => "/start";

    public async Task HandleCommand(Update update)
    {
        User? user = update.Message?.From;
        if(user == null) return;
        await _appUserService.HandleUserAsync(user);

        var messageString = "Для подписки на рассылку вакансий используйте /add. \n Например: /add junior .net киев   /add C# remote";
        await _message.Send(messageString, user.Id);
    }
}
