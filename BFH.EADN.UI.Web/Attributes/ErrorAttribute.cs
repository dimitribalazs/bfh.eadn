using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Attributes
{
    /// <summary>
    /// Error handling for server side errors
    /// </summary>
    public class ErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Gets executed if exception happened
        /// </summary>
        /// <param name="context">exception context</param>
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