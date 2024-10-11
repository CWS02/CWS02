using System;

namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Table("TAXI_APPLICATION")]
    public partial class TAXI_APPLICATION
    {
        [Key]
        [Display(Name = "Trip No")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal? TRIP_NO { get; set; }

        [Display(Name = "出差申請單:單據號碼")]
        //[Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }


        [Display(Name = "據點名稱")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }

        

        [Display(Name = "日期")]
        [Column(TypeName = "datetime2")]
        public DateTime? DOCUMENT_DATA { get; set; }
        public string FormattedDOCUMENT_DATA
        {
            get
            {
                return DOCUMENT_DATA?.ToString("yyyy-MM-dd");
            }
        }

     
        [Display(Name = "金額")]
        public decimal? AMOUNT { get; set; }

        public string FormattedAMOUNT
        {
            get
            {
                return AMOUNT?.ToString("C0");
                //顯示成金額格式
            }
        }
        [Display(Name = "公里數")]
        public decimal? KILOMETERS { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "備註")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
        [Display(Name = "佐證資料")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

       
    }

}

