using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training_Management_System.Models;
using Training_Management_System.ViewModel;

namespace Training_Management_System.Controllers
{
    public class TrainingController : Controller
    {
        private readonly TrainingDbContext _context;

        public TrainingController(TrainingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trainings = _context.Training.Include(t => t.Organization);
            return View(trainings.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Organizations = new SelectList(_context.Organizations, "OrganizationId", "Name");
            ViewBag.Employees = new MultiSelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trainingDate = model.TrainingDate.Date;
                var conflict = _context.TrainingEmployees
                    .Any(te => model.SelectedEmployeeIds.Contains(te.EmployeeId.GetValueOrDefault()) &&
                               _context.Training.Any(t => t.TrainingId == te.TrainingId && t.TrainingDate == trainingDate));

                if (conflict)
                {
                    ModelState.AddModelError("", "One or more employees already have training on this date.");
                    ViewBag.Organizations = new SelectList(_context.Organizations, "OrganizationId", "Name");
                    ViewBag.Employees = new MultiSelectList(_context.Employees, "EmployeeId", "Name");
                    return View(model);
                }

                var training = new Training
                {
                    OrganizationId = model.OrganizationId,
                    TrainingDate = model.TrainingDate,
                    Place = model.Place,
                    Purpose = model.Purpose
                };

                _context.Training.Add(training);
                await _context.SaveChangesAsync();

                foreach (var empId in model.SelectedEmployeeIds)
                {
                    _context.TrainingEmployees.Add(new TrainingEmployee
                    {
                        TrainingId = training.TrainingId,
                        EmployeeId = empId
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Organizations = new SelectList(_context.Organizations, "OrganizationId", "Name");
            ViewBag.Employees = new MultiSelectList(_context.Employees, "EmployeeId", "Name");
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var training = _context.Training.Include(t => t.Organization).FirstOrDefault(t => t.TrainingId == id);
            var employees = _context.TrainingEmployees.Where(te => te.TrainingId == id).Select(te => te.Employee).ToList();
            ViewBag.Employees = employees;
            return View(training);
        }

        public IActionResult Delete(int id)
        {
            var training = _context.Training.Find(id);
            if (training != null)
            {
                var employees = _context.TrainingEmployees.Where(te => te.TrainingId == id);
                _context.TrainingEmployees.RemoveRange(employees);
                _context.Training.Remove(training);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
