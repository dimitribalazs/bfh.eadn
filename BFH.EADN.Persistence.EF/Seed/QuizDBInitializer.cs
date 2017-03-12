using BFH.EADN.Common.Types.Enums;
using BFH.EADN.Persistence.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Seed
{
    public class QuizDBInitializer : DropCreateDatabaseAlways<QuizDataContext>
    {
        protected override void Seed(QuizDataContext context)
        {
            Topic programming = new Topic { Name = "Programming", Description = "All about programming" };
            Topic movies = new Topic { Name = "Movies", Description = "All about movies" };
            Topic actors = new Topic { Name = "Actors", Description = "All about actory" };

            Question programmingQuestion1 = new Question { Text = "Is the C# syntax similiar to C?", IsMultipleChoice = false };
            Question programmingQuestion2 = new Question { Text = "Is SQL an object oriented language" };
            Question programmingQuestion3 = new Question { Text = "1 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion4 = new Question { Text = "2 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion5 = new Question { Text = "3 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion6 = new Question { Text = "4 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion7 = new Question { Text = "5 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion8 = new Question { Text = "6 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion9 = new Question { Text = "7 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion10 = new Question { Text = "8 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion11 = new Question { Text = "9 Which are a linux system?", IsMultipleChoice = true };
            Question programmingQuestion12 = new Question { Text = "10 Which are a linux system?", IsMultipleChoice = true };
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

            Answer programmingAnwers4Ubuntu = new Answer { Text = "Ubuntu 1", IsSolution = true };
            Answer programmingAnwers5Ubuntu = new Answer { Text = "Ubuntu 2", IsSolution = true };
            Answer programmingAnwers6Ubuntu = new Answer { Text = "Ubuntu 3", IsSolution = true };
            Answer programmingAnwers7Ubuntu = new Answer { Text = "Ubuntu 4", IsSolution = true };
            Answer programmingAnwers8Ubuntu = new Answer { Text = "Ubuntu 5", IsSolution = true };
            Answer programmingAnwers9Ubuntu = new Answer { Text = "Ubuntu 6", IsSolution = true };
            Answer programmingAnwers10Ubuntu = new Answer { Text = "Ubuntu 7", IsSolution = true };
            Answer programmingAnwers11Ubuntu = new Answer { Text = "Ubuntu 8", IsSolution = true };
            Answer programmingAnwers12Ubuntu = new Answer { Text = "Ubuntu 9", IsSolution = true };


            Answer movieQuestion1first = new Answer { Text = "3" };
            Answer movieQuestion1second = new Answer { Text = "10" };
            Answer movieQuestion1third = new Answer { Text = "2", IsSolution = true };

            programming.Questions = new HashSet<Question>
            {
                programmingQuestion1,
                programmingQuestion2,
                programmingQuestion3,
                programmingQuestion4,
                programmingQuestion5,
                programmingQuestion6,
                programmingQuestion7,
                programmingQuestion8,
                programmingQuestion9,
                programmingQuestion10,
                programmingQuestion11,
                programmingQuestion12,
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

            programmingQuestion1.Answers = new HashSet<Answer>
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

            programmingQuestion3.Answers = new HashSet<Answer>
            {
                programmingAnwers3Arch,
                programmingAnwers3Debian,
                programmingAnwers3Windows,
                programmingAnwers3Ubuntu
            };

            programmingQuestion4.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion4.Answers = new HashSet<Answer>
            {
                programmingAnwers4Ubuntu
            };

            programmingQuestion5.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion5.Answers = new HashSet<Answer>
            {
                programmingAnwers5Ubuntu
            };

            programmingQuestion6.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion6.Answers = new HashSet<Answer>
            {
                programmingAnwers6Ubuntu
            };

            programmingQuestion7.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion7.Answers = new HashSet<Answer>
            {
                programmingAnwers7Ubuntu
            };

            programmingQuestion8.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion8.Answers = new HashSet<Answer>
            {
                programmingAnwers8Ubuntu
            };

            programmingQuestion9.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion9.Answers = new HashSet<Answer>
            {
                programmingAnwers9Ubuntu
            };

            programmingQuestion10.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion10.Answers = new HashSet<Answer>
            {
                programmingAnwers10Ubuntu
            };

            programmingQuestion11.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion11.Answers = new HashSet<Answer>
            {
                programmingAnwers11Ubuntu
            };

            programmingQuestion12.Topics = new HashSet<Topic>
            {
                programming
            };

            programmingQuestion12.Answers = new HashSet<Answer>
            {
                programmingAnwers12Ubuntu
            };

            
            movieQuestion1.Topics = new HashSet<Topic>
            {
                movies
            };

            movieQuestion1.Answers = new HashSet<Answer>
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
                Type = QuizType.Dynamic,
                Questions = new HashSet<Question>
                {
                    programmingQuestion1,
                    programmingQuestion2,
                    programmingQuestion3,
                    programmingQuestion4,
                    programmingQuestion5,
                    programmingQuestion6,
                    programmingQuestion7,
                    programmingQuestion8,
                    programmingQuestion9,
                    programmingQuestion10,
                    programmingQuestion11,
                    programmingQuestion12,
                }
            };

            Quiz quiz2 = new Quiz
            {
                Text = "Quiz about movies and actors variable",
                Type = QuizType.Variable,
                Questions = new HashSet<Question>
                {
                    movieQuestion1,
                    movieActorQuestion1
                }
            };

            Quiz quiz3 = new Quiz
            {
                Text = "Quiz about movies and actors dynamic",
                Type = QuizType.Dynamic,
                Questions = new HashSet<Question>
                {
                    movieQuestion1,
                    movieActorQuestion1
                }
            };

            List<Quiz> quizzes = new List<Quiz>
            {
                quiz1,
                quiz2,
                quiz3
            };

            foreach (Quiz quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
