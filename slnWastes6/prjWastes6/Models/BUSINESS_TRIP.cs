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
        [Display(Name = "�Ǹ�")]
        [Required(ErrorMessage = "����")]
        [Key]        
        public decimal? TRIP_NO { get; set; }
      

        [Display(Name = "��ڸ��X")]
        [Required(ErrorMessage = "����")]
        //[Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    
        [Display(Name = "����")]
        [StringLength(200)]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name = "�m�W")]
        [StringLength(50)]
        public string USER_NAME { get; set; }

        [Display(Name = "¾�ȥN�z�H")]
        [StringLength(50)]
        public string OFFICE_AGENT { get; set; }

        [Display(Name = "�X�t����_")]
        [Column(TypeName = "datetime2")]
        public DateTime? FROM_PERIOD { get; set; }
        public string FormattedFROM_PERIOD
        {
            get
            {
                return FROM_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "�X�t�����")]
        [Column(TypeName = "datetime2")]
        public DateTime? UNTIL_PERIOD { get; set; }
        public string FormattedUNTIL_PERIOD
        {
            get
            {
                return UNTIL_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "�X�t�a�I")]
        [StringLength(200)]
        public string TRIP_LOCATION { get; set; }

        [Display(Name = "�p���q��")]
        [StringLength(200)]
        public string CONTACT_NUMBER { get; set; }
       
        [Display(Name = "�@��")]       
        [StringLength(1)]
        public string PASSPORT { get; set; }

        [Display(Name = "�x�M��")]
        [StringLength(1)]
        public string MTP { get; set; }

        [Display(Name = "���Ȩ�")]
        [StringLength(1)]
        public string OFFICIAL_CAR { get; set; }

        [Display(Name = "�p�����ΡG�T��")]
        [StringLength(1)]
        public string CAR { get; set; }

        [Display(Name = "�p�����ΡG����")]
        [StringLength(1)]
        public string SCOOTER { get; set; }

        [Display(Name = "�p�����ΡG���K")]
        [StringLength(1)]
        public string HIGH_SPEED_RAIL { get; set; }

        [Display(Name = "����(����)")]
        [StringLength(1)]
        public string AIRPLANE_TICKET { get; set; }

        [Display(Name = "����(����)�_")]
        [StringLength(200)]
        public string FROM_PLANE_TICKET { get; set; }

        [Display(Name = "����(����)��")]
        [StringLength(200)]
        public string AIRPLANE_TICKET_TO { get; set; }

        [Display(Name = "��L��q�u��")]
        [StringLength(200)]
        public string TRANSPORTATION { get; set; }

        [Display(Name = "ñ�ҡG��a�O")]
        [StringLength(200)]
        public string VISA_COUNTRY { get; set; }

        [Display(Name = "�ƥ�")]
        [StringLength(200)]
        public string REASON { get; set; }

        [Display(Name = "���X:���")]
        [Column(TypeName = "datetime2")]
        public DateTime? VISIT_DATE { get; set; }
        public string FormattedVISIT_DATE
        {
            get
            {
                return VISIT_DATE?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "���X:�a�I")]
        [StringLength(200)]
        public string VISIT_LOCATION { get; set; }


        [Display(Name = "���X���q")]
        [StringLength(200)]
        public string VISIT_COMPANY { get; set; }

        [Display(Name = "���X��H")]
        [StringLength(200)]
        public string INTERVIEWEES { get; set; }

        [Display(Name = "���X�ɶ�")]
        [StringLength(200)]
        public string VISITING_TIME { get; set; }


        [Display(Name = "�����ұƶq")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }

        [Display(Name = "�ұƫY��")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "�G��ƺҷ�q")]
        public decimal? CARBON_DIOXIDE { get; set; }
        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

    }

}

