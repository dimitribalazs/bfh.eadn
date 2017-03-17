using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// Topic WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Topic", IsReference = true)]
    public sealed class Topic : BaseContract
    {
        /// <summary>
        /// Name of the topic
        /// </summary>
        [DataMember(Order = 0, Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the topic
        /// </summary>
        [DataMember(Order = 0, Name = "Description")]
        public string Description { get; set; }
    }
}
