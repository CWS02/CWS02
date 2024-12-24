using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using prjWastes6.Models;

namespace prjWastes6.Models
{
    public partial class CPCContext : DbContext
    {
        public CPCContext()
            : base("name=CPC")
        {
        }

        /// <summary>
        /// «È¤á³X½Í°O¿ý
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }
        /// <summary>
        /// ³X½Í°O¿ý
        /// </summary>
        public virtual DbSet<INTRB> INTRB { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
