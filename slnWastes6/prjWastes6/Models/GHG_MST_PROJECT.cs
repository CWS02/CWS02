namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GHG_MST_PROJECT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SYSTEM_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string BOUND_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string REPORT_YEAR { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string PROJECT_TYPE { get; set; }

        [StringLength(50)]
        public string PROJECT_NAME { get; set; }

        [StringLength(50)]
        public string BOUND_NAME { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USAGE_START_DAY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USAGE_END_DAY { get; set; }

        [StringLength(10)]
        public string BASE_YEAR { get; set; }

        [StringLength(50)]
        public string GWP_VALUE { get; set; }

        [StringLength(50)]
        public string CATEGORY_ID { get; set; }

        [StringLength(10)]
        public string AID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ADT { get; set; }

        [StringLength(10)]
        public string USEID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USEDT { get; set; }
    }
}
