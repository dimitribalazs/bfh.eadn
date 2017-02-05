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
            using (var repo = factory.CreateQuizRepository())
            {
                repo.Create(new BFH.EADN.Common.Types.Contracts.Quiz
                {
                    Text = "test1",
                    Type = BFH.EADN.Common.Types.Enums.QuizType.Dynamic
                });
                var result = repo.Get(Guid.NewGuid());
                Console.WriteLine(result.Text);
                Console.ReadKey();
            }
        }
    }
}
