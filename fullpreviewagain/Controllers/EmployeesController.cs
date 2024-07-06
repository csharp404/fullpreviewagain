using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullpreviewagain.Models;

namespace fullpreviewagain.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var emps = (from e in _context.employees
                        join d in _context.department on e.DepartmentId equals d.Id
                        select new Employee
                        {
                            Department = d,
                            Id = e.Id,
                          Name = e.Name,
                          Salary = e.Salary,
                        }).ToList();
            return View(emps);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var department = _context.department.ToList();
            ViewBag.Departments = department;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _context.department.ToList();
            ViewBag.Departments = department;
            var employee = _context.employees.FirstOrDefault(x => x.Id == id);
            return View("Create", employee);
        }
        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            _context.employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.employees.FirstOrDefault(m => m.Id == id);
            if (employee != null)
                _context.employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
