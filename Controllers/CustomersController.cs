using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class CustomersController : Controller
{
    private readonly HelpDeskContext _context;
    public CustomersController(HelpDeskContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Customers.AsNoTracking().OrderBy(c => c.CompanyName).ToListAsync());
}
