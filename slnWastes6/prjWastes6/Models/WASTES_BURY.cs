namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WASTES_BURY
    {
        [Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime REMOVAL_DATE { get; set; }

        [Required]
        [StringLength(200)]
        public string CLEANER_CODE { get; set; }

        [Required]
        [StringLength(200)]
        public string PURGE_NAME { get; set; }

        [StringLength(200)]
        public string NUMBER_CARRIER { get; set; }

        [StringLength(200)]
        public string TREATMENT { get; set; }

        [StringLength(200)]
        public string FINAL_DISPOSER_CODE { get; set; }

        [StringLength(200)]
        public string FINAL_DISPOSER_NAME { get; set; }

        [StringLength(200)]
        public string SCRAP_CODE { get; set; }

        [StringLength(200)]
        public string SCRAP_NAME { get; set; }

        public decimal? DECLARED_WEIGHT { get; set; }

        public decimal? KILOMETERS { get; set; }

        public decimal? ACTIVITY_DATA { get; set; }

        public decimal? CARBON_EMISSION_FACTOR { get; set; }

        public decimal? CARBON_DIOXIDE { get; set; }

        public DateTime? data { get; set; }
    }
}
