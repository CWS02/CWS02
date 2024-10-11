namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAT_TWO_TAIPOWER
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string BOUND_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TAIPOWER_NO { get; set; }

        [StringLength(50)]
        public string CATEGORY_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string REPORT_YEAR { get; set; }

        [StringLength(10)]
        public string REPORT_MONTH { get; set; }

        [StringLength(50)]
        public string BOUND_NAME { get; set; }

        [StringLength(10)]
        public string USAGE_DAYS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USAGE_START_DAY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USAGE_END_DAY { get; set; }

        public int? CARBON { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TOTAL_ENERGY { get; set; }

        [StringLength(100)]
        public string FILE_PATH { get; set; }

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
