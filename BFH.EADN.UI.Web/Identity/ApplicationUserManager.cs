using BFH.EADN.UI.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Identity
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            return new ApplicationUserManager(new ApplicationUserStore());
        }

        public ApplicationUserManager(IUserStore<User, Guid> store)
            : base(store)
        {
            ClaimsIdentityFactory = new ApplicationClaimFactory();
        }
    }
}