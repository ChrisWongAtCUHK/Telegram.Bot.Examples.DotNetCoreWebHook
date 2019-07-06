using System;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Services
{
    public class BotService : IBotService
    {
        public BotService()
        {
            try
            {
                Client = new TelegramBotClient(Config.Env.BotAccessToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TelegramBotClient Client { get; }
    }
}
