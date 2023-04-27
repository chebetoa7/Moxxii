﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moxxii.Api.Data;

#nullable disable

namespace Moxxii.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230424184322_Azure_AddUbicationRealyTime")]
    partial class Azure_AddUbicationRealyTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Moxxii.Shared.Entities.Promociones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descrition")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<double>("PromotionPrice")
                        .HasColumnType("float");

                    b.Property<int?>("ruta_paradaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ruta_paradaId");

                    b.ToTable("Promociones");
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Rutas_Paradas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommentDrive")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<string>("CommentPass")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("LongEnd")
                        .HasColumnType("float");

                    b.Property<double>("LongInitial")
                        .HasColumnType("float");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("StatusTrip")
                        .HasColumnType("bit");

                    b.Property<double>("TripPrice")
                        .HasColumnType("float");

                    b.Property<double>("TripPriceMoxxii")
                        .HasColumnType("float");

                    b.Property<int>("ViajeId")
                        .HasColumnType("int");

                    b.Property<double>("latEnd")
                        .HasColumnType("float");

                    b.Property<double>("latInitial")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ViajeId");

                    b.ToTable("Paradas");
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Birthdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("DateAge")
                        .HasColumnType("int");

                    b.Property<string>("Disponibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OtherLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("UbicationRealLat")
                        .HasColumnType("float");

                    b.Property<double>("UbicationRealLon")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Password")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Viaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommentDrive")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<string>("CommentPass")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("LongEnd")
                        .HasColumnType("float");

                    b.Property<double>("LongInitial")
                        .HasColumnType("float");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("StatusTrip")
                        .HasColumnType("bit");

                    b.Property<double>("TripPriceTotalMoxxii")
                        .HasColumnType("float");

                    b.Property<double>("latEnd")
                        .HasColumnType("float");

                    b.Property<double>("latInitial")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Viajes");
                });

            modelBuilder.Entity("UsuarioViaje", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("ViajesId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "ViajesId");

                    b.HasIndex("ViajesId");

                    b.ToTable("UsuarioViaje");
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Promociones", b =>
                {
                    b.HasOne("Moxxii.Shared.Entities.Rutas_Paradas", "ruta_parada")
                        .WithMany()
                        .HasForeignKey("ruta_paradaId");

                    b.Navigation("ruta_parada");
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Rutas_Paradas", b =>
                {
                    b.HasOne("Moxxii.Shared.Entities.Viaje", "Viaje")
                        .WithMany("rutas_paradas")
                        .HasForeignKey("ViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viaje");
                });

            modelBuilder.Entity("UsuarioViaje", b =>
                {
                    b.HasOne("Moxxii.Shared.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Moxxii.Shared.Entities.Viaje", null)
                        .WithMany()
                        .HasForeignKey("ViajesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Moxxii.Shared.Entities.Viaje", b =>
                {
                    b.Navigation("rutas_paradas");
                });
#pragma warning restore 612, 618
        }
    }
}
