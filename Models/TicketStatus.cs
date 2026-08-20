using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Models;

[Table("TicketStatuses")]
public class TicketStatus
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public bool IsClosed { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
