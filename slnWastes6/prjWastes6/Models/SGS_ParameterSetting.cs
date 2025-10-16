namespace prjWastes6.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SGS_ParameterSetting")]
    public class SGS_ParameterSetting
    {
        /// <summary>
        /// �ߤ@�ѧO�X�A�۰ʲ��͡C
        /// </summary>
        [Key]
        public Guid PAR000 { get; set; }

        /// <summary>
        /// �P SGS_Parameter ��ƪ����p�ѧO�X�C
        /// </summary>
        public Guid? PAR001 { get; set; }

        /// <summary>
        /// �W�١]��ܦW�٩��ѧO���ҡ^�C
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PAR002 { get; set; }

        /// <summary>
        /// ���O�A�Ҧp�G���O�B�q�O���C
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PAR003 { get; set; }
        /// <summary>
        /// ���O�Ӷ�
        /// </summary>
        public string PAR004 { get; set; }
        /// <summary>
        /// ���O�Ӷ�2
        /// </summary>
        public string PAR005 { get; set; }


        /// <summary>
        /// �إ߮ɶ��C
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ��s�ɶ��C
        /// </summary>
        [Required]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// �إߩΧ�s���ϥΪ� IP ��}�C
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }
    }
}
