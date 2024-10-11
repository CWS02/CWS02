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

    [Table("TRIP_APPLICATION")]
    public partial class TRIP_APPLICATION
    {

        //單據編號自動加入_年月日+流水號

        [Display(Name = "單據號碼")]
        [Required(ErrorMessage = "必填")]
        [Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }

        //填表日期:
       

        [Display(Name = "填表日期:")]
        [Column(TypeName = "datetime2")]
        public DateTime? FILLING_DAY { get; set; }
        public string FormattedFILLING_DAY
        {
            get
            {
                return FILLING_DAY?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "部門")]
        [StringLength(200)]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name = "姓名")]
        [StringLength(50)]
        public string USER_NAME { get; set; }

        [Display(Name = "職務代理人")]
        [StringLength(50)]
        public string OFFICE_AGENT { get; set; }

        [Display(Name = "出差日期起")]
        [Column(TypeName = "datetime2")]
        public DateTime? FROM_PERIOD { get; set; }
        public string FormattedFROM_PERIOD
        {
            get
            {
                return FROM_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "出差日期迄")]
        [Column(TypeName = "datetime2")]
        public DateTime? UNTIL_PERIOD { get; set; }
        public string FormattedUNTIL_PERIOD
        {
            get
            {
                return UNTIL_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "共幾天")]
        //public decimal? DAYS { get; set; }

        public int? DAYS { get; set; }

        [Display(Name = "出差地點")]
        [Required(ErrorMessage = "請選擇出差地點")]
        //[StringLength(200)]
        public string TRIP_LOCATION { get; set; }
        //20240401欄位目前為 string 類型。將其修改為布林值 (bool) 類型,以便使用框表示國內或國外       
        //public bool TRIP_LOCATION { get; set; }

        //[NotMapped] // 這個屬性不會對應到資料庫欄位
        //public string TRIP_LOCATION_STRING
        //{
        //get { return TRIP_LOCATION ? "國外" : "國內"; }
        //set { TRIP_LOCATION = value == "國外"; }
        //}



        [Display(Name = "聯絡電話")]
        [StringLength(200)]
        public string CONTACT_NUMBER { get; set; }

        [Display(Name = "申請資料及主要交通工具")]
        [StringLength(200)]
        public string APPLICATION_INFORMATION { get; set; }

        [Display(Name = "事由")]
        [StringLength(200)]
        public string REASON { get; set; }

        [Display(Name = "日期/地點/洽訪公司/洽訪對象/拜訪起訖時間:")]
        [StringLength(2000)]
        public string VISIT_CONTENT { get; set; }

        
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

    }

}

