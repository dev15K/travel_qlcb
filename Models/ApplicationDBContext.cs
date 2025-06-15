using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QLCB.Models
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<MayBay> MayBays { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<SanBay> SanBays { get; set; }
        public DbSet<ChuyenBay> ChuyenBays { get; set; }
        public DbSet<VeBay> VeBays { get; set; }
        public DbSet<ChungNhan> ChungNhans { get; set; }
        public DbSet<PhanCong> PhanCongs { get; set; }
        public DbSet<DatVe> DatVes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Config bảng SanBay
            modelBuilder.Entity<SanBay>()
                .HasKey(sb => sb.MaSanBay);

            modelBuilder.Entity<SanBay>()
                .HasMany(sb => sb.ChuyenBaysDi)
                .WithOne(cb => cb.SanBayDi)
                .HasForeignKey(cb => cb.MaSanBayDi)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SanBay>()
                .HasMany(sb => sb.ChuyenBaysDen)
                .WithOne(cb => cb.SanBayDen)
                .HasForeignKey(cb => cb.MaSanBayDen)
                .OnDelete(DeleteBehavior.Restrict);

            // Config bảng MayBay
            modelBuilder.Entity<MayBay>()
                .HasKey(mb => mb.MaMayBay);

            modelBuilder.Entity<MayBay>()
                .HasMany(mb => mb.ChuyenBays)
                .WithOne(cb => cb.MayBay)
                .HasForeignKey(cb => cb.MaMayBay)
                .OnDelete(DeleteBehavior.Cascade);

            // Config bảng ChuyenBay
            modelBuilder.Entity<ChuyenBay>()
                .HasKey(cb => cb.MaChuyenBay);

            // Config bảng VeBay
            modelBuilder.Entity<VeBay>()
                .Property(v => v.GiaVe)
                .HasColumnType("decimal(18,2)");
        }
    }
}
