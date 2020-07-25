using APPCOVID.DAL.DataManagers.Managers;
using APPCOVID.Entity.DTO;
using APPCOVID.Entity.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace APPCOVID.BAL.Helpers
{
    public class TransactionHelper
    {
        private TransactionManager _transactionManager;
        public TransactionHelper()
        {
            _transactionManager = new TransactionManager();
        }

        public IList<TransactionViewModel> GetAll()
        {
            List<TransactionDto> transaction = _transactionManager.GetTransactionData();
            return viewMapper(transaction);
        }
        public TransactionViewModel GetAllById(int tid)
        {
            List<TransactionDto> getTransactionInfo = _transactionManager.GetTransactionData();
            TransactionDto transactionDetails = getTransactionInfo.Where(t => t.PRODUCTID == tid).FirstOrDefault();
            TransactionViewModel transactionInfo = CommonHelper.ConvertTo<TransactionViewModel>(transactionDetails);
            return transactionInfo;
        }
        public IList<TransactionViewModel> GetAllByUserId(int uid)
        {
            List<TransactionDto> getTransactionInfo = _transactionManager.GetTransactionData();
           List <TransactionDto> transactionDetails = getTransactionInfo.Where(t => t.CUSTOMERID == uid).ToList();
           // TransactionViewModel transactionInfo = CommonHelper.ConvertTo<TransactionViewModel>(transactionDetails);
            return viewMapper(transactionDetails);
        }

        public bool CreateTransaction(TransactionViewModel transaction)
        {
            return _transactionManager.CreateTransactionData(transaction);
        }
        public IList<TransactionViewModel> viewMapper(List<TransactionDto> transaction)
        {
            return transaction.Select(t => t.ConvertTo<TransactionViewModel>()).AsEnumerable().ToList();
        }
        public bool UpdateTransaction(TransactionViewModel transaction)
        {
            return _transactionManager.UpdateProductTransaction(transaction);
        }
    }
}
