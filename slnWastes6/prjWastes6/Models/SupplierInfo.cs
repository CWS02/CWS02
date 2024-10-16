using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjWastes6.Models
{
    [Table("SupplierInfo")]
    public class SupplierInfo
    {
        [Key]
        //唯一值
        public string SUP000 { get; set; }
        //代碼
        public string SUP001 { get; set; }  
        //廠商
        public  string SUP002 { get; set; } 
        //建立日期
        public string SUP003 { get; set; }
        //基本資料
        public short SUP004 { get; set; }
        //承諾書
        public short SUP005 { get; set; }    
        //保密協定
        public short SUP006 { get; set; }   
        //營登
        public short SUP007 { get; set; }    
        //匯款正本
        public short SUP008 { get; set; }   
        //社會責任
        public short SUP009 { get; set; }   
        //Reach
        public short SUP010 { get; set; }
        //Reach種數
        public string SUP011 { get; set; }
        //Rohs
        public short SUP012 { get; set; }
        //無礦產
        public short SUP013 { get; set; }
        //不使用物質
        public short SUP014 { get; set; }
        //USEPA
        public short SUP015 { get; set; }
        //檢驗記錄
        public short SUP016 { get; set; }
        //委外評鑑
        public short SUP017 { get; set; }
        //人員組織表
        public short SUP018 { get; set; }
        //職安衛要求
        public short SUP019 { get; set; }
        //填入Reach總表
        public string SUP020 { get; set; }
        //簽核
        public short SUP021 { get; set; }
        //完成  歸檔
        public short SUP022 { get; set; }    
        //使用者IP
        public string SUP023 { get; set; }
        //建立時間
        public string SUP024 { get; set; }
        //是否刪除
        public short SUP025 { get; set; }
        //環境評估
        public short SUP101 { get; set; }
        //汙染許可證
        public short SUP102 { get; set; }
        //廢棄物
        public short SUP103 { get; set; }
        //噪音
        public short SUP104 { get; set; }    
        //飲用水
        public short SUP105 { get; set; }    
        //消防
        public short SUP106 { get; set; } 
        //能源
        public short SUP107 { get; set; }   
        //緊急應變
        public short SUP108 { get; set; }
        //ISO 9001
        public string SUP201 { get; set; }
        //OHSAS 18001 2007
        public string SUP202 { get; set; }
        //ISO 45001 2018
        public string SUP203 { get; set; }
        //ISO 14001 2015
        public string SUP204 { get; set; }
        //ISO SA 8000
        public string SUP205 { get; set; } 
        //FSC
        public string SUP206 { get; set; }
        //ISO TS16949
        public string SUP207 { get; set; }
        //QC080000
        public string SUP208 { get; set; }
        //IATF 16949:2016
        public string SUP209 { get; set; }
        //ISO 50001:2011
        public string SUP210 { get; set; } 
    }
}