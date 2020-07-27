using System;
using System.ComponentModel.DataAnnotations;

namespace APPCOVID.Entity.DTO
{
    public class ProductDto
    {
        public int PRODUCTID { get; set; }
        public string PRODUCTTYPE { get; set; }
        public int CUSTOMERID { get; set; }
        public int STAGE { get; set; }

        [Required(ErrorMessage = "*Mandatory")]
        [StringLength(500, ErrorMessage = "Max 500 chars only.")]
        public string DESCRIPTION { get; set; }

        [Required(ErrorMessage = "*Mandatory")]
        [StringLength(100, ErrorMessage = "Max 500 chars only.")]
        public string SHORTDESCRIPTION { get; set; }

        [Required(ErrorMessage = "*Mandatory")]
        [StringLength(500, ErrorMessage = "Max 500 chars only.")]
        public string IMAGEURL { get; set; }

        [Required(ErrorMessage ="*Mandatory")]
        [StringLength(500, ErrorMessage = "Max 500 chars only.")]
        public string PRODUCTURL { get; set; }
        public string STATUS { get; set; }

    }
}
