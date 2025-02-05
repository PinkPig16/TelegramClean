using Microsoft.Extensions.Options;
using Telegram.Bot;
using Domain.Model;

namespace Application.Service;

public class TelegramBotClientService
{
    private readonly TelegramConfigModel _config;
    private readonly ITelegramBotClient _client;

    public TelegramBotClientService(IOptions<TelegramConfigModel> options, ITelegramBotClient telegramBotClient)
    {
        _config = options.Value;
        _client = telegramBotClient;
    }

    public async Task setWebhook()
    {
        await _client.SetWebhook($"{_config.BaseUrL}api/message/update",allowedUpdates: []);

    }

}
