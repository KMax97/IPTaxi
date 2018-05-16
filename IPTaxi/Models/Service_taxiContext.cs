using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IPTaxi.Models
{
    public partial class Service_taxiContext : DbContext
    {
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Dispetcher> Dispetcher { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Mark> Mark { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<Street> Street { get; set; }
        public virtual DbSet<Ts> Ts { get; set; }

        public Service_taxiContext(DbContextOptions<Service_taxiContext> options)
    : base(options)
{ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.AmountOfOrders).HasColumnName("Amount_of_orders");

                entity.Property(e => e.AnountOfPoints).HasColumnName("Anount_of_points");

                entity.Property(e => e.NumberOfTelephone)
                    .HasColumnName("Number_of_telephone")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ColorId).HasColumnName("Color_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dispetcher>(entity =>
            {
                entity.Property(e => e.DispetcherId).HasColumnName("Dispetcher_ID");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassportData)
                    .IsRequired()
                    .HasColumnName("Passport_data")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .HasColumnName("Registration_number")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasColumnName("Telephone_number")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.RegistrationNumberNavigation)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.RegistrationNumber)
                    .HasConstraintName("FK_Driver_TS");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.Property(e => e.MarkId).HasColumnName("Mark_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.NumberOfOrder);

                entity.Property(e => e.NumberOfOrder).HasColumnName("Number_of_order");

                entity.Property(e => e.AmountOfAccruedPoints).HasColumnName("Amount_of_accrued_points");

                entity.Property(e => e.AmountOfWrittenPoints).HasColumnName("Amount_of_written_points");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.DispetcherId).HasColumnName("Dispetcher_ID");

                entity.Property(e => e.FinalStreetId).HasColumnName("Final_street_ID");

                entity.Property(e => e.NumberOfFinalHouse)
                    .IsRequired()
                    .HasColumnName("Number_of_final_house")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfRecord).HasColumnName("Number_of_record");

                entity.Property(e => e.NumberOfStartHouse)
                    .IsRequired()
                    .HasColumnName("Number_of_start_house")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.RealValue).HasColumnName("Real_value");

                entity.Property(e => e.StartStreetId).HasColumnName("Start_street_ID");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TimeOfEndingOrder)
                    .HasColumnName("Time_of_ending_order")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Order");

                entity.HasOne(d => d.Dispetcher)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.DispetcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Dispetcher");

                entity.HasOne(d => d.FinalStreet)
                    .WithMany(p => p.OrderFinalStreet)
                    .HasForeignKey(d => d.FinalStreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Street1");

                entity.HasOne(d => d.NumberOfRecordNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.NumberOfRecord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Record");

                entity.HasOne(d => d.StartStreet)
                    .WithMany(p => p.OrderStartStreet)
                    .HasForeignKey(d => d.StartStreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Street");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.NumberOfRecord);

                entity.Property(e => e.NumberOfRecord).HasColumnName("Number_of_record");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Intime).HasColumnType("datetime");

                entity.Property(e => e.Outtime).HasColumnType("datetime");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Record)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Driver");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.Property(e => e.StreetId).HasColumnName("Street_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ts>(entity =>
            {
                entity.HasKey(e => e.RegistrationNumber);

                entity.ToTable("TS");

                entity.Property(e => e.RegistrationNumber)
                    .HasColumnName("Registration_number")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ColorId).HasColumnName("Color_ID");

                entity.Property(e => e.MarkId).HasColumnName("Mark_ID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Ts)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TS_Color");

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.Ts)
                    .HasForeignKey(d => d.MarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TS_Mark");
            });
        }
    }
}
