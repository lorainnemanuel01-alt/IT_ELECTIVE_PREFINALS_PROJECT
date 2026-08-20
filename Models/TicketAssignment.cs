using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Models;

[Table("TicketAssignments")]
public class TicketAssignment
{
    [Required] public int TicketId { get; set; }
    [Required] public int EmployeeId { get; set; }
    [Required] public DateTime AssignedAt { get; set; }
    public DateTime? UnassignedAt { get; set; }
    public bool IsPrimary { get; set; }
    [ForeignKey(nameof(TicketId))] public Ticket Ticket { get; set; } = null!;
    [ForeignKey(nameof(EmployeeId))] public Employee Employee { get; set; } = null!;
    [NotMapped] public bool IsActive => UnassignedAt == null;
}
