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

    [Table("BUSINESS_TRIP")]
    public partial class BUSINESS_TRIP
    {
        [Display(Name = "序號")]
        [Required(ErrorMessage = "必填")]
        [Key]        
        public decimal? TRIP_NO { get; set; }
      

        [Display(Name = "單據號碼")]
        [Required(ErrorMessage = "必填")]
        //[Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    
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

        [Display(Name = "出差地點")]
        [StringLength(200)]
        public string TRIP_LOCATION { get; set; }

        [Display(Name = "聯絡電話")]
        [StringLength(200)]
        public string CONTACT_NUMBER { get; set; }
       
        [Display(Name = "護照")]       
        [StringLength(1)]
        public string PASSPORT { get; set; }

        [Display(Name = "台胞證")]
        [StringLength(1)]
        public string MTP { get; set; }

        [Display(Name = "公務車")]
        [StringLength(1)]
        public string OFFICIAL_CAR { get; set; }

        [Display(Name = "私車公用：汽車")]
        [StringLength(1)]
        public string CAR { get; set; }

        [Display(Name = "私車公用：機車")]
        [StringLength(1)]
        public string SCOOTER { get; set; }

        [Display(Name = "私車公用：高鐵")]
        [StringLength(1)]
        public string HIGH_SPEED_RAIL { get; set; }

        [Display(Name = "飛機(機票)")]
        [StringLength(1)]
        public string AIRPLANE_TICKET { get; set; }

        [Display(Name = "飛機(機票)起")]
        [StringLength(200)]
        public string FROM_PLANE_TICKET { get; set; }

        [Display(Name = "飛機(機票)迄")]
        [StringLength(200)]
        public string AIRPLANE_TICKET_TO { get; set; }

        [Display(Name = "其他交通工具")]
        [StringLength(200)]
        public string TRANSPORTATION { get; set; }

        [Display(Name = "簽證：國家別")]
        [StringLength(200)]
        public string VISA_COUNTRY { get; set; }

        [Display(Name = "事由")]
        [StringLength(200)]
        public string REASON { get; set; }

        [Display(Name = "拜訪:日期")]
        [Column(TypeName = "datetime2")]
        public DateTime? VISIT_DATE { get; set; }
        public string FormattedVISIT_DATE
        {
            get
            {
                return VISIT_DATE?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "拜訪:地點")]
        [StringLength(200)]
        public string VISIT_LOCATION { get; set; }


        [Display(Name = "洽訪公司")]
        [StringLength(200)]
        public string VISIT_COMPANY { get; set; }

        [Display(Name = "洽訪對象")]
        [StringLength(200)]
        public string INTERVIEWEES { get; set; }

        [Display(Name = "拜訪時間")]
        [StringLength(200)]
        public string VISITING_TIME { get; set; }


        [Display(Name = "本期碳排量")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }

        [Display(Name = "碳排係數")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "二氧化碳當量")]
        public decimal? CARBON_DIOXIDE { get; set; }
        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

    }

}

