using fullpreviewagain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fullpreviewagain.Controllers
{
    public class ManagerController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public ManagerController(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public IActionResult Index()
        {
            var data = (from m in _dbContext.managers
                        join d in _dbContext.department
                        on m.DepartmentId equals d.Id
                        select new Manager
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Price = m.Price,
                            department = d
                        }).ToList();


            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.DeptList = _dbContext.department.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Manager manager)
        {
            if (manager != null)
            {
                _dbContext.managers.Add(manager);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.DeptList = _dbContext.department.ToList();
            var manager1 = _dbContext.managers.FirstOrDefault(m => m.Id == id);
            return View("Create", manager1);
        }
        [HttpPost]
        public IActionResult Edit(string id, Manager manager)
        {

            _dbContext.managers.Update(manager);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var delete = _dbContext.managers.FirstOrDefault(manager => manager.Id == id);
            if (delete != null)
            {
                _dbContext.managers.Remove(delete);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
