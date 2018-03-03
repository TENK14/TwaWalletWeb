using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwaWallet.Entity;
using TwaWallet.Model;
using TwaWallet.Web.Helpers;

namespace TwaWallet.Web.Controllers
{
    // TODO: dokonči implementaci
    // TODO: http://www.c-sharpcorner.com/UploadFile/145c93/save-password-using-salted-hashing/
    // Alternativní link
    // TODO: https://chandradev819.wordpress.com/2011/04/11/how-to-encrypt-and-decrypt-password-in-asp-net-using-c/

    public class RegistrationController : Controller
    {
        public SecurityHelper SecurityHelper = null;

        public RegistrationController()
        {
            SecurityHelper = new SecurityHelper();
        }

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Registration(LoginAccount objNewUser)
        {
            try
            {
                using (var context = new TwaWalletDbContext()) // TODO: změň DB kontext pro: TwaWalletDbContext
                {
                    var userDb = context.LoginAccounts.SingleOrDefault(la => la.Username.Equals(objNewUser.Username) || la.Email.Equals(objNewUser.Email));

                    if (userDb == null)
                    {
                        var keyNew = SecurityHelper.GeneratePassword(10);
                        var password = SecurityHelper.EncodePassword(objNewUser.Password, keyNew);
                        objNewUser.Password = password;
                        objNewUser.CreateDate = DateTime.Now;
                        objNewUser.ModifyDate = DateTime.Now;
                        objNewUser.VCode = keyNew;
                        context.LoginAccounts.Add(objNewUser);
                        context.SaveChanges();
                        ModelState.Clear();
                        return RedirectToAction("LogIn", "Login");
                    }
                    ViewBag.ErrorMessage = "User Allredy Exixts!!!!!!!!!!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Some exception occured" + e;
                return View();
            }
        }
    }
}