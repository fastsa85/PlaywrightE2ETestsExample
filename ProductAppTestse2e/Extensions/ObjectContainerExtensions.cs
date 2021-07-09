using BoDi;
using Microsoft.Playwright;
using ProductApp.Tests.e2e.PageObjects;
using System;
using System.Threading.Tasks;

namespace ProductApp.Tests.e2e.Extensions
{
    public static class ObjectContainerExtensions
    {
        public static IObjectContainer AddConfiguration(this IObjectContainer container, Action<TestSettings> configureSettings, out TestSettings settings)
        {
            settings = new TestSettings();
            configureSettings?.Invoke(settings);
            container.RegisterInstanceAs(settings);
            return container;
        }

        public static async Task<IObjectContainer> RegisterPlaywrightAsync(this IObjectContainer container)
        {
            var settings = container.Resolve<TestSettings>();

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = settings.PlaywrightSettings.Headless,
                SlowMo = settings.PlaywrightSettings.SlowMo
            });
                       
            container.RegisterInstanceAs(playwright);         
            container.RegisterInstanceAs(browser);            
            return container;
        }

        public static IObjectContainer RegisterPages(this IObjectContainer container)
        {
            var settings = container.Resolve<TestSettings>();

            var browser = container.Resolve<IBrowser>();            
            container.RegisterInstanceAs<ProductDetailsPage>(new ProductDetailsPage(browser, settings));
            return container;
        }

        public static IObjectContainer RegisterComparers(this IObjectContainer container)
        {
            container.RegisterInstanceAs<ProductsComparer>(new ProductsComparer());
            return container;
        }
    }
}
