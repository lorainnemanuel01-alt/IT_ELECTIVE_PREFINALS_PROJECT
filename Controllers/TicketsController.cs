using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class TicketsController : Controller
{
    private readonly HelpDeskContext _context;
    public TicketsController(HelpDeskContext context) => _context = context;

    public async Task<IActionResult> Index(string? status, string? priority)
    {
        var query = _context.Tickets
            .Include(t => t.Customer).Include(t => t.Category).Include(t => t.Priority).Include(t => t.Status)
            .AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(status)) query = query.Where(t => t.Status.Name == status);
        if (!string.IsNullOrWhiteSpace(priority)) query = query.Where(t => t.Priority.Name == priority);
        ViewBag.Statuses = await _context.TicketStatuses.AsNoTracking().OrderBy(s => s.Name).Select(s => s.Name).ToListAsync();
        ViewBag.Priorities = await _context.TicketPriorities.AsNoTracking().OrderBy(p => p.SortOrder).Select(p => p.Name).ToListAsync();
        return View(await query.OrderByDescending(t => t.CreatedAt).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Customer).Include(t => t.Category).ThenInclude(c => c.ParentCategory)
            .Include(t => t.Priority).Include(t => t.Status)
            .Include(t => t.Assignments).ThenInclude(a => a.Employee)
            .Include(t => t.Comments).ThenInclude(c => c.Employee)
            .Include(t => t.Attachments)
            .Include(t => t.TicketTags).ThenInclude(tt => tt.Tag)
            .AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return ticket is null ? NotFound() : View(ticket);
    }
}
