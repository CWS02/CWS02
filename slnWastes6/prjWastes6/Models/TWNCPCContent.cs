using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using prjWastes6.Models;

namespace prjWastes6.Models
{
    public partial class TWNCPCContext : DbContext
    {
        public TWNCPCContext()
            : base("name=TWNCPCContext")
        {
        }
               

        public virtual DbSet<CMSMF> CMSMF { get; set; }
        public virtual DbSet<ACPTA> ACPTA { get; set; }
        public virtual DbSet<ACPTB> ACPTB { get; set; }

        public virtual DbSet<PURMA> PURMA { get; set; }
        public virtual DbSet<PCMTF> PCMTF { get; set; }
        public virtual DbSet<PCMTG> PCMTG { get; set; }
        public virtual DbSet<ACTTA> ACTTA { get; set; }
        public virtual DbSet<AJSTA> AJSTA { get; set; }
        public virtual DbSet<AJSTB> AJSTB { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PCMTF>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF001)
                .IsFixedLength();

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF002)
                .IsFixedLength();

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF009)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF020)
                .HasPrecision(1, 0);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF021)
                .HasPrecision(1, 0);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF023)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.TF024)
                .HasPrecision(15, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTF>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG001)
                .IsFixedLength();

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG002)
                .IsFixedLength();

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG003)
                .IsFixedLength();

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG006)
                .HasPrecision(8, 5);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG008)
                .HasPrecision(20, 9);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG009)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG010)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG011)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG012)
                .HasPrecision(1, 0);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG021)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG022)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG028)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG029)
                .HasPrecision(15, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG036)
                .HasPrecision(2, 0);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG037)
                .HasPrecision(2, 0);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.TG038)
                .HasPrecision(20, 9);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PCMTG>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA001)
                .IsFixedLength();

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA018)
                .HasPrecision(16, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA019)
                .HasPrecision(7, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA054)
                .HasPrecision(8, 5);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA058)
                .HasPrecision(8, 5);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA071)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA072)
                .HasPrecision(15, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA083)
                .HasPrecision(8, 5);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA086)
                .HasPrecision(2, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA087)
                .HasPrecision(2, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA093)
                .HasPrecision(8, 5);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.MA095)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<PURMA>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA001)
                .IsFixedLength();

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA002)
                .IsFixedLength();

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA009)
                .HasPrecision(20, 9);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA016)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA017)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA027)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA028)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA029)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA030)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA036)
                .HasPrecision(8, 5);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA037)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA038)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA042)
                .HasPrecision(8, 5);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA043)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA047)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA048)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA051)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA054)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA055)
                .HasPrecision(15, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA062)
                .HasPrecision(20, 9);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA063)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.TA064)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTA>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB001)
                .IsFixedLength();

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB002)
                .IsFixedLength();

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB003)
                .IsFixedLength();

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB009)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB010)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB015)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB016)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB017)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB018)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB024)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB025)
                .HasPrecision(15, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.TB029)
                .HasPrecision(8, 5);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACPTB>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
               .Property(e => e.FLAG)
               .HasPrecision(3, 0);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.MF001)
                    .IsFixedLength();

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.MF008)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.MF009)
                    .HasPrecision(15, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.MF013)
                    .HasPrecision(20, 9);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.UDF06)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.UDF07)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.UDF08)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.UDF09)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<CMSMF>()
                    .Property(e => e.UDF10)
                    .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA001)
                .IsFixedLength();

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA002)
                .IsFixedLength();

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA007)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA008)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA012)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA017)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA019)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA020)
                .HasPrecision(15, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.TA041)
                .IsFixedLength();

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<ACTTA>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA001)
                .IsFixedLength();

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA002)
                .IsFixedLength();

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA007)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA008)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA009)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA018)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.TA019)
                .HasPrecision(15, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTA>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.FLAG)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB001)
                .IsFixedLength();

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB002)
                .IsFixedLength();

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB003)
                .IsFixedLength();

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB004)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB007)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB016)
                .HasPrecision(20, 9);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB017)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB028)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.TB029)
                .HasPrecision(15, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.UDF06)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.UDF07)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.UDF08)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.UDF09)
                .HasPrecision(21, 6);

            modelBuilder.Entity<AJSTB>()
                .Property(e => e.UDF10)
                .HasPrecision(21, 6);
        }
    }
}
