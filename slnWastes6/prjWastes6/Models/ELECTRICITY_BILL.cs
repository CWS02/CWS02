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

    [Table("ELECTRICITY_BILL")]
    public partial class ELECTRICITY_BILL
    {
        [Display(Name = "�q���渹�X")]
        [Required(ErrorMessage = "����")]
        [Key]      
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
    

        

        [Display(Name = "�t��")]
        [StringLength(200)]
        public string FACTORY { get; set; }
        [Display(Name = "�q��")]
        [StringLength(200)]
        public string ELECTRICITY_SIGNAL { get; set; }
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

        [Display(Name = "�y�p�ιq�׼�")]
        public decimal? PEAK_ELECTRICITY { get; set; }

        public string FormattedPEAK_ELECTRICITY
        {
            get
            {
                return PEAK_ELECTRICITY?.ToString("#,##0");
                //return PEAK_ELECTRICITY?.ToString("0");
            }
        }

        [Display(Name = "�b�y�p�ιq�׼�")]
        public decimal? HALF_SPIKE_POWER { get; set; }

        public string FormattedHALF_SPIKE_POWER
        {
            get
            {
                return HALF_SPIKE_POWER?.ToString("#,##0");
                //return HALF_SPIKE_POWER?.ToString("0");
            }
        }

        [Display(Name = "�g���b�y�p�ιq�׼�")]
        public decimal? SATURDAY_HALF_PEAK { get; set; }

        public string FormattedSATURDAY_HALF_PEAK
        {
            get
            {
                return SATURDAY_HALF_PEAK?.ToString("#,##0");
                //return SATURDAY_HALF_PEAK?.ToString("0");
            }
        }

        [Display(Name = "���p�ιq�׼�")]
        public decimal? OFF_PEAK_ELECTRICITY { get; set; }
        public string FormattedOFF_PEAK_ELECTRICITY
        {
            get
            {
                return OFF_PEAK_ELECTRICITY?.ToString("#,##0");
                //return OFF_PEAK_ELECTRICITY?.ToString("0");
            }
        }

        [Display(Name = "�`�ιq��")]
        public decimal? TOTAL_ELECTRICITY { get; set; }
        public string FormattedTOTAL_ELECTRICITY
        {
            get
            {
                return TOTAL_ELECTRICITY?.ToString("#,##0");
                //return TOTAL_ELECTRICITY?.ToString("0");
            }
        }
        [Display(Name = "�`�q�O(�t�|)")]
        public decimal? TOTAL_BILL_TAX { get; set; }

        public string FormattedTOTAL_BILL_TAX
        {
            get
            {
                //return TOTAL_BILL_TAX?.ToString("#,##0");
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

        [Display(Name = "�q�Oú�O�q����:�W��(�̤j2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        [Display(Name = "�q�Oú�O�q����:�W��(�̤j2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "�ǲ����X")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

    }

}

