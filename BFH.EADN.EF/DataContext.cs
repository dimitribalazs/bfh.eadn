using BFH.EADN.EF.Entities;
using BFH.EADN.EF.Seed;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.EF
{
    public class QuizDataContext : DbContext
    {

        public QuizDataContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new QuizDBInitializer());
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
