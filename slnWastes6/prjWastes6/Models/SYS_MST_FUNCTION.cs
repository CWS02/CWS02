namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_MST_FUNCTION
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SYSTEM_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string FUNCTION_ID { get; set; }

        [StringLength(50)]
        public string FUNCTION_NAME { get; set; }

        [StringLength(100)]
        public string FUNCTION_URL { get; set; }

        [StringLength(50)]
        public string MODULE_ID { get; set; }

        [StringLength(6)]
        public string LEVEL_ID { get; set; }

        [StringLength(50)]
        public string ICON_CLASS { get; set; }

        public int? SEQ { get; set; }

        [StringLength(200)]
        public string DESCRIPTIONS { get; set; }

        [StringLength(1)]
        public string ENABLETO { get; set; }

        [StringLength(5)]
        public string AID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ADT { get; set; }

        [StringLength(5)]
        public string USEID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USEDT { get; set; }
    }
}
