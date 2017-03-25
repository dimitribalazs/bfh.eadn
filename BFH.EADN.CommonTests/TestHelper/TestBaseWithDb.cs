using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BFH.EADN.CommonTests.TestHelper
{
    [TestClass]
    public class TestBaseWithDb
    {
        private static readonly string _quizDbName = "QuizDatabase.mdf";
        private static readonly string _quizDbLogName = "QuizDatabase_log.ldf";

        public static void Init(TestContext context)
        { 
            File.Delete(_quizDbName);
            File.Delete(_quizDbLogName);

            //workaround, wait till init of db happened
            Thread.Sleep(2000);
        }
    }
}
