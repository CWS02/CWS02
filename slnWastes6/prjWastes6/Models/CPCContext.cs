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
        /// �Ȥ�X�ͰO��
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
