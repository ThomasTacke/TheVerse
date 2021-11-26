using dotenv.net;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong;
public class DatabaseContext : DbContext {
    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    public virtual DbSet<Device> Devices { get; set; }
    public virtual DbSet<DeviceType> DeviceTypes { get; set; }
    public virtual DbSet<MqttComponent> MqttComponents { get; set; }
    public virtual DbSet<MqttComponentType> MqttComponentTypes { get; set; }
    public virtual DbSet<MqttComponentValue> MqttComponentValues { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        DotEnv.Load();
        if (!optionsBuilder.IsConfigured) {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLITE_DB_PATH"))) {
                optionsBuilder.UseSqlite("DataSource=db/the-verse.sqlite3")
                              .UseSnakeCaseNamingConvention();
            } else {
                optionsBuilder.UseSqlite($"Data Source={Environment.GetEnvironmentVariable("SQLITE_DB_PATH")}")
                              .UseSnakeCaseNamingConvention();
            }
        }
    }
}
