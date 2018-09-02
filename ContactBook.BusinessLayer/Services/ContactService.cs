using ContactBook.BusinessLayer.ViewModels;
using ContactBook.DomainModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContactBook.BusinessLayer.Services
{
    public class ContactService
    {
        public List<ContactViewModel> GetAllContact()
        {
            using (ContactBookEntities db = new ContactBookEntities())
            {
                List<ContactViewModel> contactViewModelList = new List<ContactViewModel>();
                var list = db.Contacts.OrderBy(x => x.FirstName);
                foreach (var item in list)
                {
                    ContactViewModel contactViewModel = new ContactViewModel();
                    contactViewModel.Id = item.Id;
                    contactViewModel.FirstName = item.FirstName;
                    contactViewModel.LastName = item.LastName;
                    contactViewModel.Gender = item.Gender;
                    contactViewModel.MobileNumber = item.MobileNumber;
                    contactViewModel.City = item.City;
                    contactViewModelList.Add(contactViewModel);
                }
                return contactViewModelList;
            }
        }

        public ContactViewModel GetContactById(Int32 contactId)
        {
            using (ContactBookEntities db = new ContactBookEntities())
            {
                var contact = db.Contacts.Where(x => x.Id == contactId).FirstOrDefault();
                ContactViewModel contactViewModel = new ContactViewModel();
                contactViewModel.Id = contact.Id;
                contactViewModel.FirstName = contact.FirstName;
                contactViewModel.LastName = contact.LastName;
                contactViewModel.Gender = contact.Gender;
                contactViewModel.MobileNumber = contact.MobileNumber;
                contactViewModel.City = contact.City;
                return contactViewModel;
            }
        }


        public void CreateContact(ContactViewModel contactViewModel)
        {
            using (ContactBookEntities db = new ContactBookEntities())
            {
                Contact contact = new Contact();
                contact.FirstName = contactViewModel.FirstName;
                contact.LastName = contactViewModel.LastName;
                contact.Gender = contactViewModel.Gender;
                contact.MobileNumber = contactViewModel.MobileNumber;
                contact.City = contactViewModel.City;
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        public void DeleteContact(Int32 contactId)
        {
            using (ContactBookEntities db = new ContactBookEntities())
            {
                var contact = db.Contacts.Where(x => x.Id == contactId).FirstOrDefault();
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
        }

        public void UpdateContact(ContactViewModel contactViewModel)
        {
            using (ContactBookEntities db = new ContactBookEntities())
            {
                var contact = db.Contacts.Where(x => x.Id == contactViewModel.Id).FirstOrDefault();
                contact.FirstName = contactViewModel.FirstName;
                contact.LastName = contactViewModel.LastName;
                contact.MobileNumber = contactViewModel.MobileNumber;
                contact.City = contactViewModel.City;
                contact.Gender = contactViewModel.Gender;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


    }
}
