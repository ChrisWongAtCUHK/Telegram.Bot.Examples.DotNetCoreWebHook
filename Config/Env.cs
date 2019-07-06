using System;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Config
{
    public class Env
    {
        public static string BotAccessToken 
        { 
            get 
            { 
                var token = Environment.GetEnvironmentVariable("BOT_ACCESS_TOKEN"); 
                if(String.IsNullOrEmpty(token)) throw new Exception("Bot access token does not exist.");
                return token;              
            } 
        }
        public static string WebhookUrl 
        { 
            get 
            { 
                var url = Environment.GetEnvironmentVariable("WEBHOOK_URL");
                if(String.IsNullOrEmpty(url)) throw new Exception("Webhook url does not exist.");
                return url;
            } 
        }
    }
}
