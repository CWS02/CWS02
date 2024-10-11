namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    //20240126´ú¸Õ¶ñ­^¤åª©
    [Table("enus_GHG_MST_COMMUTE")]
    public partial class enus_GHG_MST_COMMUTE
    {
        [Column(TypeName = "datetime2")]
        public DateTime USEDT { get; set; }

  

        [Key]
        [Required]
        [StringLength(200)]
        public string USER_ID { get; set; }



        [Required]
        [StringLength(200)]
        public string USER_NAME { get; set; }

       
        [Required]
        [StringLength(200)]
        public string TRANSPORTATION { get; set; }

     
        [Required]
       
        public decimal? ONEWAY_KM { get; set; }

     
        //public decimal? DOUBLE_KM { get; set; }
        public decimal? DOUBLE_KM
        {
            get { return ONEWAY_KM * 2; }
            set { ONEWAY_KM = value / 2; }
        }

       
        public DateTime? data { get; set; }
      
        public string CreatedBy { get; set; }
    }
}
