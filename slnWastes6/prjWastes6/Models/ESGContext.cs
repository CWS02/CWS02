using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using prjWastes6.Models;

namespace prjWastes6.Models
{
    public partial class ESGContext : DbContext
    {
        public ESGContext()
            : base("name=ESG")
        {
        }

        public virtual DbSet<BRM_MST_CODE> BRM_MST_CODE { get; set; }
        public virtual DbSet<BRM_MST_DEPT> BRM_MST_DEPT { get; set; }
        public virtual DbSet<BRM_MST_EMISSION_FACTOR> BRM_MST_EMISSION_FACTOR { get; set; }
        public virtual DbSet<BRM_MST_LANGUAGE> BRM_MST_LANGUAGE { get; set; }
        public virtual DbSet<BRM_MST_PLANT> BRM_MST_PLANT { get; set; }
        public virtual DbSet<CAT_THREE_EMPLOYEE_COMMUTING> CAT_THREE_EMPLOYEE_COMMUTING { get; set; }
        public virtual DbSet<CAT_TWO_TAIPOWER> CAT_TWO_TAIPOWER { get; set; }
        public virtual DbSet<GHG_MST_BOUND> GHG_MST_BOUND { get; set; }
        public virtual DbSet<GHG_MST_PROJECT> GHG_MST_PROJECT { get; set; }
        public virtual DbSet<GHG01_OFFICIAL_CAR> GHG01_OFFICIAL_CAR { get; set; }
        public virtual DbSet<SYS_MST_FUNCTION> SYS_MST_FUNCTION { get; set; }
        public virtual DbSet<SYS_MST_ROLE> SYS_MST_ROLE { get; set; }
        public virtual DbSet<SYS_MST_ROLE_FUNC_REL> SYS_MST_ROLE_FUNC_REL { get; set; }
        public virtual DbSet<SYS_MST_USER> SYS_MST_USER { get; set; }
        public virtual DbSet<tMember> tMember { get; set; }
        public virtual DbSet<WASTES> WASTES { get; set; }
        public virtual DbSet<WASTES_BURY> WASTES_BURY { get; set; }
        public virtual DbSet<WASTES_INCINERATION> WASTES_INCINERATION { get; set; }
        public virtual DbSet<GHG_MST_TAIPOWER> GHG_MST_TAIPOWER { get; set; }
        public virtual DbSet<ELECTRICITY_BILL> ELECTRICITY_BILL { get; set; }
        public virtual DbSet<GHG_MST_COMMUTE> GHG_MST_COMMUTE { get; set; }

        public virtual DbSet<WATER_BILL> WATER_BILL { get; set; }
        //20240328增加出差申請單欄位
        public virtual DbSet<BUSINESS_TRIP> BUSINESS_TRIP { get; set; }

        public virtual DbSet<TRIP_APPLICATION> TRIP_APPLICATION { get; set; }

        //20240409增加:計程車-出差申請單欄位
        public virtual DbSet<TAXI_APPLICATION> TAXI_APPLICATION { get; set; }

        //20240410增加:高鐵-出差申請單欄位
        public virtual DbSet<HIGH_SPEED_RAIL> HIGH_SPEED_RAIL { get; set; }

        //20240411增加:冷器欄位
        public virtual DbSet<AIR_CONDITIONER> AIR_CONDITIONER { get; set; }
        //20240419增加:員工國外差旅欄位
        public virtual DbSet<EMPLOYEE_TRAVEL_ABROAD> EMPLOYEE_TRAVEL_ABROAD { get; set; }

        //20240419增加:車輛用油欄位
        public virtual DbSet<VEHICLE_OIL> VEHICLE_OIL { get; set; }

        //20240422增加:滅火器欄位
        public virtual DbSet<FireExtin> FireExtin { get; set; }

        //20240422增加:冷媒欄位
        public virtual DbSet<ColdCoal> ColdCoal { get; set; }

        //20241007:空壓壓欄位
        public virtual DbSet<keyenec> keyenec { get; set; }

        public virtual DbSet<keyenec2> keyenec2 { get; set; }

        public virtual DbSet<SGS_Parameter> SGS_Parameter { get; set; }

