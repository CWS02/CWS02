namespace prjWastes6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    [Table("tMember")]
    public partial class tMember
    {
        [Key]
        public int fId { get; set; }
        [DisplayName("±b¸¹")]
        [Required]
        [StringLength(50)]
        public string fUserId { get; set; }
        [DisplayName("±K½X")]
        [Required]
        [StringLength(50)]
        public string fPwd { get; set; }
        [DisplayName("©m¦W")]
        [Required]
        [StringLength(50)]
        public string fName { get; set; }
        [DisplayName("«H½c")]
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string fEmail { get; set; }

          

        [StringLength(1)]
        public string UpdateRight { get; set; }

    }
}
