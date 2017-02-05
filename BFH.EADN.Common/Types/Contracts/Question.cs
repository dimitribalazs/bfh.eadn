using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Question", IsReference = true)]
    public sealed class Question : BaseContract
    {
        [DataMember(Order = 0)]
        public string Text { get; set; }

        [DataMember(Order = 0)]
        public string Hint { get; set; }

        [DataMember(Order = 0)]
        public bool IsYesOrNo { get; set; }

        [DataMember(Order = 0)]
        public QuizType Type { get; set; }

    }
}
