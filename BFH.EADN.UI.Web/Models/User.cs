using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models
{
    public class User : BaseModel, IUser<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}