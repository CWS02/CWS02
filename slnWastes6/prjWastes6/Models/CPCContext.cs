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
        /// 客戶訪談記錄
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }
        /// <summary>
        /// 訪談記錄
        /// </summary>
        public virtual DbSet<INTRB> INTRB { get; set; }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定 INTRA 與 INTRB 的一對多關聯
            modelBuilder.Entity<INTRA>()
                .HasMany(intra => intra.INTRBs) // INTRA 會有多個 INTRB
                .WithRequired()                 // 每個 INTRB 都必須有一個 INTRA
                .HasForeignKey(intrb => intrb.INT999); // 外鍵設定為 INTRB 的 INT999
        }

    }
}
