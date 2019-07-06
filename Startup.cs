using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Telegram.Bot.Examples.DotNetCoreWebHook.Services;

namespace Telegram.Bot.Examples.DotNetCoreWebHook
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var contentRoot = env.ContentRootPath;
            Console.WriteLine("Startup==============================");
            // Get list of files in the specific directory.
            // ... Please change the first argument.
            string[] files = Directory.GetFiles("/app",
                "*.*",
                SearchOption.AllDirectories);

            // Display all the files.
            foreach (string file in files)
            {
                if(!file.Contains(".git"))
                {
                    Console.WriteLine(file);
                }
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
