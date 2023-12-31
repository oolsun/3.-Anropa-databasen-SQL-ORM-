﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _3._Anropa_databasen__SQL___ORM_.Models;

public partial class MyLab3Context : DbContext
{
    public MyLab3Context()
    {
    }

    public MyLab3Context(DbContextOptions<MyLab3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grades> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=MyLab3;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<Grades>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkemployeeId).HasColumnName("FKEmployeeID");
            entity.Property(e => e.FkstudentId).HasColumnName("FKStudentID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Grade");
            entity.Property(e => e.GradeDate).HasColumnType("date");
            entity.Property(e => e.Subject).HasMaxLength(50);

            entity.HasOne(d => d.Fkemployee).WithMany()
                .HasForeignKey(d => d.FkemployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Employees");

            entity.HasOne(d => d.Fkstudent).WithMany()
                .HasForeignKey(d => d.FkstudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Class).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
