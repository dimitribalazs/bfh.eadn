﻿using BFH.EADN.Persistence.EF.Entities;
using BFH.EADN.Persistence.EF.Seed;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF
{
    public class QuizDataContext : DbContext
    {
        public QuizDataContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new QuizDBInitializer());
            //Database.Log = Console.WriteLine;
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
