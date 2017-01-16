using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract]
    public class Quiz
    {
        public string Text { get; set; }
        public QuizType Type { get; set; }
    }
}
