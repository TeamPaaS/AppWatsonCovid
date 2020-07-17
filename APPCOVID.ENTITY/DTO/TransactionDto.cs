using System;
using System.Collections.Generic;
using System.Text;

namespace APPCOVID.Entity.DTO
{
   public class TransactionDto
    {
        public int TRANSACTIONID { get; set; }
        public int PRODUCTID { get; set; }
        public int CUSTOMERID { get; set; }
        public string SUBSCRIPTIONTYPE { get; set; }
        public string PODETAILS { get; set; }
        public string CREATEDDATE { get; set; }
        public string VALIDUPTODATE { get; set; }
        public string STATUS { get; set; }
    }
}
