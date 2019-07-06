using Microsoft.Extensions.Options;
using MihaZupan;
using System;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Services
{
    public class BotService : IBotService
    {
        private readonly string botToken = Environment.GetEnvironmentVariable("BOT_ACCESS_TOKEN");
        public BotService()
        {
            Client = new TelegramBotClient(botToken);
        }

        public TelegramBotClient Client { get; }
    }
}
