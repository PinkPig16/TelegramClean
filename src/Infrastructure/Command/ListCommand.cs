using Application.Interfaces;
using Application.IRepository;
using Application.Service;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Command
{
    public class ListCommand : IMessageCommand
    {
        public string CommandName => "/list";
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ITelegramBotClient _botClient;
        private readonly TextGenerationService _textService;
        public ListCommand(ISubscriptionsRepository subscriptionsRepository, 
            ITelegramBotClient botClient, TextGenerationService textService) 
        {
            _subscriptionsRepository = subscriptionsRepository;
            _botClient = botClient;
            _textService = textService;

        }


        public async Task HandleCommand(Update update)
        {
            var subList = await _subscriptionsRepository.GetAllByUserAsync(update.Message.From.Id);
            
            await _botClient.SendMessage(update.Message.Chat.Id,_textService.ListText(subList),ParseMode.Markdown);


        }
    }
}
