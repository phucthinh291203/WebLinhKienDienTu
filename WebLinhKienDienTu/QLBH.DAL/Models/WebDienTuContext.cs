using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLBH.DAL.Models
{
    public partial class WebDienTuContext : DbContext
    {
        public WebDienTuContext()
        {
        }

        public WebDienTuContext(DbContextOptions<WebDienTuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Chitietdonthang> Chitietdonthangs { get; set; } = null!;
        public virtual DbSet<DanhGium> DanhGia { get; set; } = null!;
        public virtual DbSet<Dondathang> Dondathangs { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<LoaiSp> LoaiSps { get; set; } = null!;
        public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebDienTu;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.MaAdmin)
                    .HasName("PK__Admin__49341E38DD64DC09");

                entity.ToTable("Admin");

                entity.Property(e => e.HoTen).HasMaxLength(250);

                entity.Property(e => e.PassAdmin)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserAdmin)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Chitietdonthang>(entity =>
            {
                entity.HasKey(e => new { e.MaDonHang, e.MaSp })
                    .HasName("PK_CTDatHang");

                entity.ToTable("CHITIETDONTHANG");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.Chitietdonthangs)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donhang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Chitietdonthangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SP");
            });

            modelBuilder.Entity<DanhGium>(entity =>
            {
                entity.HasKey(e => e.MaDanhGia)
                    .HasName("PK__DanhGia__AA9515BF67A03B5F");

                entity.Property(e => e.DiemDanhGia).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK__DanhGia__MaKH__48CFD27E");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__DanhGia__MaSP__49C3F6B7");
            });

            modelBuilder.Entity<Dondathang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang)
                    .HasName("PK_DonDatHang");

                entity.ToTable("DONDATHANG");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.Ngaydat).HasColumnType("datetime");

                entity.Property(e => e.Ngaygiao).HasColumnType("datetime");

                entity.HasOne(d => d.MaAdminNavigation)
                    .WithMany(p => p.Dondathangs)
                    .HasForeignKey(d => d.MaAdmin)
                    .HasConstraintName("FK_AD");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Dondathangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_Khachhang");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK_Khachhang");

                entity.ToTable("KHACHHANG");

                entity.HasIndex(e => e.Taikhoan, "UQ__KHACHHAN__7FB3F64F193E00FE")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__KHACHHAN__A9D10534C9EA61B3")
                    .IsUnique();

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiachiKh)
                    .HasMaxLength(200)
                    .HasColumnName("DiachiKH");

                entity.Property(e => e.DienthoaiKh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DienthoaiKH");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysinh).HasColumnType("datetime");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSp);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoaiSp).HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenLoaiSp)
                    .HasMaxLength(50)
                    .HasColumnName("TenLoaiSP");
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK_NCC");

                entity.ToTable("NHACUNGCAP");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.Diachi).HasMaxLength(200);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(50)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK_SP");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.AnhSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AnhSP");

                entity.Property(e => e.Giaban).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(100)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_LoaiSP");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_NhaCC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
