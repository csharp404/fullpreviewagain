using fullpreviewagain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fullpreviewagain.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            /* SELECT
     d.Id, 
     d.Name AS DepartmentName, 
     m.Name AS ManagerName,
     e.DepartmentId,
     e.Id,
     e.Name,
     e.Salary
 FROM
     department AS d
 INNER JOIN
     managers AS m ON m.DepartmentId = d.Id
 INNER JOIN
     employees AS e ON e.DepartmentId = d.Id*/

           

            var data = (from d in _context.department
                        join m in _context.managers
                        on d.Id equals m.DepartmentId
                        select new Department
                        {
                            manager = m,
                            Id = d.Id,
                            Name = d.Name
                        }).ToList();

            return View(data);
        }
    }
}
