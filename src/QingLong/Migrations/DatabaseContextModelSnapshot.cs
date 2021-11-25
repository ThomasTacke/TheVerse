﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QingLong;

#nullable disable

namespace QingLong.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("QingLong.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("DeviceTypeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("device_type_id");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("display_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<int?>("RoomId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("room_id");

                    b.HasKey("Id")
                        .HasName("pk_devices");

                    b.HasIndex("DeviceTypeId")
                        .HasDatabaseName("ix_devices_device_type_id");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_devices_room_id");

                    b.ToTable("devices", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_device_types");

                    b.ToTable("device_types", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.MqttComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("device_id");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("topic");

                    b.Property<int?>("TypeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("type_id");

                    b.HasKey("Id")
                        .HasName("pk_mqtt_components");

                    b.HasIndex("DeviceId")
                        .HasDatabaseName("ix_mqtt_components_device_id");

                    b.HasIndex("TypeId")
                        .HasDatabaseName("ix_mqtt_components_type_id");

                    b.ToTable("mqtt_components", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.MqttComponentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_mqtt_component_types");

                    b.ToTable("mqtt_component_types", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.MqttComponentValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("MqttComponentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("mqtt_component_id");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT")
                        .HasColumnName("timestamp");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_mqtt_component_values");

                    b.HasIndex("MqttComponentId")
                        .HasDatabaseName("ix_mqtt_component_values_mqtt_component_id");

                    b.ToTable("mqtt_component_values", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("short_name");

                    b.HasKey("Id")
                        .HasName("pk_rooms");

                    b.ToTable("rooms", (string)null);
                });

            modelBuilder.Entity("QingLong.Models.Device", b =>
                {
                    b.HasOne("QingLong.Models.DeviceType", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .HasConstraintName("fk_devices_device_types_device_type_id");

                    b.HasOne("QingLong.Models.Room", "Room")
                        .WithMany("Devices")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("fk_devices_rooms_room_id");

                    b.Navigation("DeviceType");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("QingLong.Models.MqttComponent", b =>
                {
                    b.HasOne("QingLong.Models.Device", "Device")
                        .WithMany("MqttComponents")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("fk_mqtt_components_devices_device_id");

                    b.HasOne("QingLong.Models.MqttComponentType", "Type")
                        .WithMany("MqttComponents")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("fk_mqtt_components_mqtt_component_types_type_id");

                    b.Navigation("Device");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("QingLong.Models.MqttComponentValue", b =>
                {
                    b.HasOne("QingLong.Models.MqttComponent", "MqttComponent")
                        .WithMany("Values")
                        .HasForeignKey("MqttComponentId")
                        .HasConstraintName("fk_mqtt_component_values_mqtt_components_mqtt_component_id");

                    b.Navigation("MqttComponent");
                });

            modelBuilder.Entity("QingLong.Models.Device", b =>
                {
                    b.Navigation("MqttComponents");
                });

            modelBuilder.Entity("QingLong.Models.DeviceType", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("QingLong.Models.MqttComponent", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("QingLong.Models.MqttComponentType", b =>
                {
                    b.Navigation("MqttComponents");
                });

            modelBuilder.Entity("QingLong.Models.Room", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
