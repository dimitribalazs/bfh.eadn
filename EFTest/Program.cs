using BFH.EADN.Persistence.EF;
using BFH.EADN.Persistence.EF.Entities;
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
        private readonly QuizDataContext _context;
        static void Main(string[] args)
        {
            var quiz = new Quiz
            {
                Text = "Yolo"
            };
            using (var db = new QuizDataContext())
            {
                db.Quizzes.Add(quiz);
                db.SaveChanges();
            }

            Console.WriteLine("Done with insert");
            Console.ReadKey();
        }
    }
}
