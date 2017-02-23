using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    public class QuizSelection
    {
        public string Topic { get; set; }
        public List<Quiz> Quizzes { get; set; }
    }
}
