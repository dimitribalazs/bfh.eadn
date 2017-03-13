using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Attributes
{
    public class ErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            context.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(ex)
            };
            context.ExceptionHandled = true;
        }
    }
}