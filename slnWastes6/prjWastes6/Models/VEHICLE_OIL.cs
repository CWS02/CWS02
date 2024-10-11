namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VEHICLE_OIL
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal TRIP_NO { get; set; }
        [Display(Name = "單據日期")]
        public DateTime? DOCUMENT_DATA { get; set; }
        [Column(TypeName = "datetime")]
        public string FormattedDOCUMENT_DATA
        {
            get
            {
                return DOCUMENT_DATA?.ToString("yyyy-MM-dd");
            }
        }
        [Display(Name = "ESG交通類別")]
        [StringLength(200)]
        public string CATEGORY { get; set; }
        [Display(Name = "ESG公升數")]
        public decimal? LITERS { get; set; }
        [Display(Name = "據點名稱")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }
        [Display(Name = "加油種類")]
        [StringLength(200)]
        public string OIL_TYPE { get; set; }
        [Display(Name = "佐證資料")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        public byte[] PDF_FILE { get; set; }

        public string ImageMimeType { get; set; }

        public byte[] PDF_CONTENT { get; set; }
        [Display(Name = "備註")]
        [StringLength(200)]
        public string REMARK { get; set; }
        [Display(Name = "憑單單號")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

        public DateTime? data { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
