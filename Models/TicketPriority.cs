using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Models;

[Table("TicketPriorities")]
public class TicketPriority
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public int SortOrder { get; set; }
    [Required] public int ResponseHours { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
