﻿using BFH.EADN.QuizManagementService.Contracts;
using BFH.EADN.UI.Web.Identity;
using BFH.EADN.UI.Web.Models;
using BFH.EADN.UI.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers
{
    public class SessionController : Controller
    {
        private ApplicationUserManager userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            set
            {
                userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOn(LogOn logonData, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(logonData.UserName, logonData.Password);
                if (user != null)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    Claim roleClaim = ClientProxy.GetProxy<ISession>().LogIn();
                    identity.AddClaim(roleClaim);

                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = logonData.RememberMe }, identity);
                    if(string.IsNullOrEmpty(returnUrl))
                    {
                        returnUrl = "/Topic/Index";
                    }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid user name or password.");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(logonData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}