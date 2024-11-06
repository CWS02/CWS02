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

        [MaxLength(20)]
        public string PAR001 { get; set; } // 項目

        [MaxLength(30)]
        public string PAR002 { get; set; } // 細數

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
}