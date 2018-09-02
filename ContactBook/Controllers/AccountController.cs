using ContactBook.BusinessLayer.ViewModels;
using ContactBook.DomainModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ContactBook.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
          return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                using (ContactBookEntities db = new ContactBookEntities())
                {
                    var user = db.Users.Where(x => x.UserName.Equals(loginViewModel.UserName) && x.Password.Equals(loginViewModel.Password)).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, loginViewModel.RememberMe);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ContactIndex", "Home");
                        }
                    }

                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
            }
            else
            {
                return View(loginViewModel);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }



	}
}