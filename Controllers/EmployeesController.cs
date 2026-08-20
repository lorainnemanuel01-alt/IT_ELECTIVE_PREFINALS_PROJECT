using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class EmployeesController : Controller
{
    private readonly HelpDeskContext _context;
    public EmployeesController(HelpDeskContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Employees.Include(e => e.Department).AsNoTracking().OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync());
}
