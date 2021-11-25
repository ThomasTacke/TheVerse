
using System.ComponentModel.DataAnnotations;

namespace QingLong.Models;
public class DeviceType {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Device> Devices { get; set; }
}
