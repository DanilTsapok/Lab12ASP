using Lab12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lab12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
          
            return View("AddUser");
        }

        public IActionResult AllUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult ShowCompany()
        {
            var companies = _context.Companies.ToList();
            return View(companies);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    for (int i = 0; i < ModelState[key].Errors.Count; i++)
                    {
                        var error = ModelState[key].Errors[i];
                        ModelState.AddModelError(key, error.ErrorMessage);
                    }
                }
                return View("AddUser");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                if(user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }


        public async Task<IActionResult> Edit(int? id)
        { 
            if(id != null)
            {
             var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                return View("EditUser",user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult>Edit(User user)
        {   
             _context.Users.Update(user);
             await _context.SaveChangesAsync();
             return RedirectToAction("AllUsers");   
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}