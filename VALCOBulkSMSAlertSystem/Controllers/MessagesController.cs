﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;
using VALCOBulkSMSAlertSystem.Authorization;
using VALCOBulkSMSAlertSystem.Data;
using VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Services;
using static VALCOBulkSMSAlertSystem.Authorization.Operations;

namespace VALCOBulkSMSAlertSystem.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HubtelSmsService _hubtelSmsService;
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<VALCOUser> UserManager { get; }

        //private readonly ContactsService _contactsService;

        public MessagesController(
            ApplicationDbContext context, 
            HubtelSmsService hubtelSmsService, 
            ContactsService contactsService, 
            IAuthorizationService authorizationService,
            UserManager<VALCOUser> userManager)
        {
            _context = context;
            _hubtelSmsService = hubtelSmsService;
            AuthorizationService = authorizationService;
            UserManager = userManager;
            //_contactsService = contactsService;
        }

        public IList<Messages> Contact { get; set; }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            // Authorization: check if user is authorized to see list of messages
            var messages = from m in _context.Messages
                           select m;

            if (messages == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only messages written by the User will be displayed
            if (!isAuthorized)
            {
                messages = messages.Where(m => m.UserID == currentUserId);
                return View(await messages.ToListAsync());
            }

            return View(await messages.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Sender,Recipient,Status,Date,AspnetUsers")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                // Authorization: check if user is authorized to create messages
                messages.UserID = UserManager.GetUserId(User);

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                            User, messages,
                                                            AllowOperations.Create);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                messages.Date = DateTime.Now.ToString();
                messages.Sender = User.Identity.Name;
                
                // Get single recipent from input form
                if (messages.Recipient != null)
                {
                    var msgStatus = await _hubtelSmsService.HubtelSmsServiceApi(messages.Recipient, messages.Content);
                    messages.Status = msgStatus.ToLowerInvariant() == "failed" ? "Failed" : "Sent";
                    _context.Add(messages);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // Get phone numbers list from TempData
                string[]? phoneNumbersArray = TempData["PhoneNumbersList"] as string[];
                List<string>? phoneNumbersList = phoneNumbersArray?.ToList();

                if (phoneNumbersList == null || phoneNumbersList.Count == 0)
                {
                    ModelState.AddModelError("", "Please select at least one contact for sending this message.");
                    return View(messages);
                }

                bool sendSuccess = true;
                foreach (var phoneNumber in phoneNumbersList)
                {
                    var msgStatus = await _hubtelSmsService.HubtelSmsServiceApi(phoneNumber, messages.Content);
                    if (msgStatus.ToLowerInvariant() == "failed")
                    {
                        sendSuccess = false;
                    }
                }

                // convert phone numbers list to string and save to database
                string phoneNumbersString = string.Join(", ", phoneNumbersList);
                messages.Recipient = phoneNumbersString;

                messages.Status = sendSuccess ? "Sent" : "Failed";
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(messages);
        }



        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Sender,Recipient,Status,Date,AspnetUsers")] Messages messages)
        {
            if (id != messages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.Id))
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
            return View(messages);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Messages'  is null.");
            }
            var messages = await _context.Messages.FindAsync(id);
            if (messages != null)
            {
                _context.Messages.Remove(messages);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(int id)
        {
          return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        // For Dashboard Views
        public IActionResult Dashboard() => View();

        public IActionResult Tab1(string subTab)
        {
            switch (subTab)
            {
                case "Overview":
                    return View("Overview");
                default:
                    // Default to Overview view
                    return View("Overview");
            }
        }

        public IActionResult Tab2(string subTab)
        {
            switch (subTab)
            {
                case "Analytics":
                    return View("Analytics");
                default:
                    // Default to Analytics view
                    return View("Analytics");
            }
        }

        public IActionResult Tab3(string subTab)
        {
            switch (subTab)
            {
                case "Create":
                    return View("Create");
                case "MessageTemplates":
                    return View("MessageTemplates");
                case "MessageHistory":
                    return View("MessageHistory");
                default:
                    // Default to CreateMessage view
                    return View("Create");
            }
        }

    }
}
