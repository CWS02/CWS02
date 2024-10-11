namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GHG_MST_BOUND
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
        [StringLength(50)]
        public string BOUND_NAME { get; set; }

        [StringLength(50)]
        public string REGULATORY_NO { get; set; }

        [StringLength(50)]
        public string UNIFORM_NO { get; set; }

        [StringLength(50)]
        public string CONTACTS { get; set; }

        [StringLength(100)]
        public string BOUND_ADDRESS { get; set; }

        [StringLength(255)]
        public string DESCRIPTIONS { get; set; }

        [StringLength(1)]
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
