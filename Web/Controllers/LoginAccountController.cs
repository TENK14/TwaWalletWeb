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

    public class LoginAccountController : Controller
    {
        public SecurityHelper SecurityHelper = null;

        public LoginAccountController()
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
                    var userDb = context.Users.SingleOrDefault(la => la.Username.Equals(objNewUser.Username) || la.Email.Equals(objNewUser.Email));

                    if (userDb == null)
                    {
                        var keyNew = SecurityHelper.GeneratePassword(10);
                        var password = SecurityHelper.EncodePassword(objNewUser.Password, keyNew);
                        objNewUser.Password = password;
                        objNewUser.CreateDate = DateTime.Now;
                        objNewUser.ModifyDate = DateTime.Now;
                        objNewUser.VCode = keyNew;
                        context.Users.Add(objNewUser);
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

        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogIn(string userName, string password)
        {
            try
            {
                using (var context = new TwaWalletDbContext())
                {
                    //var getUser = (from s in context.ObjRegisterUser where s.UserName == userName || s.EmailId == userName select s).FirstOrDefault();
                    var getUser = context.Users.SingleOrDefault(la => la.Username.Equals(userName) || la.Email.Equals(userName));
                    if (getUser != null)
                    {
                        var hashCode = getUser.VCode;
                        //Password Hasing Process Call Helper Class Method    
                        var encodingPasswordString = SecurityHelper.EncodePassword(password, hashCode);

                        if (getUser.Password.Equals(encodingPasswordString))
                        {
                            return RedirectToAction("Index", "Record");
                        }

                        ////Check Login Detail User Name Or Password    
                        //var query = (from s in context.ObjRegisterUser where (s.UserName == userName || s.EmailId == userName) && s.Password.Equals(encodingPasswordString) select s).FirstOrDefault();
                        //if (query != null)
                        //{
                        //    //RedirectToAction("Details/" + id.ToString(), "FullTimeEmployees");    
                        //    //return View("../Admin/Registration"); url not change in browser    
                        //    return RedirectToAction("Index", "Admin");
                        //}
                        ViewBag.ErrorMessage = "Invallid User Name or Password";
                        return View();
                    }
                    ViewBag.ErrorMessage = "Invallid User Name or Password";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = " Error!!! contact cms@info.in";
                return View();
            }
        }
    }
}