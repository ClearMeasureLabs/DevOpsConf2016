using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AutoMapper;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Extensions;
using DevOpsConf2016.Models;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
            Mapper.CreateMap<RegisterVM, LoginVM>();
            Mapper.CreateMap<LoginVM, Login>();
            Mapper.CreateMap<RegisterVM, AttendeeVM>();
        }

        // GET: Auth
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM login, string returnUrl)
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
                    serializeModel.Roles = user.Roles.Select(x => x.Description).ToArray();

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
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View(login);

        }

        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<RegisterVM, LoginVM>(vm);


                var id = Guid.NewGuid();
                var newUser = new Login()
                {
                    EMail = user.UserName,
                    Id = id,
                    Password = user.Password.EncodeToSHA1(),
                    UserInfo = new Attendee()
                    {
                        Id = id,
                        FirstName = user.AttendeeInfo.FirstName,
                        LastName = user.AttendeeInfo.LastName,
                        Title = user.AttendeeInfo.Title,
                        Twitter = user.AttendeeInfo.Twitter,
                        SpeakerInfo = new Speaker()
                        {
                            Id = id,
                            BlogURL = vm.SpeakerInfo.BlogURL,
                            Company = vm.SpeakerInfo.Company,
                            CompanyURL = vm.SpeakerInfo.CompanyURL
                        }
                    }
                };

                using (var db = new DevOpsContext())
                {
                    db.Users.Add(newUser);
                    var result = db.SaveChangesAsync().Result;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ModelState.AddModelError("Please make sure ALL required fields are entered.");
            }

            return View(vm);
        }
    }
}