using System;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Services
{
    public class BotService : IBotService
    {
        private readonly string botToken = Environment.GetEnvironmentVariable("BOT_ACCESS_TOKEN");
        public BotService()
        {
            try
            {
                if(String.IsNullOrEmpty(botToken)) throw new Exception("Bot access token is empty.");
                Client = new TelegramBotClient(botToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TelegramBotClient Client { get; }
    }
}
