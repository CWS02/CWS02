namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class HighSpeedRailViewModel
    {
        public string UDF01 { get; set; }
        public string CREATE_DATE { get; set; }

        public string TA015 { get; set; }

       
        public string UDF02 { get; set; }
        public string UDF03 { get; set; }

        public string UDF05 { get; set; }
        public decimal? UDF06 { get; set; }

        public decimal? TB009 { get; set; }

        // 20240517金額_試去掉小數點
        [NotMapped]
        public string TB009Formatted => TB009?.ToString("0");
        public string TB011 { get; set; }
        public string TB001 { get; set; }

        public string TB002 { get; set; }

        public string TB003 { get; set; }

        public string TA004 { get; set; }

        public string TF015 { get; set; }
        public string TF016 { get; set; }
    }
}