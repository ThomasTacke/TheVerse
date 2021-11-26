
using System.ComponentModel.DataAnnotations;

namespace QingLong.Models;
public class MqttComponentType {
    public int Id { get; set; }
    [Required]
    public string Type { get; set; }
    public List<MqttComponent> MqttComponents { get; set; }
}
