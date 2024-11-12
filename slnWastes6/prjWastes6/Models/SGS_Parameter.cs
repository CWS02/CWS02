using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjWastes6.Models
{
    [Table("SGS_Parameter")]
    public class SGS_Parameter
    {
        [Key]
        [MaxLength(40)]
        public string PAR000 { get; set; } // 隨機碼

        [MaxLength(60)]
        public string PAR001 { get; set; } // 項目

        [MaxLength(30)]
        public string PAR002 { get; set; } // 係數

        [MaxLength(20)]
        public string PAR003 { get; set; } // 單位

        [MaxLength(6)]
        public string PAR004 { get; set; } // 公告年份

        [MaxLength(20)]
        public string PAR005 { get; set; } // IP

        [MaxLength(20)]
        public string PAR006 { get; set; } // 時間
        public short PAR007 { get; set; } // 是否啟用

    }

    public class SGS_Search
    {
        public string category { get; set; }
        public DateTime startdate { get; set; } 
        public DateTime enddate { get; set; }
        public string factory { get; set; } 
        public string waterdiameter { get; set; }
        public string methods { get; set; } 
        public string code { get; set; } 
    }
    public class ElectricitySummaryViewModel
    {
        public string Factory { get; set; }
        public decimal?   SumPeakElectricity { get; set; }
        public decimal? SumHalfSpikePower { get; set; }
        public decimal? SumSaturdayHalfPeak { get; set; }
        public decimal? SumOffPeakElectricity { get; set; }
        public decimal? SumTotalElectricity { get; set; }
        public decimal? SumTotalBillTax { get; set; }
        public decimal? SumCarbonPeriod { get; set; }
    }

    public class WaterSummaryViewModel
    {
        public string Factory { get; set; }
        public decimal SumWaterUsage { get; set; }
        public decimal SumWaterBill { get; set; }
        // 其他水費相關欄位
    }

    public class WasteSummaryViewModel
    {
        public string Method { get; set; }
        public string WasteCode { get; set; }
        public decimal TotalWasteAmount { get; set; }
        public decimal TotalWasteCost { get; set; }
        // 其他廢棄物相關欄位
    }

}