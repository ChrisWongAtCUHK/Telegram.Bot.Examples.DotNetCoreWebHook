using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System;

namespace Telegram.Bot.Examples.DotNetCoreWebHook
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(String.Format("https://api.telegram.org/bot{0}/setWebhook?url={1}",
                        Config.Env.BotAccessToken,
                        Config.Env.WebhookUrl
                    ));
                req.Method = "POST";
                var resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader (stream);
                var webhook = JsonConvert.DeserializeObject<Models.WebhookResponse>(reader.ReadToEnd());
                if(!webhook.OK || !webhook.Result)
                {
                    throw new Exception(webhook.Description);
                }
                Console.WriteLine();
                BuildWebHost(args).Run();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }    
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
