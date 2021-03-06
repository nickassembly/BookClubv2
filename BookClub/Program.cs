using BookClub.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Hosting;
using FluentEmail.Smtp;
using System.Net.Mail;
using FluentEmail.Core;
using System.Threading.Tasks;

namespace BookClub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length == 1 && args[0].ToLower() == "/seed")
            {
                SeedDb(host);
            }
            else
            {
                host.Run();
            }
        }
        private static void SeedDb(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<BookclubSeeder>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            //bldr.Sources.Clear();
            bldr.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("config.json") //Extra config
                .AddEnvironmentVariables();
        }
    }
}
