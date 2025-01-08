using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using prjWastes6.Models;

namespace prjWastes6.Models
{
    public partial class CPCContext : DbContext
    {
        public CPCContext()
            : base("name=CPC")
        {
            this.Configuration.LazyLoadingEnabled = true; 
            this.Configuration.ProxyCreationEnabled = true; 
        }

        /// <summary>
        /// �Ȥ�X�ͰO��
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }
        /// <summary>
        /// �X�ͰO��
        /// </summary>
        public virtual DbSet<INTRB> INTRB { get; set; }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // �]�w INTRA �P INTRB ���@��h���p
            modelBuilder.Entity<INTRA>()
                .HasMany(intra => intra.INTRBs) // INTRA �|���h�� INTRB
                .WithRequired()                 // �C�� INTRB ���������@�� INTRA
                .HasForeignKey(intrb => intrb.INT999); // �~��]�w�� INTRB �� INT999
        }

    }
}
