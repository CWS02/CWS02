namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CombinedViewModel
    {

        [Display(Name = "員工編號")]
        public string USER_ID { get; set; }
        [Display(Name = "姓名")]
        public string USER_NAME { get; set; }
        [Display(Name = "到職日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public string FIRST_WORK_DATE { get; set; }
        [Display(Name = "離職日")]

        public string LAST_WORK_DATE { get; set; }

        [Display(Name = "狀態")]
        public string Status { get; set; }


        [Display(Name = "住址")]
        public string USER_ADDRESS { get; set; }
        [Display(Name = "課別部門")]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name = "職稱")]
        public string TITLE_NAME { get; set; }
        [Display(Name = "WORK_DATE")]
        public int WORK_DATE { get; set; }
     

        [Display(Name = "出勤天數")]
        public int WORK_DATE_COUNT { get; set; }
        [Display(Name = "班別")]
        public string SHIFT_NAME { get; set; }
        public string CLOCK_IN { get; set; }
        public string CLOCK_OUT { get; set; }
        public string WORKING_HOURS { get; set; }
        public string UNIT { get; set; }

        public DateTime? USEDT { get; set; }  // Change to DateTime?
        [Display(Name = "交通工具")]
        public string TRANSPORTATION { get; set; }
        [Display(Name = "單程公里數")]
        public decimal? ONEWAY_KM { get; set; }
        [Display(Name = "來回公里數")]
        public decimal? DOUBLE_KM { get; set; }



        public int AttendanceDays { get; set; }


        [Display(Name = "活動數據")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal ActivityData { get; set; }
        [Display(Name = "碳排係數")]

        [DisplayFormat(DataFormatString = "{0:0.000}")]


        public decimal EmissionFactor { get; set; }


        [Display(Name = "碳排放量")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Emissions { get; set; }

        [Display(Name = "CO2e(公噸)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal CO2e { get; set; }

        [Display(Name = "年份")]
        public int Year { get; set; }

        
    }
}