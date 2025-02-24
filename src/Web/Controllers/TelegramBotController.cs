using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Web.Command.Handler;
using Infrastructure.Logging;

namespace Web.Controllers;

[ApiController]
[Route("api/message")]
public class TelegramBotController : Controller
{
    private readonly IParse _parse;
    private readonly CommandHadler _commandHandler;
    private readonly ILogger _logger;
    public TelegramBotController(IParse parse,
                                CommandHadler commandHandler,
                                ILogger<TelegramBotController> logger)
    {
        _parse = parse;
        _commandHandler = commandHandler;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] Update update)
    {
        var command = _commandHandler.GetCommand(update);

        if (command == null)
        {
            _logger.MethodNotExist(update.Id);
            return Ok();
        }
        await command.HandleCommand(update);

        return Ok();
    }

    [HttpGet]
    [Route("Run")]
    public async Task<IActionResult> RunParse()
    {
        await _parse.GetVacancies();
        return Ok();
    }

}
