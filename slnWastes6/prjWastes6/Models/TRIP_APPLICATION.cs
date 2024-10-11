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

        //��ڽs���۰ʥ[�J_�~���+�y����

        [Display(Name = "��ڸ��X")]
        [Required(ErrorMessage = "����")]
        [Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }

        //�����:
       

        [Display(Name = "�����:")]
        [Column(TypeName = "datetime2")]
        public DateTime? FILLING_DAY { get; set; }
        public string FormattedFILLING_DAY
        {
            get
            {
                return FILLING_DAY?.ToString("yyyy-MM-dd");
            }
        }

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

        [Display(Name = "�@�X��")]
        //public decimal? DAYS { get; set; }

        public int? DAYS { get; set; }

        [Display(Name = "�X�t�a�I")]
        [Required(ErrorMessage = "�п�ܥX�t�a�I")]
        //[StringLength(200)]
        public string TRIP_LOCATION { get; set; }
        //20240401���ثe�� string �����C�N��קאּ���L�� (bool) ����,�H�K�ϥήت�ܰꤺ�ΰ�~       
        //public bool TRIP_LOCATION { get; set; }

        //[NotMapped] // �o���ݩʤ��|�������Ʈw���
        //public string TRIP_LOCATION_STRING
        //{
        //get { return TRIP_LOCATION ? "��~" : "�ꤺ"; }
        //set { TRIP_LOCATION = value == "��~"; }
        //}



        [Display(Name = "�p���q��")]
        [StringLength(200)]
        public string CONTACT_NUMBER { get; set; }

        [Display(Name = "�ӽи�ƤΥD�n��q�u��")]
        [StringLength(200)]
        public string APPLICATION_INFORMATION { get; set; }

        [Display(Name = "�ƥ�")]
        [StringLength(200)]
        public string REASON { get; set; }

        [Display(Name = "���/�a�I/���X���q/���X��H/���X�_�W�ɶ�:")]
        [StringLength(2000)]
        public string VISIT_CONTENT { get; set; }

        
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

    }

}

