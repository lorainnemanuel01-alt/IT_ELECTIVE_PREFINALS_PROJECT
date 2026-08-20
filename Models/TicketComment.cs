using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Models;

[Table("TicketComments")]
public class TicketComment
{
    [Key] public int Id { get; set; }
    [Required] public int TicketId { get; set; }
    public int? EmployeeId { get; set; }
    [Required] public string Comment { get; set; } = string.Empty;
    [Required] public DateTime CreatedAt { get; set; }
    public bool IsInternal { get; set; }
    [ForeignKey(nameof(TicketId))] public Ticket Ticket { get; set; } = null!;
    [ForeignKey(nameof(EmployeeId))] public Employee? Employee { get; set; }
}
