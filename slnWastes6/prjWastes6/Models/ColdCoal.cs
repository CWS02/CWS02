namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ColdCoal")]
    public partial class ColdCoal
    {
        [Display(Name = "�s��")]
        [StringLength(30)]
        public string CC001 { get; set; }

        [Display(Name = "���x�W��")]
        [StringLength(255)]
        public string CC002 { get; set; }

        [Display(Name = "�����N��/�O�ޤH")]
        [StringLength(20)]
        public string CC003 { get; set; }
        [Display(Name = "�t�Ϧ�m")]
        [StringLength(20)]
        public string CC004 { get; set; }
        [Display(Name = "�N�o���x�W��")]
        [StringLength(50)]
        public string CC005 { get; set; }
        [Display(Name = "�N�o���x�s��")]
        [StringLength(30)]
        public string CC006 { get; set; }
        [Display(Name = "�N�C����")]
        [StringLength(30)]
        public string CC007 { get; set; }
        [Display(Name = "��l��R�q(����)")]
        [Column(TypeName = "numeric")]
        public decimal? CC008 { get; set; }
        [Display(Name = "�ۤ��s��")]
        [StringLength(40)]
        public string CC009 { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CC000 { get; set; }

        [Display(Name = "�t��")]
        [StringLength(20)]
        public string CC010 { get; set; }
    }
}
