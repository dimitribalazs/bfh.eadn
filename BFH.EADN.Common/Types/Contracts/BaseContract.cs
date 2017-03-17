using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// BaseContract WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "BaseContract", IsReference = true)]
    public abstract class BaseContract
    {
        /// <summary>
        /// Database Id of this element
        /// </summary>
        [DataMember(Order = 0, Name = "Id")]
        public Guid Id { get; set; }
    }
}
