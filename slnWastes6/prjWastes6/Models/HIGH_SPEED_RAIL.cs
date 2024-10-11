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
        [Display(Name = "��")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal? TRIP_NO { get; set; }

        [Display(Name = "���ڳ�O�渹")]
        //[Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }


        [Display(Name = "�H��")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }

        

        [Display(Name = "���")]
        [Column(TypeName = "datetime2")]
        public DateTime? DOCUMENT_DATA { get; set; }
        public string FormattedDOCUMENT_DATA
        {
            get
            {
                return DOCUMENT_DATA?.ToString("yyyy-MM-dd");
            }
        }
        [Display(Name = "�_�I")]
        [StringLength(200)]
        public string ORIGIN { get; set; }

        [Display(Name = "���I")]
        [StringLength(200)]
        public string DESTINATION { get; set; }

        [Display(Name = "�Ӧ^or��{")]
        [StringLength(200)]
        public string ROUND_ONE { get; set; }

        [Display(Name = "����/���ɼ�")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }
        [Display(Name = "��q���O")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "���ڳƵ�")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
        

        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

       
    }

}

