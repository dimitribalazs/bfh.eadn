using BFH.EADN.Persistence.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Seed
{
    public class QuizDBInitializer : DropCreateDatabaseIfModelChanges<QuizDataContext>
    {
        protected override void Seed(QuizDataContext context)
        {
            Topic programming = new Topic { Name = "Programming", Description = "All about programming" };
            Topic movies = new Topic { Name = "Movies", Description = "All about movies" };
            Topic actors = new Topic { Name = "Actors", Description = "All about actory" };

            Question programmingQuestion1 = new Question { Text = "Is the C# syntax similiar to C?", IsMultipleChoice = false };
            Question programmingQuestion2 = new Question { Text = "Is SQL an object oriented language" };
            Question programmingQuestion3 = new Question { Text = "Which are a linux system?", IsMultipleChoice = true };
            Question movieQuestion1 = new Question { Text = "How many sequels does matrix have?" };
            Question movieActorQuestion1 = new Question { Text = "Does Keanu Reeves star in all Matrix movies?" };

            Answer programmingAnwers1Yes = new Answer { Text = "Yes", IsSolution = true };
            Answer programmingAnwers1No = new Answer { Text = "No" };

            Answer programmingAnwers2Yes = new Answer { Text = "Yes", IsSolution = true };
            Answer programmingAnwers2No = new Answer { Text = "No" };

            Answer programmingAnwers3Ubuntu = new Answer { Text = "Ubuntu", IsSolution = true };
            Answer programmingAnwers3Windows = new Answer { Text = "Windows", IsSolution = false };
            Answer programmingAnwers3Arch = new Answer { Text = "Arch", IsSolution = true };
            Answer programmingAnwers3Debian = new Answer { Text = "Debian", IsSolution = true };


            Answer movieQuestion1first = new Answer { Text = "3" };
            Answer movieQuestion1second = new Answer { Text = "10" };
            Answer movieQuestion1third = new Answer { Text = "2", IsSolution = true };

            programming.Questions = new HashSet<Question>
            {
                programmingQuestion1,
                //programmingQuestion2,
                programmingQuestion3
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

            programmingQuestion1.Anwers = new HashSet<Answer>
              {
                  programmingAnwers1No,
                  programmingAnwers1Yes
              };

            programmingQuestion2.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion3.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion3.Anwers = new HashSet<Answer>
            {
                programmingAnwers3Arch,
                programmingAnwers3Debian,
                programmingAnwers3Windows,
                programmingAnwers3Ubuntu
            };

            movieQuestion1.Topics = new HashSet<Topic>
            {
                movies
            };

            movieQuestion1.Anwers = new HashSet<Answer>
            {
                movieQuestion1first,
                movieQuestion1second,
                movieQuestion1third
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
                    //programmingQuestion2,
                    programmingQuestion3
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
