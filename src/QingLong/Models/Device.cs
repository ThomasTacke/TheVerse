
using System.ComponentModel.DataAnnotations;

namespace QingLong.Models;
public class Device {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public int DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }
    public List<MqttComponent> MqttComponents { get; set; }
}
