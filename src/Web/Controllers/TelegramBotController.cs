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
    private readonly CommandHadler _commandHadler;
    private readonly ILogger _logger;

    public TelegramBotController(CommandsRegistry commandsRegistry,
                                IParse parse,
                                CommandHadler commandHadler,
                                ILogger<TelegramBotController> logger)
    {
        _parse = parse;
        _commandHadler = commandHadler;
        _logger = logger;
    }




    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] Update update)
    {
        if (update == null) return Ok();

        var command = _commandHadler.GetCommand(update);

        if (command == null)
        {
            _logger.MethotNotExist(update.Id);
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
