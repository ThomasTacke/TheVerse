
using System.ComponentModel.DataAnnotations;

namespace QingLong.Models;
public class MqttComponent {
    public int Id { get; set; }
    [Required]
    public string Topic { get; set; }
    public int MqttComponentTypeId { get; set; }
    public MqttComponentType Type { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public List<MqttComponentValue> Values { get; set; }
}
