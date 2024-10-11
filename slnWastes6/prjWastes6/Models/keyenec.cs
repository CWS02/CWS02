using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjWastes6.Models
{
    [Table("keyenec")]
    public partial class keyenec
    {
        [Key, Column(Order = 0)]
        [Display(Name = "年份")]
        [Required(ErrorMessage = "請填寫年份")]
        [StringLength(4)]
        public string Year { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "月份")]
        [Required(ErrorMessage = "請填寫月份")]
        [StringLength(2)]
        public string Month { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "日期")]
        [Required(ErrorMessage = "請填寫日期")]
        [StringLength(2)]
        public string Day { get; set; }

        [Key, Column(Order = 3)]
        [Display(Name = "小時")]
        [Required(ErrorMessage = "請填寫小時")]
        [StringLength(2)]
        public string Hour { get; set; }

        [Display(Name = "總量")]
        [Column(TypeName = "decimal(15, 3)")]
        public decimal? total_amount { get; set; }

        [Display(Name = "洩漏量")]
        [Column(TypeName = "decimal(15, 3)")]
        public decimal? Leakage { get; set; }
    }
}