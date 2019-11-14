﻿// <auto-generated />
using System;
using Hunter.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hunter.DataBase.Migrations
{
    [DbContext(typeof(HunterDBContext))]
    [Migration("20190531160728_removed_devicetypes_IMEI_lastconnected")]
    partial class removed_devicetypes_IMEI_lastconnected
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter.DataBase.Models.Devices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Manufacturer")
                        .IsRequired();

                    b.Property<string>("Model")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Hunter.DataBase.Models.TaskData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("TasksId");

                    b.HasKey("Id");

                    b.HasIndex("TasksId");

                    b.ToTable("TaskData");
                });

            modelBuilder.Entity("Hunter.DataBase.Models.Tasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<Guid>("DeviceId");

                    b.Property<DateTime?>("End");

                    b.Property<int>("IntervalDays");

                    b.Property<int>("IntervalSeconds");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnabled");

                    b.Property<DateTime?>("LastRun");

                    b.Property<DateTime?>("NextRun");

                    b.Property<DateTime>("Start");

                    b.Property<int>("Status");

                    b.Property<string>("StatusMessage");

                    b.Property<int>("TaskType");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Hunter.DataBase.Models.TaskData", b =>
                {
                    b.HasOne("Hunter.DataBase.Models.Tasks", "Tasks")
                        .WithMany("TaskData")
                        .HasForeignKey("TasksId");
                });

            modelBuilder.Entity("Hunter.DataBase.Models.Tasks", b =>
                {
                    b.HasOne("Hunter.DataBase.Models.Devices", "Device")
                        .WithMany("Tasks")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}