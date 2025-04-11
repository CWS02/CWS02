namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CombinedViewModel
    {

        [Display(Name = "���u�s��")]
        public string USER_ID { get; set; }
        [Display(Name = "�m�W")]
        public string USER_NAME { get; set; }
        [Display(Name = "��¾��")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public string FIRST_WORK_DATE { get; set; }
        [Display(Name = "��¾��")]

        public string LAST_WORK_DATE { get; set; }

        [Display(Name = "���A")]
        public string Status { get; set; }


        [Display(Name = "��}")]
        public string USER_ADDRESS { get; set; }
        [Display(Name = "�ҧO����")]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name = "¾��")]
        public string TITLE_NAME { get; set; }
        [Display(Name = "WORK_DATE")]
        public int WORK_DATE { get; set; }
     

        [Display(Name = "�X�ԤѼ�")]
        public int WORK_DATE_COUNT { get; set; }
        [Display(Name = "�Z�O")]
        public string SHIFT_NAME { get; set; }
        public string CLOCK_IN { get; set; }
        public string CLOCK_OUT { get; set; }
        public string WORKING_HOURS { get; set; }
        public string UNIT { get; set; }

        public DateTime? USEDT { get; set; }  // Change to DateTime?
        [Display(Name = "��q�u��")]
        public string TRANSPORTATION { get; set; }
        [Display(Name = "��{������")]
        public decimal? ONEWAY_KM { get; set; }
        [Display(Name = "�Ӧ^������")]
        public decimal? DOUBLE_KM { get; set; }



        public int AttendanceDays { get; set; }


        [Display(Name = "���ʼƾ�")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal ActivityData { get; set; }
        [Display(Name = "�ұƫY��")]

        [DisplayFormat(DataFormatString = "{0:0.000}")]


        public decimal EmissionFactor { get; set; }


        [Display(Name = "�ұƩ�q")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Emissions { get; set; }

        [Display(Name = "CO2e(����)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal CO2e { get; set; }

        [Display(Name = "�~��")]
        public int Year { get; set; }

        
    }
}