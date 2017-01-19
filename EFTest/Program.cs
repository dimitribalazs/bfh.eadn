using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFTest
{
    class Program
    {
        //private readonly QuizDataContext _context;
        static void Main(string[] args)
        {
            //var quiz = new Quiz
            //{
            //    Text = "Yolo"
            //};
            //using (var db = new QuizDataContext())
            //{
            //    db.Quizzes.Add(quiz);
            //    db.SaveChanges();
            //}

            Console.WriteLine("Done with insert");

            IFactoryPersistence factory = Factory.CreateInstance<IFactoryPersistence>();
            var repo = factory.CreateQuizRepo();

            Console.ReadKey();
        }
    }
}
