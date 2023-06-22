using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Data;
using VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //public static ITempDataDictionary TempData { get; set; }

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        /*public async Task<IActionResult> Index()
        {
              return _context.Contacts != null ? 
                          View(await _context.Contacts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Contacts'  is null.");
        }*/
        public async Task<IActionResult> Index()
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contacts' is null.");
            }

            var contactsList = await _context.Contacts.ToListAsync();
            var viewModel = new ContactsIndexViewModel
            {
                ContactsList = contactsList,
                SelectedContacts = new List<string>()
            };
            return View(viewModel);
        }


        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacts);
        }

        // GET: Contacts/SelectRecipients
        public IActionResult SelectedRecipients()
        {
            List<Contacts> contactsList = _context.Contacts.ToList<Contacts>();
            var contactNamesList = contactsList.Select(x => x.Name).ToList();

            ViewData["ContactsList"] = contactNamesList;
            ViewData["SelectedContacts"] = new List<string>();

            return View();
        }


        // POST: Contacts/SelectRecipients
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult SelectRecipients([Bind("SelectedContacts")] List<string> selectedContacts)
        {
            if (selectedContacts != null)
            {
                TempData["SelectedContacts"] = string.Join(',', selectedContacts);
            }
            // Convert to string array for use in GetContactsList() method
            var recipientList = TempData["SelectedContacts"].ToString().Split(',');
            TempData["SelectedContacts"] = recipientList;

            // Keep TempData["SelectedContacts"] for use in next request
            TempData.Keep("SelectedContacts");

            // Get phone numbers of selected contacts and save to TempData
            GetContactsList();
            if (TempData["PhoneNumbersList"] == null)
            {
                return Problem("TempData['PhoneNumbersList'] is null.");
            }
            
            return RedirectToAction("Create", "Messages");

            
        }   

        // Get Phone numbers of selected contacts and return as a List<string> and save to TempData
        public List<string> GetContactsList()
        {
            // Get selected contacts from TempData
            var selectedContacts = TempData["SelectedContacts"] as string[];
            // Get phone numbers of selected contacts
            var contactsList = _context.Contacts.ToList<Contacts>();
            var phoneNumbersList = contactsList.Where(x => selectedContacts.Contains(x.Name)).Select(x => x.Phone).ToList();

            // Save phone numbers to TempData
            TempData["PhoneNumbersList"] = phoneNumbersList;

            // Keep TempData["PhoneNumbersList"] for use in next request
            TempData.Keep("PhoneNumbersList");

            return phoneNumbersList;
        }
         


        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            return View(contacts);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone")] Contacts contacts)
        {
            if (id != contacts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.Id))
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
            return View(contacts);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contacts'  is null.");
            }
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts != null)
            {
                _context.Contacts.Remove(contacts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
          return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Method to Upload & Read Excel File and save to Contacts table (Comment in details)
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected");
            }

            // Get file extension
            var fileExtension = Path.GetExtension(file.FileName);

            // Check if file is an Excel file
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                return Content("File is not an Excel file");
            }

            // Get file path
            var filePath = Path.GetTempFileName();

            // Save file to path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Open file and read data
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Get data from Excel file
                    var contactsList = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    }).Tables[0];

                    // Get data from Excel file and save to Contacts table
                    // the first row of the Excel file is the header row
                    // the first column of the Excel file is the Name column
                    // the second column of the Excel file is the Phone column
                    // the first row of the Excel file is skipped

                    for (int i = 0; i < contactsList.Rows.Count; i++)
                    {
                        var contact = new Contacts
                        {
                            Name = contactsList.Rows[i][0].ToString(),
                            Phone = contactsList.Rows[i][1].ToString()
                        };

                        _context.Contacts.Add(contact);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index)); 
        }
    }

}
