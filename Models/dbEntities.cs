using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace csproject.Models;

public partial class dbEntities : DbContext
{
    public dbEntities()
    {
    }

    public dbEntities(DbContextOptions<dbEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<Genders> Genders { get; set; }

    public virtual DbSet<Money> Money { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbconn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Departments_1");

            entity.Property(e => e.Id).HasComment("索引");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .HasComment("班級名稱");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(50)
                .HasComment("班級代號");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasComment("備註");
        });

        modelBuilder.Entity<Genders>(entity =>
        {
            entity.Property(e => e.Id).HasComment("索引");
            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .HasComment("性別名稱");
            entity.Property(e => e.GenderNo)
                .HasMaxLength(50)
                .HasComment("性別代號");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasComment("備註");
        });

        modelBuilder.Entity<Money>(entity =>
        {
            entity.Property(e => e.Id).HasComment("索引");
            entity.Property(e => e.Breakfast).HasComment("早餐天數");
            entity.Property(e => e.Dinner).HasComment("晚餐天數");
            entity.Property(e => e.Lunch).HasComment("午餐天數");
            entity.Property(e => e.Price).HasComment("總金額");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasComment("備註");
            entity.Property(e => e.UserNo)
                .HasMaxLength(50)
                .HasComment("登入帳號");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.Id).HasComment("索引");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasComment("備註");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasComment("身分名稱");
            entity.Property(e => e.RoleNo)
                .HasMaxLength(50)
                .HasComment("身分代號");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.Id).HasComment("索引");
            entity.Property(e => e.Birthday).HasComment("出生日期");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(50)
                .HasComment("電子信箱");
            entity.Property(e => e.ContactTel)
                .HasMaxLength(50)
                .HasComment("行動電話");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(50)
                .HasComment("班級代號");
            entity.Property(e => e.GenderNo)
                .HasMaxLength(50)
                .HasComment("性別代號");
            entity.Property(e => e.IsValid).HasComment("合法性");
            entity.Property(e => e.LeaveDate).HasComment("結訓日期");
            entity.Property(e => e.NotifyPassword)
                .HasMaxLength(250)
                .HasComment("通知Token");
            entity.Property(e => e.OnboardDate).HasComment("起訓日期");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasComment("登入密碼");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasComment("備註");
            entity.Property(e => e.RoleNo)
                .HasMaxLength(50)
                .HasComment("身分代號");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasComment("登入名稱");
            entity.Property(e => e.UserNo)
                .HasMaxLength(50)
                .HasComment("登入帳號");
            entity.Property(e => e.ValidateCode)
                .HasMaxLength(250)
                .HasComment("驗證碼");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
