using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;
using VALCOBulkSMSAlertSystem.Data;
using VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<VALCOUser> _userManager;

        public UserSettingsController(ApplicationDbContext context, UserManager<VALCOUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserSettings
        public async Task<IActionResult> Index()
        {
            // Return a list of all users in the database
            var userSettings = new UserSettings
            {                
                VALCOUsers = await _context.Users.ToListAsync()
            };

            // Use the CorrectUserName method to correct the username
            foreach (var user in userSettings.VALCOUsers)
            {
                user.UserName = CorrectUserName(user.UserName);
            }

            return View(userSettings);                       
        }

        
        // GET: UserSettings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            // get the correct username
            user.UserName = CorrectUserName(user.UserName);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // method to correct username by removing the @ symbol and domain name
        public string CorrectUserName(string userName)
        {
            string correctUserName = userName.Substring(0, userName.IndexOf('@'));

            return correctUserName;
        }

        // GET: UserSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VALCOUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // User creation successful
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If model state is invalid or user creation failed, return the view with the model
            return View(user);
        }


        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber")] VALCOUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
