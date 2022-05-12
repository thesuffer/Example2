using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalAssistantBot.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAssistantBot
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<BossemployeeInfoAll> BossemployeeInfoAlls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DbConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BossemployeeInfoAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BOSSEmployeeInfo_All");

                entity.Property(e => e.BusinessEmail)
                    .HasMaxLength(100)
                    .HasColumnName("BUSINESS_EMAIL");

                entity.Property(e => e.BusinessMobile)
                    .HasMaxLength(100)
                    .HasColumnName("BUSINESS_MOBILE");

                entity.Property(e => e.BusinessPhone)
                    .HasMaxLength(100)
                    .HasColumnName("BUSINESS_PHONE");

                entity.Property(e => e.Cityname)
                    .HasMaxLength(255)
                    .HasColumnName("CITYNAME");

                entity.Property(e => e.Costcentre)
                    .HasMaxLength(1029)
                    .HasColumnName("COSTCENTRE");

                entity.Property(e => e.Departmentid).HasColumnName("DEPARTMENTID");

                entity.Property(e => e.Departmentname)
                    .HasMaxLength(255)
                    .HasColumnName("DEPARTMENTNAME");

                entity.Property(e => e.Departmentnameru)
                    .HasMaxLength(255)
                    .HasColumnName("DEPARTMENTNAMERU");


                entity.Property(e => e.Employeetype)
                    .HasMaxLength(100)
                    .HasColumnName("EMPLOYEETYPE");


                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("FIRST_NAME");


                entity.Property(e => e.FirstNameRu)
                    .HasMaxLength(100)
                    .HasColumnName("FIRST_NAME_RU");


                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("LAST_NAME");


                entity.Property(e => e.LastNameRu)
                    .HasMaxLength(100)
                    .HasColumnName("LAST_NAME_RU");

                entity.Property(e => e.Managersid).HasColumnName("MANAGERSID");

                entity.Property(e => e.MiddleNameRu)
                    .HasMaxLength(100)
                    .HasColumnName("MIDDLE_NAME_RU");


                entity.Property(e => e.Networkaccount)
                    .HasMaxLength(255)
                    .HasColumnName("NETWORKACCOUNT");

                entity.Property(e => e.Photo)
                    .HasColumnType("image")
                    .HasColumnName("PHOTO");


                entity.Property(e => e.Positionname)
                    .HasMaxLength(255)
                    .HasColumnName("POSITIONNAME");

                entity.Property(e => e.Positionnameru)
                    .HasMaxLength(255)
                    .HasColumnName("POSITIONNAMERU");

                entity.Property(e => e.Sector)
                    .HasMaxLength(100)
                    .HasColumnName("SECTOR");

                entity.Property(e => e.Sectorru)
                    .HasMaxLength(255)
                    .HasColumnName("SECTORRU");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Wwid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("WWID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
