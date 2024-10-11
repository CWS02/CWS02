namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMPLOYEE_TRAVEL_ABROAD
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal TRIP_NO { get; set; }
        [Display(Name = "姓名")]
        [StringLength(200)]
        public string DOCUMENT_NAME { get; set; }
        [Display(Name = "啟程日期")]
        public DateTime? DEPARTURE_DATE { get; set; }
        [Display(Name = "啟程地點")]
        [StringLength(200)]
        public string AIRPORT { get; set; }
        [Display(Name = "目的地")]
        [StringLength(200)]
        public string DESTINATION { get; set; }
        [Display(Name = "艙等")]
        [StringLength(200)]
        public string CABIN_CLASS { get; set; }
        [Display(Name = "里程數mi")]
        [StringLength(200)]
        public string MILAGE { get; set; }
        [Display(Name = "passengers'CO2/journey(KG)")]
        public decimal? EMISSIONS { get; set; }
        [Display(Name = "回程日期")]
        public DateTime? RETURN_DATE { get; set; }
        [Display(Name = "啟程地點")]
        [StringLength(200)]
        public string RETURN_AIRPORT { get; set; }
        [Display(Name = "目的地")]
        [StringLength(200)]
        public string RETURN_DESTINATION { get; set; }
        [Display(Name = "艙等")]
        [StringLength(200)]
        public string RETURN_CABIN { get; set; }
        [Display(Name = "里程數mi")]
        [StringLength(200)]
        public string RETURN_MILAGE { get; set; }
        [Display(Name = "passengers'CO2/journey(KG)")]
        public decimal? RETURN_EMISSIONS { get; set; }
        [Display(Name = "總里程數mi")]
        [StringLength(200)]
        public string TOTAL_MILAGE { get; set; }
        [Display(Name = "Totalpassengers'CO2/journey(KG)")]
        public decimal? TOTAL_EMISSIONS { get; set; }
        [Display(Name = "佐證資料")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        public byte[] PDF_FILE { get; set; }

        public string ImageMimeType { get; set; }

        public byte[] PDF_CONTENT { get; set; }
        [Display(Name = "備註")]
        [StringLength(200)]
        public string REMARK { get; set; }
        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

        public DateTime? data { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Display(Name = "出差申請單:單據號碼")]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    }
}
