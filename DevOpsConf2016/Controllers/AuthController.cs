using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using DevOpsConf2016.Models;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login(string url)
        {

            return View();
        }

        public ActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = Models.Login.ValidateUser(login.UserName, login.Password);
                if (user != null)
                {

                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Id = user.Id;
                    serializeModel.FirstName = user.UserInfo.FirstName;
                    serializeModel.LastName = user.UserInfo.LastName;

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(serializeModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.EMail,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View(login);
        }
    }
}