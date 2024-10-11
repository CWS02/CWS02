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

    [Table("HIGH_SPEED_RAIL")]
    public partial class HIGH_SPEED_RAIL
    {
        [Key]
        [Display(Name = "序")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal? TRIP_NO { get; set; }

        [Display(Name = "票據單別單號")]
        //[Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }


        [Display(Name = "人員")]
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
        [Display(Name = "起點")]
        [StringLength(200)]
        public string ORIGIN { get; set; }

        [Display(Name = "迄點")]
        [StringLength(200)]
        public string DESTINATION { get; set; }

        [Display(Name = "來回or單程")]
        [StringLength(200)]
        public string ROUND_ONE { get; set; }

        [Display(Name = "公里/公升數")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }
        [Display(Name = "交通類別")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "票據備註")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
        

        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

       
    }

}

