using BoDi;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ProductApp.Tests.e2e.Extensions;
using Microsoft.Playwright;
using ProductApp.Tests.e2e.PageObjects;
using Microsoft.Extensions.Configuration;

namespace ProductApp.Tests.e2e.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static async Task RegisterDependencies(IObjectContainer container)
        {
            AddConfiguration(container);

            await container.RegisterPlaywrightAsync();
            container.RegisterPages();
        }

        private static void AddConfiguration(IObjectContainer container)
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile("appsettings.development.json", optional: true)
                                .AddEnvironmentVariables()
                                .Build();

            container.AddConfiguration(configureSettings =>
            {
                configuration.GetSection("TestSettings").Bind(configureSettings);
            }, out var settings);
        }
    }
}
