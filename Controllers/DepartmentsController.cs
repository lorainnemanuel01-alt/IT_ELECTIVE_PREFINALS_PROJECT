using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class DepartmentsController : Controller
{
    private readonly HelpDeskContext _context;
    public DepartmentsController(HelpDeskContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Departments.Include(d => d.Employees).AsNoTracking().OrderBy(d => d.Name).ToListAsync());
}
