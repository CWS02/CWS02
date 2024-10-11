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
        [Display(Name = "��ڤ��")]
        public DateTime? DOCUMENT_DATA { get; set; }
        [Column(TypeName = "datetime")]
        public string FormattedDOCUMENT_DATA
        {
            get
            {
                return DOCUMENT_DATA?.ToString("yyyy-MM-dd");
            }
        }
        [Display(Name = "ESG��q���O")]
        [StringLength(200)]
        public string CATEGORY { get; set; }
        [Display(Name = "ESG���ɼ�")]
        public decimal? LITERS { get; set; }
        [Display(Name = "���I�W��")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }
        [Display(Name = "�[�o����")]
        [StringLength(200)]
        public string OIL_TYPE { get; set; }
        [Display(Name = "���Ҹ��")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        public byte[] PDF_FILE { get; set; }

        public string ImageMimeType { get; set; }

        public byte[] PDF_CONTENT { get; set; }
        [Display(Name = "�Ƶ�")]
        [StringLength(200)]
        public string REMARK { get; set; }
        [Display(Name = "�̳�渹")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

        public DateTime? data { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
