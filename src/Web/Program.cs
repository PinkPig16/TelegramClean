using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using HtmlAgilityPack;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Application.Service;
using Infrastructure.Command;
using Web.Service;
using Web.Command.Handler;
using Infrastructure.DTO;
using Telegram.Bot;
using Infrastructure.Mapping;
using Infrastructure.Builder;
using Infrastructure.Parse;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var botConfigSection = builder.Configuration.GetSection("TelegramBot");
builder.Services.Configure<TelegramConfigModel>(botConfigSection);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(botConfigSection.Get<TelegramConfigModel>().Token, httpClient));
builder.Services.AddLogging();
builder.Services.AddScoped<IMessageCommand, StartCommand>();
builder.Services.AddScoped<IMessageCommand, AddCommand>();
builder.Services.AddScoped<IMessageCommand, CallbackCommand>();
builder.Services.AddScoped<IMessageCommand, ListCommand>(); 
builder.Services.AddScoped<IMessageCommand, DelateCommand>();
builder.Services.AddScoped<TextGenerationService>();
builder.Services.AddScoped<VacanciesBuilder>();
builder.Services.AddScoped<CallbackBase, CallbackDTO>();
builder.Services.AddScoped<ICallbackFactory, CallbackFactory>();
builder.Services.AddScoped<IMember, DeleteMember>();
builder.Services.AddScoped<IMember, AddMember>();
builder.Services.AddScoped<IVacanciesService, VacanciesService>(); 
builder.Services.AddScoped<ParseMessage>();
builder.Services.AddTransient<Subscriptions>();
builder.Services.AddTransient<City>();
builder.Services.AddTransient<Vacancies>();
builder.Services.AddScoped<AppUserService>();
builder.Services.AddScoped<SubscriptionsService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<CommandsRegistry>();
builder.Services.AddScoped<CommandHadler>();
builder.Services.AddScoped<SubscriptionsBuilder>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IMessage, MessageOperation>();
builder.Services.AddScoped<IParse, ParseWebSite>();
builder.Services.AddScoped<HtmlWeb>();
builder.Services.AddScoped<ITransliterate, TransliterationService>();
builder.Services.AddSingleton<TelegramBotClientService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.ConfigureTelegramBotMvc();
builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

var app = builder.Build();


using (var scoped = app.Services.CreateScope())
{
    var telegramBotClientService = scoped.ServiceProvider.GetRequiredService<TelegramBotClientService>();
    await telegramBotClientService.setWebhook();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
//TODO Middleware
//app.UseMiddleware<ValidatorMiddleware>();
app.Use((context, next) => next());
app.MapControllers();

app.Run();
