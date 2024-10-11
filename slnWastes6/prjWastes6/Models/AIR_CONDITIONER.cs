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

    [Table("AIR_CONDITIONER")]
    public partial class AIR_CONDITIONER
    {
        

        [Display(Name = "���x���X")]
        [Key]
        [StringLength(200)]
        public string MACHINE_NUMBER { get; set; }


        [Display(Name = "���I�W��")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }

        [Display(Name = "�Žվ�����")]
        [StringLength(200)]
        public string AIR_CONDITIONER_TYPE { get; set; }
        [Display(Name = "�ƶq")]
        public int? QUANTITY { get; set; }

        

     
        [Display(Name = "�`�B��ɼ�")]
        public decimal? TOTAL_HOURS { get; set; }

        [Display(Name = "�N�C����")]
        [StringLength(200)]
        public string REFRIGERANT { get; set; }

        [Display(Name = "�Ƶ�")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "���Ҹ��:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        

        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

       
    }

}

