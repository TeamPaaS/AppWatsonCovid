using APPCOVID.DAL.DataManagers.Managers;
using APPCOVID.Entity.DTO;
using APPCOVID.Entity.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace APPCOVID.BAL.Helpers
{
    public class ProductHelper
    {
        private ProductManager _productManager;
        public ProductHelper()
        {
            _productManager = new ProductManager();
        }

        public IList<ProductViewModel> GetAll()
        {
            List<ProductDto> products = _productManager.GetProductData();
            return viewMapper(products);
        }
        public ProductViewModel GetAllById(int pid)
        {
            List<ProductDto> getProductInfo = _productManager.GetProductData();
            ProductDto productDetails = getProductInfo.Where(t => t.PRODUCTID == pid).FirstOrDefault();
            ProductViewModel productInfo = CommonHelper.ConvertTo<ProductViewModel>(productDetails);
            return productInfo;
        }

        public bool CreateProduct(ProductViewModel product)
        {
            return _productManager.CreateInsuranceProduct(product);
        }
        public bool UpdateProduct(ProductViewModel product)
        {
            return _productManager.UpdateInsuranceProduct(product);
        }

        public IList<ProductViewModel> viewMapper(List<ProductDto> products)
        {
            return products.Select(t => t.ConvertTo<ProductViewModel>()).AsEnumerable().ToList();
        }
    }
}
