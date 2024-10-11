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

        [Display(Name = "�X�t�ӽг�:��ڸ��X")]
        //[Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }


        [Display(Name = "���I�W��")]
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

     
        [Display(Name = "���B")]
        public decimal? AMOUNT { get; set; }

        public string FormattedAMOUNT
        {
            get
            {
                return AMOUNT?.ToString("C0");
                //��ܦ����B�榡
            }
        }
        [Display(Name = "������")]
        public decimal? KILOMETERS { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "�Ƶ�")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
        [Display(Name = "���Ҹ��")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

       
    }

}

