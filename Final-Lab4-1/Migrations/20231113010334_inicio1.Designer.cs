﻿// <auto-generated />
using Final_Lab4_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Final_Lab4_1.Migrations
{
    [DbContext(typeof(AppDBcontext))]
    [Migration("20231113010334_inicio1")]
    partial class inicio1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Final_Lab4_1.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CuotaId")
                        .HasColumnType("int");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocalidadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.Property<int>("ProvinciaId")
                        .HasColumnType("int");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CuotaId");

                    b.HasIndex("LocalidadId");

                    b.HasIndex("ProfesorId");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Cuota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EstadoCuota")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cuotas");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Localidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreLocalidades")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("localidades");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Profesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<int>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("profesores");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreProvincia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("provincias");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TurnosClase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("turnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Alumno", b =>
                {
                    b.HasOne("Final_Lab4_1.Models.Categoria", "categorias")
                        .WithMany("Alumnos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_Lab4_1.Models.Cuota", "cuotas")
                        .WithMany("Alumnos")
                        .HasForeignKey("CuotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_Lab4_1.Models.Localidad", "localidades")
                        .WithMany("Alumnos")
                        .HasForeignKey("LocalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_Lab4_1.Models.Profesor", "Profesor")
                        .WithMany("Alumnos")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_Lab4_1.Models.Provincia", "provincias")
                        .WithMany("Alumnos")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categorias");

                    b.Navigation("cuotas");

                    b.Navigation("localidades");

                    b.Navigation("Profesor");

                    b.Navigation("provincias");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Profesor", b =>
                {
                    b.HasOne("Final_Lab4_1.Models.Turno", "Turno")
                        .WithMany("Profesores")
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Categoria", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Cuota", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Localidad", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Profesor", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Provincia", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("Final_Lab4_1.Models.Turno", b =>
                {
                    b.Navigation("Profesores");
                });
#pragma warning restore 612, 618
        }
    }
}
