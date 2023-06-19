using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Http;

public class ContactsService
{
    private readonly ApplicationDbContext _context;
    private readonly ITempDataDictionary tempData;

    public ContactsService(ApplicationDbContext context)
    {
        _context = context;
    }

    /*public async Task<List<Contacts>> GetContactsList(List<string>? contactsList, ITempDataDictionary tempData)
    {
        // Get selected contacts from TempData
        if (tempData["SelectedContacts"] is string[] selectedContacts)
        {
            contactsList = selectedContacts.ToList();
        }

        // Get contacts from database based on selected contacts from TempData
        var allContacts = await _context.Contacts.ToListAsync();
        var contacts = from c in allContacts
                       where contactsList.Contains(c.Name)
                       select c;

        return contacts.ToList();
    }*/

    public async Task<List<Contacts>> GetContactsList(List<string>? contactsList)
    {
        //var tempData = httpContextAccessor.HttpContext?.TempData;

        // Get selected contacts from TempData
        if (tempData["SelectedContacts"] is string[] selectedContacts)
        {
            contactsList = selectedContacts.ToList();
        }

        // Get contacts from database based on selected contacts from TempData
        var allContacts = await _context.Contacts.ToListAsync();
        var contacts = from c in allContacts
                       where contactsList.Contains(c.Name)
                       select c;
        return contacts.ToList();
    }

}
