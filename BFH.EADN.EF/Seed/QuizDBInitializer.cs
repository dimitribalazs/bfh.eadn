using BFH.EADN.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.EF.Seed
{
    public class QuizDBInitializer : DropCreateDatabaseAlways<QuizDataContext>
    {
        protected override void Seed(QuizDataContext context)
        {
            Topic programming = new Topic { Name = "Programming", Description = "All about programming" };
            Topic movies = new Topic { Name = "Movies", Description = "All about movies" };
            Topic actors = new Topic { Name = "Actors", Description = "All about actory" };
           
            Question programmingQuestion1 = new Question { Text = "Is the C# syntax similiar to C?" };
            Question programmingQuestion2  = new Question { Text = "Is SQL an object oriented language" };
            Question movieQuestion1 = new Question { Text =  "How many sequels does matrix have?" };
            Question movieActorQuestion1 = new Question { Text = "Does Keanu Reeves star in all Matrix movies?" };

            programming.Questions = new HashSet<Question>
            {
                programmingQuestion1,
                programmingQuestion2
            };

            movies.Questions = new HashSet<Question>
            {
                movieQuestion1,
                movieActorQuestion1
            };

            actors.Questions = new HashSet<Question>
            {
                movieActorQuestion1
            };

            programmingQuestion1.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion2.Topics = new HashSet<Topic>
            {
                programming
            };

            movieQuestion1.Topics = new HashSet<Topic>
            {
                movies
            };

            movieActorQuestion1.Topics = new HashSet<Topic>
            {
                movies,
                actors
            };

            HashSet<Topic> topics = new HashSet<Topic>
            {
                programming, movies                
            };

            Quiz quiz1 = new Quiz
            {
                Text = "Programming quiz",
                Questions = new HashSet<Question>
                {
                    programmingQuestion1,
                    programmingQuestion2,
                }
            };

            Quiz quiz2 = new Quiz
            {
                Text = "Quiz about movies and actory",
                Questions = new HashSet<Question>
                {
                    movieQuestion1,
                    movieActorQuestion1
                }
            };


            List<Quiz> quizzes = new List<Quiz>
            {
                quiz1,
                quiz2
            };

            foreach (Quiz quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }

            //context.Topics.Add(programming);
            //context.Topics.Add(movies);
            //context.Topics.Add(actors);

            //context.Questions.Add(programmingQuestion1);
            //context.Questions.Add(programmingQuestion2);
            //context.Questions.Add(movieQuestion1);
            //context.Questions.Add(movieActorQuestion1);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
