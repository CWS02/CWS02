namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCMTF")]
    public partial class PCMTF
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
        [StringLength(4)]
        public string TF001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TF002 { get; set; }

        [StringLength(8)]
        public string TF003 { get; set; }

        [StringLength(8)]
        public string TF004 { get; set; }

        [StringLength(1)]
        public string TF005 { get; set; }

        [StringLength(10)]
        public string TF006 { get; set; }

        [StringLength(10)]
        public string TF007 { get; set; }

        [StringLength(10)]
        public string TF008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TF009 { get; set; }

        [StringLength(20)]
        public string TF010 { get; set; }

        [StringLength(255)]
        public string TF011 { get; set; }

        [StringLength(4)]
        public string TF012 { get; set; }

        [StringLength(11)]
        public string TF013 { get; set; }

        [StringLength(1)]
        public string TF014 { get; set; }

        [StringLength(4)]
        public string TF015 { get; set; }

        [StringLength(11)]
        public string TF016 { get; set; }

        [StringLength(1)]
        public string TF017 { get; set; }

        [StringLength(10)]
        public string TF018 { get; set; }

        [StringLength(1)]
        public string TF019 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TF020 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TF021 { get; set; }

        [StringLength(10)]
        public string TF022 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TF023 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TF024 { get; set; }

        [StringLength(1)]
        public string TF025 { get; set; }

        [StringLength(30)]
        public string TF026 { get; set; }

        [StringLength(60)]
        public string TF027 { get; set; }

        [StringLength(6)]
        public string TF028 { get; set; }

        [StringLength(40)]
        public string TF029 { get; set; }

        [StringLength(60)]
        public string TF030 { get; set; }

        [StringLength(30)]
        public string TF031 { get; set; }

        [StringLength(40)]
        public string TF032 { get; set; }

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
