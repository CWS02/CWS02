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
        

        [Display(Name = "機台號碼")]
        [Key]
        [StringLength(200)]
        public string MACHINE_NUMBER { get; set; }


        [Display(Name = "據點名稱")]
        [StringLength(200)]
        public string BASE_NAME { get; set; }

        [Display(Name = "空調機類型")]
        [StringLength(200)]
        public string AIR_CONDITIONER_TYPE { get; set; }
        [Display(Name = "數量")]
        public int? QUANTITY { get; set; }

        

     
        [Display(Name = "總運轉時數")]
        public decimal? TOTAL_HOURS { get; set; }

        [Display(Name = "冷媒種類")]
        [StringLength(200)]
        public string REFRIGERANT { get; set; }

        [Display(Name = "備註")]
        [StringLength(200)]
        public string REMARK { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "佐證資料:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        

        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

       
    }

}

