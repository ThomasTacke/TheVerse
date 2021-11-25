
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QingLong.Models;
public class MqttComponentValue {
    public int Id { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public string Type { get; set; }
    public int MqttComponentId { get; set; }
    public MqttComponent MqttComponent { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Timestamp { get; set; }
}
