using Application.Interfaces;
using Application.IRepository;
using Application.Service;
using Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Command
{
    public class DelateCommand : IMessageCommand
    {
        public string CommandName => "/delete";
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ITelegramBotClient _botClient;
        private readonly TextGenerationService _textService;
        private readonly ILogger<DelateCommand> _logger;

        public DelateCommand(ISubscriptionsRepository subscriptionsRepository,
            ITelegramBotClient botClient, TextGenerationService textService,
            ILogger<DelateCommand> logger)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _botClient = botClient;
            _textService = textService;
            _logger = logger;
        }

        public async Task HandleCommand(Update update)
        {
            if (update.Message.Entities.Count() > 1)
            {
                _logger.NotEntities(update.Id);
                return;
            }

            var method = update.Message.Entities.First();
            var textWithoutMethod = update.Message.Text.Substring(method.Length);

            if (!Int32.TryParse(textWithoutMethod, out int number))
            {
                _logger.IntParseExection(update.Id);
                return;
            }
            var subscription = await _subscriptionsRepository.DeleletByIndexAsync(update.Message.From.Id, number);
            
            if (subscription == null)
            {
               await _botClient.SendMessage(update.Message.Chat, "*Ошибка удаления*", ParseMode.MarkdownV2);
            }
            else
            {
                await _botClient.SendMessage(update.Message.Chat, $"Удалил подписку *{subscription.Name}*",ParseMode.MarkdownV2);
            }



        }
    }
}
