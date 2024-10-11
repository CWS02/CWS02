namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PURMA")]
    public partial class PURMA
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
        [StringLength(10)]
        public string MA001 { get; set; }

        [StringLength(30)]
        public string MA002 { get; set; }

        [StringLength(80)]
        public string MA003 { get; set; }

        [StringLength(6)]
        public string MA004 { get; set; }

        [StringLength(20)]
        public string MA005 { get; set; }

        [StringLength(6)]
        public string MA006 { get; set; }

        [StringLength(6)]
        public string MA007 { get; set; }

        [StringLength(20)]
        public string MA008 { get; set; }

        [StringLength(20)]
        public string MA009 { get; set; }

        [StringLength(20)]
        public string MA010 { get; set; }

        [StringLength(60)]
        public string MA011 { get; set; }

        [StringLength(30)]
        public string MA012 { get; set; }

        [StringLength(30)]
        public string MA013 { get; set; }

        [StringLength(255)]
        public string MA014 { get; set; }

        [StringLength(255)]
        public string MA015 { get; set; }

        [StringLength(1)]
        public string MA016 { get; set; }

        [StringLength(8)]
        public string MA017 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA018 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA019 { get; set; }

        [StringLength(1)]
        public string MA020 { get; set; }

        [StringLength(4)]
        public string MA021 { get; set; }

        [StringLength(8)]
        public string MA022 { get; set; }

        [StringLength(8)]
        public string MA023 { get; set; }

        [StringLength(1)]
        public string MA024 { get; set; }

        [StringLength(16)]
        public string MA025 { get; set; }

        [StringLength(40)]
        public string MA026 { get; set; }

        [StringLength(20)]
        public string MA027 { get; set; }

        [StringLength(30)]
        public string MA028 { get; set; }

        [StringLength(1)]
        public string MA029 { get; set; }

        [StringLength(1)]
        public string MA030 { get; set; }

        [StringLength(1)]
        public string MA031 { get; set; }

        [StringLength(1)]
        public string MA032 { get; set; }

        [StringLength(1)]
        public string MA033 { get; set; }

        [StringLength(1)]
        public string MA034 { get; set; }

        [StringLength(2)]
        public string MA035 { get; set; }

        [StringLength(1)]
        public string MA036 { get; set; }

        [StringLength(2)]
        public string MA037 { get; set; }

        [StringLength(1)]
        public string MA038 { get; set; }

        [StringLength(2)]
        public string MA039 { get; set; }

        [StringLength(255)]
        public string MA040 { get; set; }

        [StringLength(20)]
        public string MA041 { get; set; }

        [StringLength(20)]
        public string MA042 { get; set; }

        [StringLength(20)]
        public string MA043 { get; set; }

        [StringLength(1)]
        public string MA044 { get; set; }

        [StringLength(1)]
        public string MA045 { get; set; }

        [StringLength(6)]
        public string MA046 { get; set; }

        [StringLength(10)]
        public string MA047 { get; set; }

        [StringLength(30)]
        public string MA048 { get; set; }

        [StringLength(30)]
        public string MA049 { get; set; }

        [StringLength(6)]
        public string MA050 { get; set; }

        [StringLength(255)]
        public string MA051 { get; set; }

        [StringLength(255)]
        public string MA052 { get; set; }

        [StringLength(1)]
        public string MA053 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA054 { get; set; }

        [StringLength(6)]
        public string MA055 { get; set; }

        [StringLength(1)]
        public string MA056 { get; set; }

        [StringLength(1)]
        public string MA057 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA058 { get; set; }

        [StringLength(20)]
        public string MA059 { get; set; }

        [StringLength(1)]
        public string MA060 { get; set; }

        [StringLength(1)]
        public string MA061 { get; set; }

        [StringLength(1)]
        public string MA062 { get; set; }

        [StringLength(10)]
        public string MA063 { get; set; }

        [StringLength(4)]
        public string MA064 { get; set; }

        [StringLength(6)]
        public string MA065 { get; set; }

        [StringLength(255)]
        public string MA066 { get; set; }

        [StringLength(10)]
        public string MA067 { get; set; }

        [StringLength(1)]
        public string MA068 { get; set; }

        [StringLength(4)]
        public string MA069 { get; set; }

        [StringLength(8)]
        public string MA070 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA071 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA072 { get; set; }

        [StringLength(1)]
        public string MA073 { get; set; }

        [StringLength(30)]
        public string MA074 { get; set; }

        [StringLength(60)]
        public string MA075 { get; set; }

        [StringLength(3)]
        public string MA076 { get; set; }

        [StringLength(1)]
        public string MA077 { get; set; }

        [StringLength(1)]
        public string MA078 { get; set; }

        [StringLength(255)]
        public string MA079 { get; set; }

        [StringLength(10)]
        public string MA080 { get; set; }

        [StringLength(10)]
        public string MA081 { get; set; }

        [StringLength(1)]
        public string MA082 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA083 { get; set; }

        [StringLength(1)]
        public string MA084 { get; set; }

        [StringLength(20)]
        public string MA085 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA086 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA087 { get; set; }

        [StringLength(255)]
        public string MA088 { get; set; }

        [StringLength(20)]
        public string MA089 { get; set; }

        [StringLength(8)]
        public string MA090 { get; set; }

        [StringLength(8)]
        public string MA091 { get; set; }

        [StringLength(8)]
        public string MA092 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA093 { get; set; }

        [StringLength(1)]
        public string MA094 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MA095 { get; set; }

        [StringLength(1)]
        public string MA096 { get; set; }

        [StringLength(255)]
        public string MA097 { get; set; }

        [StringLength(1)]
        public string MA098 { get; set; }

        [StringLength(255)]
        public string MA099 { get; set; }

        [StringLength(30)]
        public string MA100 { get; set; }

        [StringLength(1)]
        public string MA101 { get; set; }

        [StringLength(1)]
        public string MA102 { get; set; }

        [StringLength(10)]
        public string MA103 { get; set; }

        [StringLength(30)]
        public string MA104 { get; set; }

        [StringLength(30)]
        public string MA105 { get; set; }

        [StringLength(60)]
        public string MA106 { get; set; }

        [StringLength(255)]
        public string MA107 { get; set; }

        [StringLength(5)]
        public string MA108 { get; set; }

        [StringLength(10)]
        public string MA109 { get; set; }

        [StringLength(255)]
        public string MA110 { get; set; }

        [StringLength(4)]
        public string MA111 { get; set; }

        [StringLength(1)]
        public string MA112 { get; set; }

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
