using EmployeeTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            // Query to get all employees that are not soft-deleted
            var employees = from e in _context.employee
                            .Include(e => e.People)
                            .Where(e => !e.IsDeleted) // Filter out soft-deleted employees
                            select e;

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.People.Email.Contains(searchString)
                                               || e.EmployeeCode.Contains(searchString));
            }

            // Execute the query and return the view
            return View(employees.ToList());
        }

        public IActionResult EmployeeForm()
        {
            var positions = _context.positions.ToList();

            var model = new EmployeeFormViewModel
            {
                Positions = positions
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeForm(EmployeeFormViewModel employeeFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = await _context.people.Where(p => p.Email == employeeFormViewModel.Email).FirstOrDefaultAsync();

                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "The email address is already in use.");
                    employeeFormViewModel.Positions = _context.positions.ToList();
                    return View(employeeFormViewModel);
                }

                // Create a new People entry
                var people = new People
                {
                    FirstName = employeeFormViewModel.FirstName,
                    MiddleName = employeeFormViewModel.MiddleName,
                    LastName = employeeFormViewModel.LastName,
                    Address = employeeFormViewModel.Address,
                    Email = employeeFormViewModel.Email
                };

                // Add People to the context and save changes to generate the PersonId
                _context.people.Add(people);
                await _context.SaveChangesAsync();

                var utcStartDate = employeeFormViewModel.JobStartDate.ToUniversalTime();
                var utcEndDate = employeeFormViewModel.JobEndDate.ToUniversalTime();

                var employee = new Employee
                {
                    PersonId = people.PersonId,
                    PositionId = employeeFormViewModel.PositionId,
                    Salary = employeeFormViewModel.Salary,
                    StartDate = utcStartDate,
                    EndDate = utcEndDate,
                    EmployeeCode = employeeFormViewModel.EmployeeCode,
                    IsDisable = employeeFormViewModel.IsDisable
                };

                // Add Employee to the context and save changes
                _context.employee.Add(employee);
                await _context.SaveChangesAsync();

                var jobHistory = new EmployeeJobHistories
                {
                    EmployeeId = employee.EmployeeId,
                    PositionId = employeeFormViewModel.PositionId,
                    StartDate = utcStartDate,
                    EndDate = utcEndDate,
                };

                _context.employeeJobHistories.Add(jobHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // If ModelState is not valid, re-populate the ViewModel and return the view
            employeeFormViewModel.Positions = _context.positions.ToList();
            return View(employeeFormViewModel);
        }


        //To edit
        public async Task<IActionResult> EditEmployee(int id)
        {

            var employee = await _context.employee
                .Include(e => e.People)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            var model = new EmployeeFormViewModel
            {
                // Add EmployeeId to the view model
                FirstName = employee.People.FirstName,
                MiddleName = employee.People.MiddleName,
                LastName = employee.People.LastName,
                Address = employee.People.Address,
                Email = employee.People.Email,
                PositionId = employee.PositionId,
                Salary = employee.Salary,
                JobStartDate = employee.StartDate,
                JobEndDate = employee.EndDate,
                EmployeeCode = employee.EmployeeCode,
                IsDisable = employee.IsDisable,
                Positions = await _context.positions.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, EmployeeFormViewModel employeeFormViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var employee = await _context.employee
                        .Include(e => e.People)
                        .FirstOrDefaultAsync(e => e.EmployeeId == id);

                    if (employee == null)
                    {
                        return NotFound();
                    }

                    var people = employee.People;

                    // Update People entry
                    people.FirstName = employeeFormViewModel.FirstName;
                    people.MiddleName = employeeFormViewModel.MiddleName;
                    people.LastName = employeeFormViewModel.LastName;
                    people.Address = employeeFormViewModel.Address;
                    people.Email = employeeFormViewModel.Email;


                    // Update Employee entry
                    employee.PositionId = employeeFormViewModel.PositionId;
                    employee.Salary = employeeFormViewModel.Salary;
                    employee.StartDate = employeeFormViewModel.JobStartDate.ToUniversalTime();
                    employee.EndDate = employeeFormViewModel.JobEndDate.ToUniversalTime();
                    employee.EmployeeCode = employeeFormViewModel.EmployeeCode;
                    employee.IsDisable = employeeFormViewModel.IsDisable;

                    _context.Update(people);
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return RedirectToAction("Index");
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // Re-populate the positions in the ViewModel
            employeeFormViewModel.Positions = await _context.positions.ToListAsync();
            return View(employeeFormViewModel);

        }


        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.employee
                .Include(e => e.People)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            // Soft delete the employee
            employee.IsDeleted = true;

            // Mark the entity as modified
            _context.Update(employee);
            await _context.SaveChangesAsync();

            return View("Index"); // Respond with 200 OK for successful deletion
        }

        public IActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition(Positions positions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(positions);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // Log validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(positions);
        }

        public async Task<IActionResult> EmployeeHistory(int id)
        {
            var employee = await _context.employee
                .Include(e => e.People)
                .Include(e => e.EmployeeJobHistories)
                .ThenInclude(j => j.Position)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        public async Task<IActionResult> EditEmployeeJobHistory(int id)
        {
            // Retrieve the job history to be edited
            var jobHistory = await _context.employeeJobHistories
                .Include(j => j.Position)  // Include related position for dropdown
                .FirstOrDefaultAsync(j => j.EmployeeJobHistoryId == id);

            if (jobHistory == null)
            {
                return NotFound();
            }

            // Retrieve positions for dropdown
            var positions = await _context.positions.ToListAsync();

            // Prepare the view model
            var viewModel = new EditEmployeeJobHistoryModel
            {
                EmployeeJobHistoryId = jobHistory.EmployeeJobHistoryId,
                PositionId = jobHistory.PositionId,
                StartDate = jobHistory.StartDate,
                EndDate = jobHistory.EndDate,
                Positions = positions
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeJobHistory(EditEmployeeJobHistoryModel viewModel)
        {
            
                var existingJobHistory = await _context.employeeJobHistories
                    .FirstOrDefaultAsync(j => j.EmployeeJobHistoryId == viewModel.EmployeeJobHistoryId);

                if (existingJobHistory == null)
                {
                    return NotFound();
                }

                existingJobHistory.PositionId = viewModel.PositionId;
                existingJobHistory.StartDate = DateTime.SpecifyKind(viewModel.StartDate, DateTimeKind.Utc);
                existingJobHistory.EndDate = DateTime.SpecifyKind(viewModel.EndDate, DateTimeKind.Utc);

                _context.Update(existingJobHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction("EmployeeHistory", new { id = existingJobHistory.EmployeeId });
        }

    }
}
