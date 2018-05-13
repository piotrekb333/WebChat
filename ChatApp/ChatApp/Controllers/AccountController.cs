﻿using ChatApp.Models;
using ChatApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            
            UserRepository repo = new UserRepository();
            var usr=repo.GetUsersByField("username", model.Username);
            if (usr.Count > 0)
                return View();
            repo.Insert(new Models.Entities.User { Password = Helper.GetHashString(model.Password), UserName = model.Username ,RegisterDate=DateTime.Now.ToUniversalTime()});
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            UserRepository repo = new UserRepository();
            var usr = repo.GetUsersByField("userName", model.Username);
            if (usr.Count == 0)
                return View();
            if (usr[0].Password == Helper.GetHashString(model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);

                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(20), false,string.Empty);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}