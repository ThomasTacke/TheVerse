using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QingLong.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mqtt_component_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mqtt_component_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    short_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    display_name = table.Column<string>(type: "TEXT", nullable: false),
                    room_id = table.Column<int>(type: "INTEGER", nullable: true),
                    device_type_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_devices", x => x.id);
                    table.ForeignKey(
                        name: "fk_devices_device_types_device_type_id",
                        column: x => x.device_type_id,
                        principalTable: "device_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_devices_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "mqtt_components",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    topic = table.Column<string>(type: "TEXT", nullable: false),
                    type_id = table.Column<int>(type: "INTEGER", nullable: true),
                    device_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mqtt_components", x => x.id);
                    table.ForeignKey(
                        name: "fk_mqtt_components_devices_device_id",
                        column: x => x.device_id,
                        principalTable: "devices",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_mqtt_components_mqtt_component_types_type_id",
                        column: x => x.type_id,
                        principalTable: "mqtt_component_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "mqtt_component_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    value = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    mqtt_component_id = table.Column<int>(type: "INTEGER", nullable: true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mqtt_component_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_mqtt_component_values_mqtt_components_mqtt_component_id",
                        column: x => x.mqtt_component_id,
                        principalTable: "mqtt_components",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_devices_device_type_id",
                table: "devices",
                column: "device_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_devices_room_id",
                table: "devices",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_mqtt_component_values_mqtt_component_id",
                table: "mqtt_component_values",
                column: "mqtt_component_id");

            migrationBuilder.CreateIndex(
                name: "ix_mqtt_components_device_id",
                table: "mqtt_components",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_mqtt_components_type_id",
                table: "mqtt_components",
                column: "type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mqtt_component_values");

            migrationBuilder.DropTable(
                name: "mqtt_components");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "mqtt_component_types");

            migrationBuilder.DropTable(
                name: "device_types");

            migrationBuilder.DropTable(
                name: "rooms");
        }
    }
}
