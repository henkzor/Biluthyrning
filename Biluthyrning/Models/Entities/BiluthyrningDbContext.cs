﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biluthyrning.Models.Entities
{
    public partial class BiluthyrningDBContext : DbContext
    {
        public BiluthyrningDBContext()
        {
        }

        public BiluthyrningDBContext(DbContextOptions<BiluthyrningDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Events> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Biluthyrning;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.ToTable("Bookings", "buh");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookingEnd).HasColumnType("datetime");

                entity.Property(e => e.BookingPlaced).HasColumnType("datetime");

                entity.Property(e => e.BookingStart).HasColumnType("datetime");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__CarID__6166761E");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__Custom__625A9A57");
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.ToTable("Cars", "buh");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cartype)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RegnNr)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("Customers", "buh");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PersonNr)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("Events", "buh");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Events__BookingI__681373AD");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Events__CarID__662B2B3B");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Events__Customer__671F4F74");
            });
        }
    }
}
