namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CMSMF")]
    public partial class CMSMF
    {
        [StringLength(20)]
        public string COMPANY { get; set; }

        [StringLength(10)]
        public string CREATOR { get; set; }

        [StringLength(10)]
        public string USR_GROUP { get; set; }

        [StringLength(8)]
        public string CREATE_DATE { get; set; }

        [StringLength(10)]
        public string MODIFIER { get; set; }

        [StringLength(8)]
        public string MODI_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FLAG { get; set; }

        [StringLength(20)]
        public string CREATE_TIME { get; set; }

        [StringLength(50)]
        public string CREATE_AP { get; set; }

        [StringLength(50)]
        public string CREATE_PRID { get; set; }

        [StringLength(20)]
        public string MODI_TIME { get; set; }

        [StringLength(50)]
        public string MODI_AP { get; set; }

        [StringLength(50)]
        public string MODI_PRID { get; set; }

        [Key]
        [StringLength(4)]
        public string MF001 { get; set; }

        [StringLength(40)]
        public string MF002 { get; set; }

        [StringLength(1)]
        public string MF003 { get; set; }

        [StringLength(1)]
        public string MF004 { get; set; }

        [StringLength(1)]
        public string MF005 { get; set; }

        [StringLength(1)]
        public string MF006 { get; set; }

        [StringLength(255)]
        public string MF007 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MF008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MF009 { get; set; }

        [StringLength(1)]
        public string MF010 { get; set; }

        [StringLength(30)]
        public string MF011 { get; set; }

        [StringLength(60)]
        public string MF012 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MF013 { get; set; }

        [StringLength(1)]
        public string MF014 { get; set; }

        [StringLength(255)]
        public string UDF01 { get; set; }

        [StringLength(255)]
        public string UDF02 { get; set; }

        [StringLength(255)]
        public string UDF03 { get; set; }

        [StringLength(255)]
        public string UDF04 { get; set; }

        [StringLength(255)]
        public string UDF05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF10 { get; set; }
    }
}
