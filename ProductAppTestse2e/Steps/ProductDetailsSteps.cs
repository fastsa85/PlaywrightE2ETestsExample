using NUnit.Framework;
using ProductApp.Tests.e2e.PageObjects;
using ProductAppSample.Models;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProductApp.Tests.e2e.Steps
{
    [Binding]
    public class ProductDetailsSteps
    {
        private readonly ProductDetailsPage _productDetailsPage;
        private readonly ProductsComparer _productsComparer;

        public ProductDetailsSteps(ProductDetailsPage productDetailsPage, ProductsComparer productsComparer)
        {
            _productDetailsPage = productDetailsPage;
            _productsComparer = productsComparer;
        }

        [Given(@"the user is on the Product Details page")]
        public async Task GivenTheUserIsOnTheProductDetailsPage()
        {
            await _productDetailsPage.NavigateAsync();
        }

        [When(@"the user enters product details")]
        public async Task WhenTheUserEntersProductDetails(Table table)
        {
            var product = table.CreateInstance<Product>();
            await _productDetailsPage.SetProductNameAsync(product.ProductName);
            await _productDetailsPage.SetProductDescriptionAsync(product.ProductDescription);
            await _productDetailsPage.SetProductTypeAsync(product.ProductType);
            await _productDetailsPage.SetProductSupplierAsync(product.ProductSupplier);
            await _productDetailsPage.SetProductManufacturerAsync(product.ProductManufacturer);
        }

        [When(@"the user clicks Submit button on the Product Details page")]
        public async Task WhenTheUserClicksSubmitButton()
        {
            await _productDetailsPage.ClickSubmitAsync();
        }

        [Then(@"list of products contains items as follows")]
        public async Task ThenListOfProductsContainsItemsAsFollows(Table table)
        {
            var expectedProducts = table.CreateSet<Product>();
            var actualProducts = await _productDetailsPage.GetProductsAsync();

            CollectionAssert.AreEqual(expectedProducts, actualProducts, _productsComparer);
        }
    }
}
