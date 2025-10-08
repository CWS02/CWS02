namespace prjWastes6.Models
{
    using Antlr.Runtime.Tree;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoefficientLinks")]
    public class CoefficientLink
    {
        [Key]
        public int ID {  get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Status { get; set; }
    }
}
