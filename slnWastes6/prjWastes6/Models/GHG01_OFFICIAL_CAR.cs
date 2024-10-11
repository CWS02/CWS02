namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GHG01_OFFICIAL_CAR
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string SUBPOENA_TYPE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string SUBPOENA_NUMBER { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string SUBPOENA_BRANCH { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SUBPOENA_DATE { get; set; }

        [StringLength(10)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(20)]
        public string DEPARTMENT_NAME { get; set; }

        [StringLength(50)]
        public string INVOICE_NUMBER { get; set; }

        [StringLength(10)]
        public string USER_NAME { get; set; }

        [StringLength(10)]
        public string GHG_NAME { get; set; }

        [StringLength(10)]
        public string OIL_TYPE { get; set; }

        public decimal? ACTIVITY_DATA { get; set; }
    }
}
