using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactBook.BusinessLayer.Services;
using ContactBook.BusinessLayer.ViewModels;
using ContactBook.DomainModal;

namespace ContactBook.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ContactService contactService = new ContactService();
        public ActionResult ContactIndex()
        {
            var contacts = contactService.GetAllContact();
            return View(contacts);
        }

        public ActionResult ContactAdd()
        {
            return PartialView("~/Views/Home/ContactAdd.cshtml");
        }

        [HttpPost]
        public ActionResult ContactAdd(ContactViewModel contactViewModel)
        {
            contactService.CreateContact(contactViewModel);
            return RedirectToAction("ContactIndex", "Home");
        }

        public ActionResult ContactDelete(Int32 contactId)
        {
            contactService.DeleteContact(contactId);
            return RedirectToAction("ContactIndex");
        }

        public ActionResult ContactEdit(Int32 id)
        {
            var contact = contactService.GetContactById(id);
            return PartialView("~/Views/Home/ContactEdit.cshtml", contact);
        }

        [HttpPost]
        public ActionResult ContactEdit(ContactViewModel contactViewModel)
        {
            contactService.UpdateContact(contactViewModel);
            return RedirectToAction("ContactIndex");
        }

	}
}