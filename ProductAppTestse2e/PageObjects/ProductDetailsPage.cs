using Microsoft.Playwright;
using ProductAppSample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApp.Tests.e2e.PageObjects
{
    public class ProductDetailsPage : BasePageObject
    {
        private const string ProductNameSelector = "input[name='ProductName']";
        private const string ProductDescriptionSelector = "input[name='ProductDescription']";
        private const string ProductTypeSelector = "input[name='ProductType']";
        private const string ProductSupplierSelector = "input[name='ProductSupplier']";
        private const string ProductManufacturerSelector = "input[name='ProductManufacturer']";
        private const string SubmitButtonSelector = "button[type='submit']";

        private const string ProductItemsSelector = "table>tr";
        private const string ProductItemDetailsSelector = "td";

        public override string PagePath { get; }
        public override IPage Page { get; set; }
        public override IBrowser Browser { get; }

        public ProductDetailsPage(IBrowser browser, TestSettings settings)
        {
            PagePath = settings.BaseUrl;
            Browser = browser;
        }

        public Task SetProductNameAsync(string productName) => Page.FillAsync(ProductNameSelector, productName);
        public Task SetProductDescriptionAsync(string productDescription) => Page.FillAsync(ProductDescriptionSelector, productDescription);
        public Task SetProductTypeAsync(string productType) => Page.FillAsync(ProductTypeSelector, productType);
        public Task SetProductSupplierAsync(string productSupplier) => Page.FillAsync(ProductSupplierSelector, productSupplier);
        public Task SetProductManufacturerAsync(string productManufacturer) => Page.FillAsync(ProductManufacturerSelector, productManufacturer);
        public async Task ClickSubmitAsync()
        {
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await Page.ClickAsync(SubmitButtonSelector);
            }, response => response.Url == "https://latestproductapp.azurewebsites.net/api/Products/" && response.Request.Method == "GET");

            await Page.WaitForLoadStateAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = new List<Product>();
            var productItems = await Page.QuerySelectorAllAsync(ProductItemsSelector);
            foreach (var productItem in productItems)
            {
                var productDetails = await productItem.QuerySelectorAllAsync(ProductItemDetailsSelector);

                products.Add(new Product()
                {
                    ProductName = await productDetails[0].InnerHTMLAsync(),
                    ProductDescription = await productDetails[1].InnerHTMLAsync(),
                    ProductType = await productDetails[2].InnerHTMLAsync(),
                    ProductSupplier = await productDetails[3].InnerHTMLAsync(),
                    ProductManufacturer = await productDetails[4].InnerHTMLAsync(),
                });
            }

            return products;
        }
    }
}
