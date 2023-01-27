using Anyar.DAL;
using Anyar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminEmployeeController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public AdminEmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid) { return View(); }

            bool isExsisFullName = await _context.Employees.AnyAsync(c => c.FullName.ToLower().Trim() == employee.FullName.ToLower().Trim());
            bool isExsisProfession = await _context.Employees.AnyAsync(c => c.Professia.ToLower().Trim() == employee.Professia.ToLower().Trim());
            bool isExsisFacebook = await _context.Employees.AnyAsync(c => c.Facebook.ToLower().Trim() == employee.Facebook.ToLower().Trim());
            bool isExsisTwitter = await _context.Employees.AnyAsync(c => c.Twitter.ToLower().Trim() == employee.Twitter.ToLower().Trim());
            bool isExsisInstagram = await _context.Employees.AnyAsync(c => c.Instagram.ToLower().Trim() == employee.Instagram.ToLower().Trim());
            
            if (isExsisFullName)
            {
                ModelState.AddModelError("FullName", "Already exsist");
                return View();
            }
            if (isExsisProfession) { ModelState.AddModelError("Profession", "Already exsist"); }
            if (isExsisFacebook) { ModelState.AddModelError("Facebook", "Already exsist"); }
            if (isExsisTwitter) { ModelState.AddModelError("Twitter", "Already exsist"); }
            if (isExsisInstagram) { ModelState.AddModelError("Instagram", "Already exsist"); }

            if(employee.FormFile is null)
            {
                ModelState.AddModelError("FormFile", "Image can not be null");
                return View();
            }
            if (!employee.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("FormFile", "enter image");
                return View();
            }
            if(employee.FormFile.Length / 1024 / 1024  >= 5)
            {
                ModelState.AddModelError("FormFile", "dont input image big 5MB");
                return View();
            }

            string FileName = Guid.NewGuid() + employee.FormFile.FileName;
            string FolderName = ("/assets/img/team/");
            string FullPath = _env.WebRootPath + FolderName + FileName;
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                employee.FormFile.CopyTo(fileStream);
            }
            employee.Img = FileName;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Employee? employee = _context.Employees.Find(id);
            if (employee == null) { return NotFound(); }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee employee)
        {
            Employee? exsisEmployee = _context.Employees.Find(employee.Id);
            if (exsisEmployee == null) { return NotFound(); }
            if (!ModelState.IsValid)
            {
                return View();
            }

            exsisEmployee.FullName= employee.FullName;
            exsisEmployee.Professia = employee.Professia;
            exsisEmployee.Facebook = employee.Facebook;
            exsisEmployee.Twitter = employee.Twitter;
            exsisEmployee.Instagram = employee.Instagram;


            if (employee.FormFile is null)
            {
                ModelState.AddModelError("FormFile", "Image can not be null");
                return View(employee);
            }
            if (!employee.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("FormFile", "enter image");
                return View(employee);
            }
            if (employee.FormFile.Length / 1024 / 1024 >= 5)
            {
                ModelState.AddModelError("FormFile", "dont input image big 5MB");
                return View(employee);
            }

            string FileName = Guid.NewGuid() + employee.FormFile.FileName;
            string FolderName = ("/assets/img/team/");
            string FullPath = _env.WebRootPath + FolderName + FileName;
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                employee.FormFile.CopyTo(fileStream);
            }
            exsisEmployee.Img = FileName;


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Employee? employee = _context.Employees.Find(id);
            if (employee == null) { return NotFound(); }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
