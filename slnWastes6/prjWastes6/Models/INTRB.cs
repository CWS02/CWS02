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
        /// �H���X (�D��)
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }

        /// <summary>
        /// �D�D
        /// </summary>
        [Required]
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// �X�ͰO���O
        /// 1=��X, 2=�q��, 3=�q�H
        /// </summary>
        public int INT002 { get; set; }

        /// <summary>
        /// �ɮ׺��}
        /// </summary>
        [StringLength(150)]
        public string INT003 { get; set; }

        /// <summary>
        /// ���e
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// �X�ͮɶ�
        /// </summary>
        public string INT005 { get; set; }
        /// <summary>
        /// �D�ަ^����
        /// </summary>
        public string INT006 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [ForeignKey("INTRA")]
        [StringLength(36)]
        public string INT999 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// ���A
        /// 0=���f��,1=�w�f�� 2=�R��
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// �إ߮ɶ� (�۰ʱa�J)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }

        public virtual INTRA INTRA { get; set; }
    }
}