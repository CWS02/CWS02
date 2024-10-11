using System;

namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Table("WATER_BILL")]
    public partial class WATER_BILL
    {
        [Display(Name = "��ڸ��X")]
        [Required(ErrorMessage = "����")]
        [Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    
        [Display(Name = "�t��")]
        [StringLength(200)]
        public string FACTORY { get; set; }
        [Display(Name = "����")]
        [StringLength(200)]
        public string WATER_SIGNAL { get; set; }

        [Display(Name = "�p�O�����_")]
        [Column(TypeName = "datetime2")]
        public DateTime? FROM_BILLING_PERIOD { get; set; }
        public string FormattedFROM_BILLING_PERIOD
        {
            get
            {
                return FROM_BILLING_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "�p�O������")]
        [Column(TypeName = "datetime2")]
        public DateTime? UNTIL_BILLING_PERIOD { get; set; }
        public string FormattedUNTIL_BILLING_PERIOD
        {
            get
            {
                return UNTIL_BILLING_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "�Τ��اO")]
        [StringLength(200)]
        public string TYPE { get; set; }

        [Display(Name = "�����")]
        [StringLength(200)]
        public string METER_NUMBER { get; set; }
       

        [Display(Name = "����f�|")]
        public int METER_DIAMETER { get; set; }


        [Display(Name = "�������w��")]
        public decimal? NUMBER_POINTERS { get; set; }

        public string FormattedNUMBER_POINTERS
        {
            get
            {
                return NUMBER_POINTERS?.ToString("#,##0");
                //return NUMBER_POINTERS?.ToString("0");
            }
        }

        [Display(Name = "�`�Ϋ׼�")]
        public decimal? TOTAL_WATER { get; set; }
        public string FormattedTOTAL_WATER
        {
            get
            {
                return TOTAL_WATER?.ToString("#,##0");
                //return TOTAL_WATER?.ToString("0");
            }
        }

        [Display(Name = "�`���O(�t�|)")]
        public decimal? TOTAL_BILL_TAX { get; set; }

        public string FormattedTOTAL_BILL_TAX
        {
            get
            {
                return TOTAL_BILL_TAX?.ToString("C0");
                //��ܦ����B�榡
            }
        }

        [Display(Name = "�����ұƶq")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }

        [Display(Name = "�ұƫY��")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "�G��ƺҷ�q")]
        public decimal? CARBON_DIOXIDE { get; set; }
        [Display(Name = "�إ߮ɶ�")]
        public DateTime? data { get; set; }
        [Display(Name = "�إߪ�")]
        public string CreatedBy { get; set; }

        [Display(Name = "���O�q�l�q����:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }
  
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }     
        
        [Display(Name = "���O�q�l�q����:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
    }

}

