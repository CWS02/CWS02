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
        [Display(Name = "�m�W")]
        [StringLength(200)]
        public string DOCUMENT_NAME { get; set; }
        [Display(Name = "�ҵ{���")]
        public DateTime? DEPARTURE_DATE { get; set; }
        [Display(Name = "�ҵ{�a�I")]
        [StringLength(200)]
        public string AIRPORT { get; set; }
        [Display(Name = "�ت��a")]
        [StringLength(200)]
        public string DESTINATION { get; set; }
        [Display(Name = "����")]
        [StringLength(200)]
        public string CABIN_CLASS { get; set; }
        [Display(Name = "���{��mi")]
        [StringLength(200)]
        public string MILAGE { get; set; }
        [Display(Name = "passengers'CO2/journey(KG)")]
        public decimal? EMISSIONS { get; set; }
        [Display(Name = "�^�{���")]
        public DateTime? RETURN_DATE { get; set; }
        [Display(Name = "�ҵ{�a�I")]
        [StringLength(200)]
        public string RETURN_AIRPORT { get; set; }
        [Display(Name = "�ت��a")]
        [StringLength(200)]
        public string RETURN_DESTINATION { get; set; }
        [Display(Name = "����")]
        [StringLength(200)]
        public string RETURN_CABIN { get; set; }
        [Display(Name = "���{��mi")]
        [StringLength(200)]
        public string RETURN_MILAGE { get; set; }
        [Display(Name = "passengers'CO2/journey(KG)")]
        public decimal? RETURN_EMISSIONS { get; set; }
        [Display(Name = "�`���{��mi")]
        [StringLength(200)]
        public string TOTAL_MILAGE { get; set; }
        [Display(Name = "Totalpassengers'CO2/journey(KG)")]
        public decimal? TOTAL_EMISSIONS { get; set; }
        [Display(Name = "���Ҹ��")]
        [StringLength(200)]
        public string INFORMATION { get; set; }

        public byte[] PDF_FILE { get; set; }

        public string ImageMimeType { get; set; }

        public byte[] PDF_CONTENT { get; set; }
        [Display(Name = "�Ƶ�")]
        [StringLength(200)]
        public string REMARK { get; set; }
        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

        public DateTime? data { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Display(Name = "�X�t�ӽг�:��ڸ��X")]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    }
}
