using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjWastes6.Models
{
    [Table("INTRA")]
    public class INTRA
    {
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// 型態別
        /// </summary>
        [StringLength(2)]
        public string INT002 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(50)]
        public string INT003 { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [StringLength(20)]
        public string INT004 { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [StringLength(20)]
        public string INT005 { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [StringLength(50)]
        public string INT006 { get; set; }
        /// <summary>
        /// 區域
        /// </summary>
        [StringLength(50)]
        public string INT007 { get; set; }
        /// <summary>
        /// 訪談時間
        /// </summary>
        [StringLength(50)]
        public string INT008 { get; set; }

        /// <summary>
        /// 接洽人1
        /// </summary>
        [StringLength(50)]
        public string INT009 { get; set; }

        /// <summary>
        /// 職稱1
        /// </summary>
        [StringLength(50)]
        public string INT010 { get; set; }

        /// <summary>
        /// 分機1
        /// </summary>
        [StringLength(50)]
        public string INT011 { get; set; }

        /// <summary>
        /// 接洽人2
        /// </summary>
        [StringLength(50)]
        public string INT012 { get; set; }

        /// <summary>
        /// 職稱2
        /// </summary>
        [StringLength(50)]
        public string INT013 { get; set; }

        /// <summary>
        /// 分機2
        /// </summary>
        [StringLength(50)]
        public string INT014 { get; set; }

        /// <summary>
        /// 接洽人3
        /// </summary>
        [StringLength(50)]
        public string INT015 { get; set; }

        /// <summary>
        /// 職稱3
        /// </summary>
        [StringLength(50)]
        public string INT016 { get; set; }

        /// <summary>
        /// 分機3
        /// </summary>
        [StringLength(50)]
        public string INT017 { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string INT018 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// 狀態 0=啟用 1=刪除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間 (自動帶入)
        /// </summary>
        [NotMapped]
        public DateTime CreateTime { get; set; }
    }
}