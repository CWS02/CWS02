using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjWastes6.Models
{
    [Table("INTRB")]
    public class INTRB
    {
        /// <summary>
        /// 隨機碼 (主鍵)
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }

        /// <summary>
        /// 主題
        /// </summary>
        [Required]
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// 訪談記錄別
        /// 1=到訪, 2=電話, 3=通信
        /// </summary>
        public int INT002 { get; set; }

        /// <summary>
        /// 檔案網址
        /// </summary>
        [StringLength(150)]
        public string INT003 { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(36)]
        public string INT999 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// 狀態
        /// 0=啟用, 1=刪除
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 建立時間 (自動帶入)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }
}