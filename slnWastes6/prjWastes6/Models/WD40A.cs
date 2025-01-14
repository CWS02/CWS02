using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjWastes6.Models
{
    [Table("WD40A")]
    public class WD40A
    {
        /// <summary>
        /// 唯一碼 (Primary Key).
        /// </summary>
        [Key]
        public string WD000 { get; set; }

        /// <summary>
        /// 採購單.
        /// </summary>
        public string WD001 { get; set; }

        /// <summary>
        /// 進貨單.
        /// </summary>
        public string WD002 { get; set; }

        /// <summary>
        /// 供應廠.
        /// </summary>
        public string WD003 { get; set; }

        /// <summary>
        /// 日期.
        /// </summary>
        public string WD004 { get; set; }

        /// <summary>
        /// 品號.
        /// </summary>
        public string WD005 { get; set; }

        /// <summary>
        /// 品名.
        /// </summary>
        public string WD006 { get; set; }

        /// <summary>
        /// 已交量.
        /// </summary>
        public string WD007 { get; set; }

        /// <summary>
        /// 交期.
        /// </summary>
        public string WD008 { get; set; }

        /// <summary>
        /// 內容量(克/罐).
        /// </summary>
        public string WD009 { get; set; }

        /// <summary>
        /// 重量(克).
        /// </summary>
        public string WD010 { get; set; }

        /// <summary>
        /// 二氧化碳占比%.
        /// </summary>
        public string WD011 { get; set; }

        /// <summary>
        /// 二氧化碳重量(克).
        /// </summary>
        public string WD012 { get; set; }

        /// <summary>
        /// IP 地址.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 狀態 (0=未審核, 1=刪除).
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間 (自動帶入).
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}