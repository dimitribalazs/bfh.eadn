using BFH.EADN.UI.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BFH.EADN.UI.Web.Identity
{
    public class ApplicationClaimFactory : ClaimsIdentityFactory<User, Guid>
    {
        public override Task<ClaimsIdentity> CreateAsync(UserManager<User, Guid> manager, User user, string authenticationType)
        {
            return Task<ClaimsIdentity>.Factory.StartNew(() =>
            {
                return new ClaimsIdentity(
                    new[] { 
                          // adding following 2 claim just for supporting default anti-forgery provider
                          new Claim(ClaimTypes.NameIdentifier, user.UserName),
                          new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                          new Claim(ClaimTypes.Name, user.UserName),
                    },
                    DefaultAuthenticationTypes.ApplicationCookie
                );
            });
        }
    }
}