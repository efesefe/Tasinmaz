﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tasinmaz_v3;

namespace tasinmaz_v3.Migrations
{
    [DbContext(typeof(TasinmazDbContext))]
    [Migration("20210726124449_Schema2")]
    partial class Schema2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("tasinmaz_v3.Models.Durumlar", b =>
                {
                    b.Property<int>("durumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("durumAdi")
                        .HasColumnType("text");

                    b.HasKey("durumID");

                    b.ToTable("Durumlar");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Il", b =>
                {
                    b.Property<int>("ilID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ilName")
                        .HasColumnType("text");

                    b.HasKey("ilID");

                    b.ToTable("iller");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Ilce", b =>
                {
                    b.Property<int>("ilceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ilID")
                        .HasColumnType("integer");

                    b.Property<string>("ilceName")
                        .HasColumnType("text");

                    b.HasKey("ilceID");

                    b.HasIndex("ilID");

                    b.ToTable("ilceler");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Islemler", b =>
                {
                    b.Property<int>("islemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("islemAdi")
                        .HasColumnType("text");

                    b.HasKey("islemID");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Kullanici", b =>
                {
                    b.Property<int>("kullaniciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("isim")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<bool>("role")
                        .HasColumnType("boolean");

                    b.Property<string>("soyisim")
                        .HasColumnType("text");

                    b.HasKey("kullaniciID");

                    b.ToTable("kullanicilar");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Log", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("aciklama")
                        .HasColumnType("text");

                    b.Property<int>("durumID")
                        .HasColumnType("integer");

                    b.Property<string>("ip")
                        .HasColumnType("text");

                    b.Property<int>("islemID")
                        .HasColumnType("integer");

                    b.Property<int>("kullaniciID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("tarihSaat")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("durumID");

                    b.HasIndex("islemID");

                    b.HasIndex("kullaniciID");

                    b.ToTable("loglar");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Mahalle", b =>
                {
                    b.Property<int>("mahalleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ilceID")
                        .HasColumnType("integer");

                    b.Property<string>("mahalleName")
                        .HasColumnType("text");

                    b.HasKey("mahalleID");

                    b.HasIndex("ilceID");

                    b.ToTable("mahalleler");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Tasinmaz", b =>
                {
                    b.Property<int>("tasinmazID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Ada")
                        .HasColumnType("integer");

                    b.Property<int>("Parsel")
                        .HasColumnType("integer");

                    b.Property<string>("adres")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<int>("mahalleID")
                        .HasColumnType("integer");

                    b.Property<string>("nitelik")
                        .HasColumnType("text");

                    b.HasKey("tasinmazID");

                    b.HasIndex("mahalleID");

                    b.ToTable("tasinmazlar");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Ilce", b =>
                {
                    b.HasOne("tasinmaz_v3.Models.Il", "Il")
                        .WithMany()
                        .HasForeignKey("ilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Il");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Log", b =>
                {
                    b.HasOne("tasinmaz_v3.Models.Durumlar", "Durumlar")
                        .WithMany()
                        .HasForeignKey("durumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tasinmaz_v3.Models.Islemler", "Islemler")
                        .WithMany()
                        .HasForeignKey("islemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tasinmaz_v3.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("kullaniciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Durumlar");

                    b.Navigation("Islemler");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Mahalle", b =>
                {
                    b.HasOne("tasinmaz_v3.Models.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("ilceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ilce");
                });

            modelBuilder.Entity("tasinmaz_v3.Models.Tasinmaz", b =>
                {
                    b.HasOne("tasinmaz_v3.Models.Mahalle", "Mahalle")
                        .WithMany()
                        .HasForeignKey("mahalleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mahalle");
                });
#pragma warning restore 612, 618
        }
    }
}
