namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AJSTA")]
    public partial class AJSTA
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
        [Column(Order = 0)]
        [StringLength(11)]
        public string TA001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string TA002 { get; set; }

        [StringLength(5)]
        public string TA003 { get; set; }

        [StringLength(4)]
        public string TA004 { get; set; }

        [StringLength(11)]
        public string TA005 { get; set; }

        [StringLength(8)]
        public string TA006 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA007 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA009 { get; set; }

        [StringLength(255)]
        public string TA010 { get; set; }

        [StringLength(10)]
        public string TA011 { get; set; }

        [StringLength(8)]
        public string TA012 { get; set; }

        [StringLength(5)]
        public string TA013 { get; set; }

        [StringLength(1)]
        public string TA014 { get; set; }

        [StringLength(2)]
        public string TA015 { get; set; }

        [StringLength(1)]
        public string TA016 { get; set; }

        [StringLength(6)]
        public string TA017 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA018 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA019 { get; set; }

        [StringLength(1)]
        public string TA020 { get; set; }

        [StringLength(30)]
        public string TA021 { get; set; }

        [StringLength(60)]
        public string TA022 { get; set; }

        [StringLength(10)]
        public string TA023 { get; set; }

        [StringLength(20)]
        public string TA024 { get; set; }

        [StringLength(6)]
        public string TA025 { get; set; }

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
