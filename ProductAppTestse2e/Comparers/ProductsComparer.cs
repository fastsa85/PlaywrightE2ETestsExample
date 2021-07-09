using ProductAppSample.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Tests.e2e
{
    public class ProductsComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var productX = (Product)x;
            var productY = (Product)y;

            return productX.ProductName == productY.ProductName &&
                productX.ProductDescription == productX.ProductDescription &&
                productX.ProductType == productY.ProductType &&
                productX.ProductSupplier == productY.ProductSupplier &&
                productX.ProductManufacturer == productY.ProductManufacturer ? 0 : -1;
        }
    }
}