        public virtual DbSet<WD40A> WD40A { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tMember>()
                .Property(e => e.UpdateRight)
                .IsFixedLength();

            modelBuilder.Entity<BRM_MST_EMISSION_FACTOR>()
                .Property(e => e.EF_VALUE)
                .HasPrecision(15, 10);

            modelBuilder.Entity<CAT_TWO_TAIPOWER>()
                .Property(e => e.BOUND_ID)
                .IsFixedLength();

            modelBuilder.Entity<CAT_TWO_TAIPOWER>()
                .Property(e => e.BOUND_NAME)
                .IsFixedLength();

            modelBuilder.Entity<CAT_TWO_TAIPOWER>()
                .Property(e => e.USAGE_DAYS)
                .IsFixedLength();

            modelBuilder.Entity<GHG01_OFFICIAL_CAR>()
                .Property(e => e.GHG_NAME)
                .IsFixedLength();

            modelBuilder.Entity<GHG01_OFFICIAL_CAR>()
                .Property(e => e.ACTIVITY_DATA)
                .HasPrecision(10, 4);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.FUNCTION_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.FUNCTION_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.FUNCTION_URL)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.MODULE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.LEVEL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.ICON_CLASS)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.DESCRIPTIONS)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.ENABLETO)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.AID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_MST_FUNCTION>()
                .Property(e => e.USEID)
                .IsUnicode(false);

            modelBuilder.Entity<WASTES>()
                .Property(e => e.DECLARED_WEIGHT)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES>()
                .Property(e => e.KILOMETERS)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES>()
                .Property(e => e.ACTIVITY_DATA)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES>()
                .Property(e => e.CARBON_EMISSION_FACTOR)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES>()
                .Property(e => e.CARBON_DIOXIDE)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_BURY>()
                .Property(e => e.DECLARED_WEIGHT)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_BURY>()
                .Property(e => e.KILOMETERS)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_BURY>()
                .Property(e => e.ACTIVITY_DATA)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_BURY>()
                .Property(e => e.CARBON_EMISSION_FACTOR)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_BURY>()
                .Property(e => e.CARBON_DIOXIDE)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_INCINERATION>()
                .Property(e => e.DECLARED_WEIGHT)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_INCINERATION>()
                .Property(e => e.KILOMETERS)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_INCINERATION>()
                .Property(e => e.ACTIVITY_DATA)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_INCINERATION>()
                .Property(e => e.CARBON_EMISSION_FACTOR)
                .HasPrecision(15, 10);

            modelBuilder.Entity<WASTES_INCINERATION>()
                .Property(e => e.CARBON_DIOXIDE)
                .HasPrecision(15, 10);

     

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.PEAK_ELECTRICITY)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.HALF_SPIKE_POWER)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.SATURDAY_HALF_PEAK)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.OFF_PEAK_ELECTRICITY)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.TOTAL_ELECTRICITY)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
              .Property(e => e.TOTAL_BILL_TAX)
              .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
              .Property(e => e.CARBON_PERIOD)
              .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
              .Property(e => e.CARBON_EMISSION_FACTOR)
              .HasPrecision(15, 2);

            modelBuilder.Entity<ELECTRICITY_BILL>()
              .Property(e => e.CARBON_DIOXIDE)
              .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
                .Property(e => e.NUMBER_POINTERS)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
                .Property(e => e.TOTAL_WATER)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
              .Property(e => e.TOTAL_BILL_TAX)
              .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
              .Property(e => e.CARBON_PERIOD)
              .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
              .Property(e => e.CARBON_EMISSION_FACTOR)
              .HasPrecision(15, 2);

            modelBuilder.Entity<WATER_BILL>()
              .Property(e => e.CARBON_DIOXIDE)
              .HasPrecision(15, 2);

            //20240328增加出差申請單欄位-太複雜
            modelBuilder.Entity<BUSINESS_TRIP>()
             .Property(e => e.CARBON_PERIOD)
             .HasPrecision(15, 2);

            modelBuilder.Entity<BUSINESS_TRIP>()
              .Property(e => e.CARBON_EMISSION_FACTOR)
              .HasPrecision(15, 2);

            modelBuilder.Entity<BUSINESS_TRIP>()
              .Property(e => e.CARBON_DIOXIDE)
              .HasPrecision(15, 2);

            //20240410增加高鐵-出差申請單欄位
            modelBuilder.Entity<HIGH_SPEED_RAIL>()
             .Property(e => e.CARBON_PERIOD)
             .HasPrecision(15, 2);

            //20240409增加計程車-出差申請單欄位

            modelBuilder.Entity<TAXI_APPLICATION>()
              .Property(e => e.AMOUNT)
              .HasPrecision(15, 2);

            //20240412增加冷氣欄位

            modelBuilder.Entity<AIR_CONDITIONER>()
              .Property(e => e.TOTAL_HOURS)
              .HasPrecision(15, 2);

            modelBuilder.Entity<GHG_MST_COMMUTE>()
                .Property(e => e.ONEWAY_KM)
                .HasPrecision(4, 1);

            modelBuilder.Entity<GHG_MST_COMMUTE>()
                .Property(e => e.DOUBLE_KM)
                .HasPrecision(4, 1);

            //20250325水費要顯示 PDF 檔案
            modelBuilder.Entity<WATER_BILL>()
                 .Property(e => e.PDF_FILE)
                 .IsMaxLength();
            
            modelBuilder.Entity<WATER_BILL>()
                .Property(e => e.PDF_CONTENT)
                .IsMaxLength();


            //20250326電費要顯示 PDF 檔案
            modelBuilder.Entity<ELECTRICITY_BILL>()
                 .Property(e => e.PDF_FILE)
                 .IsMaxLength();

            modelBuilder.Entity<ELECTRICITY_BILL>()
                .Property(e => e.PDF_CONTENT)
                .IsMaxLength();

            //20250326廢棄物要顯示 PDF 檔案
            modelBuilder.Entity<WASTES>()
                 .Property(e => e.PDF_FILE)
                 .IsMaxLength();

            modelBuilder.Entity<WASTES>()
                .Property(e => e.PDF_CONTENT)
                .IsMaxLength();

            //20240419增加:員工國外差旅欄位

            modelBuilder.Entity<EMPLOYEE_TRAVEL_ABROAD>()
                .Property(e => e.TRIP_NO)
                .HasPrecision(18, 0);

            modelBuilder.Entity<EMPLOYEE_TRAVEL_ABROAD>()
                .Property(e => e.EMISSIONS)
                .HasPrecision(15, 2);

            modelBuilder.Entity<EMPLOYEE_TRAVEL_ABROAD>()
                .Property(e => e.RETURN_EMISSIONS)
                .HasPrecision(15, 2);

            modelBuilder.Entity<EMPLOYEE_TRAVEL_ABROAD>()
                .Property(e => e.TOTAL_EMISSIONS)
                .HasPrecision(15, 2);

            //20240419增加:車輛用油欄位

            modelBuilder.Entity<VEHICLE_OIL>()
                .Property(e => e.TRIP_NO)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VEHICLE_OIL>()
                .Property(e => e.LITERS)
                .HasPrecision(15, 2);

            //20240422增加:冷媒欄位
            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC001)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC002)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC003)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC004)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC005)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC006)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC007)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC008)
                .HasPrecision(18, 6);

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC009)
                .IsFixedLength();

            modelBuilder.Entity<ColdCoal>()
                .Property(e => e.CC000)
                .HasPrecision(18, 0);

            //20240422增加:滅火器欄位
            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE001)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE002)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE003)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE004)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE005)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE006)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE007)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE008)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE009)
                .IsFixedLength();

            modelBuilder.Entity<FireExtin>()
                .Property(e => e.FE000)
                .HasPrecision(18, 0);

            modelBuilder.Entity<keyenec>()
                   .HasKey(k => new { k.Year, k.Month, k.Day, k.Hour });

            modelBuilder.Entity<keyenec>()
       .Property(k => k.Year).HasColumnType("nvarchar").HasMaxLength(50);

            modelBuilder.Entity<keyenec>()
                .Property(k => k.Month).HasColumnType("nvarchar").HasMaxLength(50);

            modelBuilder.Entity<keyenec>()
                .Property(k => k.Day).HasColumnType("nvarchar").HasMaxLength(50);

            modelBuilder.Entity<keyenec>()
                .Property(k => k.Hour).HasColumnType("nvarchar").HasMaxLength(50);

            modelBuilder.Entity<keyenec>()
                .Property(k => k.total_amount).HasColumnType("decimal").HasPrecision(15, 3);

            modelBuilder.Entity<keyenec>()
                .Property(k => k.Leakage).HasColumnType("decimal").HasPrecision(15, 3);
        }
    }
}
