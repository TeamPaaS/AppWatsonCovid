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
        private readonly ProductManager _productManager;
        private readonly TransactionHelper _transactionHelper;
        public ProductHelper()
        {
            _productManager = new ProductManager();
            _transactionHelper = new TransactionHelper();
            //UpdateTransaction();

        }

        public IList<ProductViewModel> GetAll()
        {
            List<ProductDto> products = _productManager.GetProductData();
            return ViewMapper(products);
        }

        public IList<ProductViewModel> GetAll(int stage)
        {
            List<ProductDto> activeProducts = new List<ProductDto>();
            List<ProductDto> products = _productManager.GetProductData().Where(t=>t.STAGE==stage).ToList();
            if (!products.Any()) return ViewMapper(activeProducts);
            {
                List<int> activeProductIds = _transactionHelper.GetAll()
                    .Where(t => string.Equals(t.STATUS, "active", System.StringComparison.CurrentCultureIgnoreCase))
                    .Select(t => t.PRODUCTID).ToList();
                foreach (var tranProdId in activeProductIds)
                {
                    activeProducts.AddRange(products.Where(prod => tranProdId == prod.PRODUCTID));
                }
            }

            return ViewMapper(activeProducts);
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

        public IList<ProductViewModel> ViewMapper(List<ProductDto> products)
        {
            return products.Select(t => t.ConvertTo<ProductViewModel>()).AsEnumerable().ToList();
        }

        private void UpdateTransaction() {
            var transactions = _transactionHelper.GetAll();
            foreach (var itm in transactions) {
                DateTime dt;
                DateTime.TryParse(itm.VALIDUPTODATE, out dt);
                if (DateTime.Now > dt) {
                    TransactionViewModel transaction = itm;
                    transaction.STATUS = "InActive";
                    _transactionHelper.UpdateTransaction(transaction);
                }
            }
        }
    }
}
