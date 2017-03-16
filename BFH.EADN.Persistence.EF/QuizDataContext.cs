﻿using System.Data.Entity;

using BFH.EADN.Persistence.EF.Entities;
using BFH.EADN.Persistence.EF.Seed;

namespace BFH.EADN.Persistence.EF
{
    /// <summary>
    /// DataContext which the EF uses for saving
    /// </summary>
    internal class QuizDataContext : DbContext
    {
        public QuizDataContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new QuizDBInitializer());
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionAnswerState> QuestionAnswerStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //todo comment this

            //modelBuilder.Entity<Quiz>()
            //    .HasOptional<ICollection<Question>>(q => q.Questions)
            //    .WithOptionalDependent(q => q.)
                //.WithRequired(q => q.FOo)
                //.HasForeignKey(e => e.FooId)
                //
                //.WillCascadeOnDelete(false);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOptional()
                .WillCascadeOnDelete();
        }

    }
}
