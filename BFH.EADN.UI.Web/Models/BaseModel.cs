using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models
{
    /// <summary>
    /// Base view model
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Database Id
        /// </summary>
        public Guid Id { get; set; }
    }
}