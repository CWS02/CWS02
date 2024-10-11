namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FireExtin")]
    public partial class FireExtin
    {
        [Display(Name = "�������W��")]
        [StringLength(50)]
        public string FE001 { get; set; }


        [Display(Name = "�ƶq")]
        [Column(TypeName = "numeric")]
        public decimal? FE002 { get; set; }

        [Display(Name = "��m")]
        [StringLength(10)]
        public string FE003 { get; set; }

        [Display(Name = "�t�Ϧ�m")]
        [StringLength(50)]
        public string FE004 { get; set; }

        [Display(Name = "���t��")]
        [StringLength(50)]
        public string FE005 { get; set; }

        [Display(Name = "��l��R�q(kg/�~)")]
        [Column(TypeName = "numeric")]
        public decimal? FE006 { get; set; }

        [Display(Name = "�޲z����")]
        [StringLength(10)]
        public string FE007 { get; set; }

        
        [StringLength(10)]
        public string FE008 { get; set; }

        [Display(Name = "�ۤ��s��")]
        [StringLength(30)]
        public string FE009 { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal FE000 { get; set; }

        [Display(Name = "�t��")]
        [StringLength(50)]
        public string FE010 { get; set; }
    }
}
