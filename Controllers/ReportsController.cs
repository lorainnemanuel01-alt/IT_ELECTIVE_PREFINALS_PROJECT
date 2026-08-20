using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using IT_ELECTIVE_PREFINALS_PROJECT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class ReportsController : Controller
{
    private readonly HelpDeskContext _context;
    public ReportsController(HelpDeskContext context) => _context = context;

    public async Task<IActionResult> EmployeeWorkload()
    {
        var rows = await _context.Employees.Where(e => e.IsActive)
            .Select(e => new EmployeeWorkloadViewModel
            {
                Employee = e.FirstName + " " + e.LastName,
                Department = e.Department.Name,
                UnresolvedTicketCount = e.TicketAssignments.Count(a => a.UnassignedAt == null && !a.Ticket.Status.IsClosed)
            }).OrderBy(x => x.Department).ThenBy(x => x.Employee).AsNoTracking().ToListAsync();
        return View(rows);
    }

    public async Task<IActionResult> DepartmentWorkload()
    {
        var rows = await _context.Departments
            .Select(d => new DepartmentWorkloadViewModel
            {
                Department = d.Name,
                EmployeeCount = d.Employees.Count(e => e.IsActive),
                UnresolvedTicketCount = d.Employees.SelectMany(e => e.TicketAssignments).Count(a => a.UnassignedAt == null && !a.Ticket.Status.IsClosed)
            }).OrderBy(x => x.Department).AsNoTracking().ToListAsync();
        return View(rows);
    }

    public async Task<IActionResult> UnassignedTickets()
    {
        var rows = await _context.Tickets
            .Where(t => !t.Assignments.Any(a => a.UnassignedAt == null))
            .Select(t => new UnassignedTicketViewModel
            {
                TicketId = t.Id, Subject = t.Subject, Customer = t.Customer.CompanyName,
                Priority = t.Priority.Name, Status = t.Status.Name, CreatedAt = t.CreatedAt
            }).OrderByDescending(x => x.CreatedAt).AsNoTracking().ToListAsync();
        return View(rows);
    }

    public async Task<IActionResult> MultipleAssignees()
    {
        var tickets = await _context.Tickets
            .Where(t => t.Assignments.Count(a => a.UnassignedAt == null) > 1)
            .Select(t => new
            {
                t.Id, t.Subject,
                ActiveAssignees = t.Assignments.Where(a => a.UnassignedAt == null)
                    .Select(a => a.Employee.FirstName + " " + a.Employee.LastName).ToList()
            }).OrderBy(x => x.Id).AsNoTracking().ToListAsync();
        var rows = tickets.Select(t => new MultipleAssigneeViewModel
        {
            TicketId = t.Id, Subject = t.Subject,
            NumberOfActiveAssignees = t.ActiveAssignees.Count,
            Assignees = string.Join(", ", t.ActiveAssignees)
        }).ToList();
        return View(rows);
    }

    public async Task<IActionResult> PrimaryAssignee()
    {
        var rows = await _context.Tickets
            .Select(t => new PrimaryAssigneeViewModel
            {
                TicketId = t.Id,
                Subject = t.Subject,
                PrimaryAssignee = t.Assignments.Where(a => a.IsPrimary && a.UnassignedAt == null)
                    .Select(a => a.Employee.FirstName + " " + a.Employee.LastName).FirstOrDefault() ?? "Unassigned"
            }).OrderBy(x => x.TicketId).AsNoTracking().ToListAsync();
        return View(rows);
    }

    public async Task<IActionResult> CategoryHierarchy()
    {
        var rows = await _context.TicketCategories
            .Select(c => new CategoryHierarchyViewModel { Category = c.Name, ParentCategory = c.ParentCategory == null ? "—" : c.ParentCategory.Name })
            .OrderBy(x => x.ParentCategory).ThenBy(x => x.Category).AsNoTracking().ToListAsync();
        return View(rows);
    }
}
