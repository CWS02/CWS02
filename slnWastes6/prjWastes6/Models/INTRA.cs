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
        /// �Ȥ�W��
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }
        /// <summary>
        /// �Ȥ�W��
        /// </summary>
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// ���A�O
        /// </summary>
        [StringLength(2)]
        public string INT002 { get; set; }

        /// <summary>
        /// �a�}
        /// </summary>
        [StringLength(50)]
        public string INT003 { get; set; }

        /// <summary>
        /// �q��
        /// </summary>
        [StringLength(20)]
        public string INT004 { get; set; }

        /// <summary>
        /// �ǯu
        /// </summary>
        [StringLength(20)]
        public string INT005 { get; set; }

        /// <summary>
        /// ��a
        /// </summary>
        [StringLength(50)]
        public string INT006 { get; set; }
        /// <summary>
        /// �ϰ�
        /// </summary>
        [StringLength(50)]
        public string INT007 { get; set; }
        /// <summary>
        /// �X�ͮɶ�
        /// </summary>
        [StringLength(50)]
        public string INT008 { get; set; }

        /// <summary>
        /// �����H1
        /// </summary>
        [StringLength(50)]
        public string INT009 { get; set; }

        /// <summary>
        /// ¾��1
        /// </summary>
        [StringLength(50)]
        public string INT010 { get; set; }

        /// <summary>
        /// ����1
        /// </summary>
        [StringLength(50)]
        public string INT011 { get; set; }

        /// <summary>
        /// �����H2
        /// </summary>
        [StringLength(50)]
        public string INT012 { get; set; }

        /// <summary>
        /// ¾��2
        /// </summary>
        [StringLength(50)]
        public string INT013 { get; set; }

        /// <summary>
        /// ����2
        /// </summary>
        [StringLength(50)]
        public string INT014 { get; set; }

        /// <summary>
        /// �����H3
        /// </summary>
        [StringLength(50)]
        public string INT015 { get; set; }

        /// <summary>
        /// ¾��3
        /// </summary>
        [StringLength(50)]
        public string INT016 { get; set; }

        /// <summary>
        /// ����3
        /// </summary>
        [StringLength(50)]
        public string INT017 { get; set; }

        /// <summary>
        /// ���e
        /// </summary>
        public string INT018 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// ���A 0=�ҥ� 1=�R��
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// �إ߮ɶ� (�۰ʱa�J)
        /// </summary>
        [NotMapped]
        public DateTime CreateTime { get; set; }
    }
}