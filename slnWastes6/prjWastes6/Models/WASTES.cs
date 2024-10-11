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
    using prjWastes6.Models.Extensions;

    [Table("WASTES")]

    public partial class WASTES
    {
        [Display(Name = "�p��s��")]
        [Required(ErrorMessage = "����")]
        [Key]
        
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
        [Display(Name = "�M�B���")]
        [Column(TypeName = "datetime2")]
        public DateTime? REMOVAL_DATE { get; set; }

        public string FormattedRemovalDate
        {
            get
            {
                return REMOVAL_DATE?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "�M���̥N�X")]
        [StringLength(200)]
        public string CLEANER_CODE { get; set; }
        [Display(Name = "�M���̦W��")]
        [StringLength(200)]
        public string PURGE_NAME { get; set; }
        [Display(Name = "�B������")]
        [StringLength(200)]
        public string NUMBER_CARRIER { get; set; }
        [Display(Name = "�����B�z�覡")]
        [StringLength(200)]
        public string TREATMENT { get; set; }
        [Display(Name = "�̲׳B�m�̥N�X")]
        [StringLength(200)]
        public string FINAL_DISPOSER_CODE { get; set; }
        [Display(Name = "�̲׳B�m�̦W��")]
        [StringLength(200)]
        public string FINAL_DISPOSER_NAME { get; set; }
        [Display(Name = "�̲׳B�m�覡")]
        [StringLength(200)]
        public string FINAL_DISPOSER { get; set; }
        [Display(Name = "�o�󪫥N�X")]
        [StringLength(200)]
        public string SCRAP_CODE { get; set; }
        [Display(Name = "�o�󪫦W��")]
        [StringLength(200)]
        public string SCRAP_NAME { get; set; }
        [Display(Name = "�ӳ����q(����)")]
        public decimal? DECLARED_WEIGHT { get; set; }
        [Display(Name = "�B�餽��")]
        public decimal? KILOMETERS { get; set; }
        [Display(Name = "���ʼƾ�")]
        public decimal? ACTIVITY_DATA { get; set; }
        [Display(Name = "�Ʃ�Y��")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "�G��ƺҷ�q")]
        public decimal? CARBON_DIOXIDE { get; set; }

        public DateTime? data { get; set; }

        public string CreatedBy { get; set; }

        [Display(Name = "�����Ӥ�:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        [Display(Name = "�����Ӥ�:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
    }
}
//HomeController_20231226�����n���ɶ�
namespace prjWastes6.Models.Extensions
{

    public partial class WASTES
    {
        public DateTime? CreatedTime { get; set; }
    }
    

}
