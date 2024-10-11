namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GHG_MST_COMMUTE")]
    public partial class GHG_MST_COMMUTE
    {
        [Column(TypeName = "datetime2")]
        public DateTime USEDT { get; set; }

        [Display(Name = "���u�s��")]

        [Key]
        [Required]
        [StringLength(200)]
        public string USER_ID { get; set; }


        [Display(Name = "�m�W")]
        [Required]
        [StringLength(200)]
        public string USER_NAME { get; set; }

        [Display(Name = "��q�u��")]
        [Required]
        [StringLength(200)]
        public string TRANSPORTATION { get; set; }

        [Display(Name = "��{������")]
        [Required]
       
        public decimal? ONEWAY_KM { get; set; }

        [Display(Name = "�Ӧ^������")]
        //public decimal? DOUBLE_KM { get; set; }
        public decimal? DOUBLE_KM
        {
            get { return ONEWAY_KM * 2; }
            set { ONEWAY_KM = value / 2; }
        }

        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }
    }
}
