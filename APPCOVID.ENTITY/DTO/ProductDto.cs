using System;

namespace APPCOVID.Entity.DTO
{
    public class ProductDto
    {
        public int PRODUCTID { get; set; }
        public string PRODUCTTYPE { get; set; }
        public int CUSTOMERID { get; set; }
        public int STAGE { get; set; }
        public string DESCRIPTION { get; set; }          
        public string SHORTDESCRIPTION { get; set; }
        public string IMAGEURL { get; set; }
        public string PRODUCTURL { get; set; }
        public string STATUS { get; set; }

    }
}
