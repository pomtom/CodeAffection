using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeAffection.Models;
using Microsoft.Extensions.Configuration;

namespace CodeAffection.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly IConfiguration _configuration;

        public EmployeesController(EmployeeDbContext context, IConfiguration configuration)
        {
            _context = context;
            this._configuration = configuration;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/AddOrEdit/5
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {

            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                if (_context.Employees == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }

        }

        // POST: Employees/AddOrEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,City,Phone,BirthDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (employee.Id == 0)
                    {
                        _context.Add(employee);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'EmployeeDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
