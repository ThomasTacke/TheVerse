
using System.ComponentModel.DataAnnotations;

namespace QingLong.Models;
public class Room {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string ShortName { get; set; }
    public List<Device> Devices { get; set; }
}
