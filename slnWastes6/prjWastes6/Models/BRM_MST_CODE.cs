namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BRM_MST_CODE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SYSTEM_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CODE_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string GROUP_ID { get; set; }

        [StringLength(100)]
        public string CODE_NAME { get; set; }

        [StringLength(100)]
        public string GROUP_NAME { get; set; }

        public int? SEQ { get; set; }

        [StringLength(255)]
        public string DESCRIPTIONS { get; set; }

        [StringLength(50)]
        public string ENABLETO { get; set; }

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
