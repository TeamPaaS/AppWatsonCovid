using APPCOVID.DAL.DataManagers.Managers;
using APPCOVID.Entity.DTO;
using APPCOVID.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APPCOVID.BAL.Helpers
{
    public class ProductHelper
    {
        private ProductManager _productManager;
        private TransactionHelper _transactionHelper;
        public ProductHelper()
        {
            _productManager = new ProductManager();
            _transactionHelper = new TransactionHelper();
        }

        public IList<ProductViewModel> GetAll()
        {
            List<ProductDto> products = _productManager.GetProductData();
            return viewMapper(products);
        }

        public IList<ProductViewModel> GetAll(int stage)
        {
            List<ProductDto> activeproducts = new List<ProductDto>();
            List<ProductDto> products = _productManager.GetProductData().Where(t=>t.STAGE==stage).ToList();
            List<int> activeProductIds = _transactionHelper.GetAll()
                .Where(t => string.Equals(t.STATUS, "active", System.StringComparison.CurrentCultureIgnoreCase))
                .Select(t => t.PRODUCTID).ToList();
            foreach (int id in activeProductIds) {
                activeproducts.Add(products.Where(t => t.PRODUCTID == id).FirstOrDefault());
            }            
            return viewMapper(activeproducts);
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
