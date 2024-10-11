using System;

namespace prjWastes6.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CombinedHighSpeedRailViewModel
    {
        public string UDF01 { get; set; }
        public string CREATE_DATE { get; set; }
        public string TA015 { get; set; }
        public string UDF02 { get; set; }
        public string UDF03 { get; set; }
        public decimal? UDF06 { get; set; }
        public string TB011 { get; set; }
        public string TB001 { get; set; }
        public string TB002 { get; set; }
        public string TB003 { get; set; }
        public string TA004 { get; set; }
        public int Source { get; set; } // 1 for HIGH_SPEED_RAIL_List, 2 for HIGH_SPEED_RAIL_List2
        public string TF015 { get; set; }

        public string TF016 { get; set; }


        // ¨ä¥LÄÝ©Ê...
        public DateTime? StartDocDate { get; set; }
        public DateTime? EndDocDate { get; set; }
        [Required]
        public string DateFilter { get; set; }
    }
}