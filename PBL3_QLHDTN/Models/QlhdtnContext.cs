using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PBL3_QLHDTN.Models;

public partial class QlhdtnContext : DbContext
{
    public QlhdtnContext()
    {
    }

    public QlhdtnContext(DbContextOptions<QlhdtnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baocao> Baocaos { get; set; }

    public virtual DbSet<Canhan> Canhans { get; set; }

    public virtual DbSet<DanhgiaHd> DanhgiaHds { get; set; }

    public virtual DbSet<DanhgiaTnv> DanhgiaTnvs { get; set; }

    public virtual DbSet<Hdyeuthich> Hdyeuthiches { get; set; }

    public virtual DbSet<Hoatdong> Hoatdongs { get; set; }

    public virtual DbSet<Linhvuc> Linhvucs { get; set; }

    public virtual DbSet<MotaHd> MotaHds { get; set; }

    public virtual DbSet<Motatochuc> Motatochucs { get; set; }

    public virtual DbSet<QuanlyTghd> QuanlyTghds { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tochuc> Tochucs { get; set; }

    public virtual DbSet<TrangthaiHd> TrangthaiHds { get; set; }

    public virtual DbSet<Vaitro> Vaitros { get; set; }

    public virtual DbSet<YeucauTnv> YeucauTnvs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6H8LU7L\\SQLEXPRESS;Initial Catalog=QLHDTN;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
#pragma warning restore CS1030 // #warning directive

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baocao>(entity =>
        {
            entity.ToTable("Baocao");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idtkbaocao).HasColumnName("IDtkbaocao");
            entity.Property(e => e.Idtkbibaocao).HasColumnName("IDtkbibaocao");
            entity.Property(e => e.Ndbaocao)
                .HasMaxLength(500)
                .HasColumnName("NDbaocao");
            entity.Property(e => e.Tgbaocao).HasColumnName("TGbaocao");

            entity.HasOne(d => d.IdtkbaocaoNavigation).WithMany(p => p.BaocaoIdtkbaocaoNavigations)
                .HasForeignKey(d => d.Idtkbaocao)
                .HasConstraintName("FK_Baocao_Canhan");

            entity.HasOne(d => d.Idtkbaocao1).WithMany(p => p.BaocaoIdtkbaocao1s)
                .HasForeignKey(d => d.Idtkbaocao)
                .HasConstraintName("FK_Baocao_Tochuc");

            entity.HasOne(d => d.IdtkbibaocaoNavigation).WithMany(p => p.BaocaoIdtkbibaocaoNavigations)
                .HasForeignKey(d => d.Idtkbibaocao)
                .HasConstraintName("FK_Baocao_Canhan1");

            entity.HasOne(d => d.Idtkbibaocao1).WithMany(p => p.BaocaoIdtkbibaocao1s)
                .HasForeignKey(d => d.Idtkbibaocao)
                .HasConstraintName("FK_Baocao_Tochuc1");
        });

        modelBuilder.Entity<Canhan>(entity =>
        {
            entity.HasKey(e => e.Idcanhan);

            entity.ToTable("Canhan");

            entity.Property(e => e.Idcanhan)
                .ValueGeneratedNever()
                .HasColumnName("IDcanhan");
            entity.Property(e => e.Diachi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.IdcanhanNavigation).WithOne(p => p.Canhan)
                .HasForeignKey<Canhan>(d => d.Idcanhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canhan_Taikhoan");
        });

        modelBuilder.Entity<DanhgiaHd>(entity =>
        {
            entity.ToTable("DanhgiaHD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Danhgia).HasMaxLength(500);
            entity.Property(e => e.Idcanhan).HasColumnName("IDcanhan");
            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");
            entity.Property(e => e.Tgdanhgia)
            .HasColumnName("TGdanhgia");

            entity.HasOne(d => d.IdcanhanNavigation).WithMany(p => p.DanhgiaHds)
                .HasForeignKey(d => d.Idcanhan)
                .HasConstraintName("FK_DanhgiaHD_Canhan");

            entity.HasOne(d => d.IdhoatdongNavigation).WithMany(p => p.DanhgiaHds)
                .HasForeignKey(d => d.Idhoatdong)
                .HasConstraintName("FK_DanhgiaHD_Hoatdong");
        });

        modelBuilder.Entity<DanhgiaTnv>(entity =>
        {
            entity.ToTable("DanhgiaTNV");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Danhgia).HasMaxLength(500);
            entity.Property(e => e.Idcanhan).HasColumnName("IDcanhan");
            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");
            entity.Property(e => e.Idtochuc).HasColumnName("IDtochuc");
            entity.Property(e => e.Tgdanhgia).HasColumnName("TGdanhgia");

            entity.HasOne(d => d.IdcanhanNavigation).WithMany(p => p.DanhgiaTnvs)
                .HasForeignKey(d => d.Idcanhan)
                .HasConstraintName("FK_DanhgiaTNV_Canhan");

            entity.HasOne(d => d.IdhoatdongNavigation).WithMany(p => p.DanhgiaTnvs)
                .HasForeignKey(d => d.Idhoatdong)
                .HasConstraintName("FK_DanhgiaTNV_Hoatdong");

            entity.HasOne(d => d.IdtochucNavigation).WithMany(p => p.DanhgiaTnvs)
                .HasForeignKey(d => d.Idtochuc)
                .HasConstraintName("FK_DanhgiaTNV_Tochuc");
        });

        modelBuilder.Entity<Hdyeuthich>(entity =>
        {
            entity.ToTable("HDyeuthich");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcanhan).HasColumnName("IDcanhan");
            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");

            entity.HasOne(d => d.IdcanhanNavigation).WithMany(p => p.Hdyeuthiches)
                .HasForeignKey(d => d.Idcanhan)
                .HasConstraintName("FK_HDyeuthich_Canhan");

            entity.HasOne(d => d.IdhoatdongNavigation).WithMany(p => p.Hdyeuthiches)
                .HasForeignKey(d => d.Idhoatdong)
                .HasConstraintName("FK_HDyeuthich_Hoatdong");
        });

        modelBuilder.Entity<Hoatdong>(entity =>
        {
            entity.HasKey(e => e.Idhoatdong);

            entity.ToTable("Hoatdong");

            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");
            entity.Property(e => e.Idtochuc).HasColumnName("IDtochuc");
            entity.Property(e => e.Sltnvcan).HasColumnName("SLTNVcan");

            entity.HasOne(d => d.IdtochucNavigation).WithMany(p => p.Hoatdongs)
                .HasForeignKey(d => d.Idtochuc)
                .HasConstraintName("FK_Hoatdong_Tochuc");

            entity.HasOne(d => d.TrangthaiNavigation).WithMany(p => p.Hoatdongs)
                .HasForeignKey(d => d.Trangthai)
                .HasConstraintName("FK_Hoatdong_TrangthaiHD");
        });

        modelBuilder.Entity<Linhvuc>(entity =>
        {
            entity.HasKey(e => e.Idlinhvuc);

            entity.ToTable("Linhvuc");

            entity.Property(e => e.Idlinhvuc).HasColumnName("IDlinhvuc");
            entity.Property(e => e.Anh).HasColumnType("image");
            entity.Property(e => e.Linhvuc1)
                .HasMaxLength(200)
                .HasColumnName("Linhvuc");
        });

        modelBuilder.Entity<MotaHd>(entity =>
        {
            entity.HasKey(e => e.Idhoatdong);

            entity.ToTable("MotaHD");

            entity.Property(e => e.Idhoatdong)
                .ValueGeneratedNever()
                .HasColumnName("IDhoatdong");
            entity.Property(e => e.DiaDiem).HasMaxLength(200);
            entity.Property(e => e.Lydohuy).HasMaxLength(500);
            entity.Property(e => e.MuctieuHd)
                .HasMaxLength(500)
                .HasColumnName("MuctieuHD");
            entity.Property(e => e.Tenhoatdong).HasMaxLength(200);
            entity.Property(e => e.Tgbdchinhsua).HasColumnName("TGBDchinhsua");
            entity.Property(e => e.Tgktchinhsua).HasColumnName("TGKTchinhsua");

            entity.HasOne(d => d.IdhoatdongNavigation).WithOne(p => p.MotaHd)
                .HasForeignKey<MotaHd>(d => d.Idhoatdong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MotaHD_Hoatdong");

            entity.HasOne(d => d.LinhvucNavigation).WithMany(p => p.MotaHds)
                .HasForeignKey(d => d.Linhvuc)
                .HasConstraintName("FK_MotaHD_Linhvuc");
        });

        modelBuilder.Entity<Motatochuc>(entity =>
        {
            entity.HasKey(e => e.Idtochuc);

            entity.ToTable("Motatochuc");

            entity.Property(e => e.Idtochuc)
                .ValueGeneratedNever()
                .HasColumnName("IDtochuc");
            entity.Property(e => e.Gioithieu).HasMaxLength(500);
            entity.Property(e => e.Thanhtuu).HasMaxLength(500);

            entity.HasOne(d => d.IdtochucNavigation).WithOne(p => p.Motatochuc)
                .HasForeignKey<Motatochuc>(d => d.Idtochuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Motatochuc_Tochuc");
        });

        modelBuilder.Entity<QuanlyTghd>(entity =>
        {
           
            entity.HasKey(e => e.Id);
            entity.ToTable("QuanlyTGHD");
            entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();
           
            entity.Property(e => e.Idcanhan).HasColumnName("IDcanhan");
            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");
            entity.Property(e => e.Nhuocdiem).HasMaxLength(500);
            entity.Property(e => e.Tenban).HasMaxLength(200);
            entity.Property(e => e.Uudiem).HasMaxLength(500);

            entity.HasOne(d => d.IdcanhanNavigation).WithMany(p => p.QuanlyTghds)
                .HasForeignKey(d => d.Idcanhan)
                .HasConstraintName("FK_QuanlyTGHD_Canhan");

            entity.HasOne(d => d.IdhoatdongNavigation).WithMany(p => p.QuanlyTghds)
                .HasForeignKey(d => d.Idhoatdong)
                .HasConstraintName("FK_QuanlyTGHD_Hoatdong");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.ToTable("Taikhoan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Matkhau).HasMaxLength(50);
            entity.Property(e => e.Tendangnhap).HasMaxLength(50);

            entity.HasOne(d => d.VaitroNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Vaitro)
                .HasConstraintName("FK_Taikhoan_Vaitro");
        });

        modelBuilder.Entity<Tochuc>(entity =>
        {
            entity.HasKey(e => e.Idtochuc);

            entity.ToTable("Tochuc");

            entity.Property(e => e.Idtochuc)
                .ValueGeneratedNever()
                .HasColumnName("IDtochuc");
            entity.Property(e => e.Diachi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.IdtochucNavigation).WithOne(p => p.Tochuc)
                .HasForeignKey<Tochuc>(d => d.Idtochuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tochuc_Taikhoan");
        });

        modelBuilder.Entity<TrangthaiHd>(entity =>
        {
            entity.HasKey(e => e.Idtrangthai).HasName("PK_Trangthai");

            entity.ToTable("TrangthaiHD");

            entity.Property(e => e.Idtrangthai)
                .ValueGeneratedNever()
                .HasColumnName("IDtrangthai");
            entity.Property(e => e.Trangthai).HasMaxLength(200);
        });

        modelBuilder.Entity<Vaitro>(entity =>
        {
            entity.HasKey(e => e.Idvaitro);

            entity.ToTable("Vaitro");

            entity.Property(e => e.Idvaitro)
                .ValueGeneratedNever()
                .HasColumnName("IDvaitro");
            entity.Property(e => e.Vaitro1)
                .HasMaxLength(20)
                .HasColumnName("Vaitro");
        });

        modelBuilder.Entity<YeucauTnv>(entity =>
        {
            entity.ToTable("YeucauTNV");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idhoatdong).HasColumnName("IDhoatdong");
            entity.Property(e => e.Nhiemvu).HasMaxLength(500);
            entity.Property(e => e.Sltnvcan).HasColumnName("SLTNVcan");
            entity.Property(e => e.Sltnvdk).HasColumnName("SLTNVdk");
            entity.Property(e => e.Tenban).HasMaxLength(200);
            entity.Property(e => e.Yeucau).HasMaxLength(500);

            entity.HasOne(d => d.IdhoatdongNavigation).WithMany(p => p.YeucauTnvs)
                .HasForeignKey(d => d.Idhoatdong)
                .HasConstraintName("FK_YeucauTNV_Hoatdong");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
