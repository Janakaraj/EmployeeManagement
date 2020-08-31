using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentRepository deptRepo = new DepartmentRepository();
       
        // GET: DepartmentController
        public ActionResult Index()
        {
            IEnumerable<Department> dept = deptRepo.GetDepartments();
            return View(dept);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            Department dept = deptRepo.GetDepartmentById(id);
            return View(dept);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department collection)
        {
            try
            {
                var d = deptRepo.GetDepartments().Last();
                collection.DepartmentId = d.DepartmentId+1;
                deptRepo.AddDepartment(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            Department dept = deptRepo.GetDepartmentById(id);
            return View(dept);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department collection)
        {
            try
            {
                deptRepo.EditDepartment(id, (Department)collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            Department dept = deptRepo.GetDepartmentById(id);
            return View(dept);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department collection)
        {
            try
            {
                deptRepo.DeleteDepartment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
