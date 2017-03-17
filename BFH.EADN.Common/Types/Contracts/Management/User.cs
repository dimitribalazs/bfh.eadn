using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// User WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "User", IsReference = true)]
    public sealed class User : BaseContract
    {
        /// <summary>
        /// Name of the user
        /// </summary>
        [DataMember(Order = 0, Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Hashed password of the user
        /// </summary>
        [DataMember(Order = 0, Name = "Password")]
        public string Password { get; set; }
    }
}
