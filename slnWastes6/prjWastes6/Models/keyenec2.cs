using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjWastes6.Models
{
    [Table("keyenec2")]
    public partial class keyenec2
    {
        [Key, Column(Order = 0)]
        [Display(Name = "KEY01")]
        [StringLength(10)]
        public string Key01 { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "KEY02")]
        [StringLength(10)]
        public string Key02 { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "KEY03")]
        [StringLength(10)]
        public string Key03 { get; set; }

        [Key, Column(Order = 3)]
        [Display(Name = "KEY04")]
        [StringLength(10)]
        public string Key04 { get; set; }

        [Display(Name = "KEY11")]
        public decimal? Key11 { get; set; }

        [Display(Name = "KEY12")]
        public decimal? Key12 { get; set; }

        [Display(Name = "KEY13")]
        public decimal? Key13 { get; set; }

        [Display(Name = "KEY14")]
        public decimal? Key14 { get; set; }

        [Display(Name = "KEY15")]
        public decimal? Key15 { get; set; }

        [Display(Name = "KEY16")]
        public decimal? Key16 { get; set; }

        [Display(Name = "KEY17")]
        public decimal? Key17 { get; set; }

        [Display(Name = "KEY18")]
        public decimal? Key18 { get; set; }

        [Display(Name = "KEY19")]
        public decimal? Key19 { get; set; }

        [Display(Name = "KEY20")]
        public decimal? Key20 { get; set; }

        [Display(Name = "KEY21")]
        public decimal? Key21 { get; set; }

        [Display(Name = "KEY22")]
        public decimal? Key22 { get; set; }

        [Display(Name = "KEY23")]
        public decimal? Key23 { get; set; }

        [Display(Name = "KEY24")]
        public decimal? Key24 { get; set; }
    }
}