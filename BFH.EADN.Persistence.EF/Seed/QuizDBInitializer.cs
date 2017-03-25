using System.Collections.Generic;
using System.Data.Entity;

using BFH.EADN.Common.Types.Enums;
using BFH.EADN.Persistence.EF.Entities;
using System;

namespace BFH.EADN.Persistence.EF.Seed
{
    /// <summary>
    /// Seeds the example data. Used for unit tests and also for "human" tests
    /// </summary>
    internal class QuizDBInitializer : DropCreateDatabaseIfModelChanges<QuizDataContext>
    {
        protected override void Seed(QuizDataContext context)
        {

            Topic math = new Topic { Name = "Math", Description = "Math questions" };
            Topic programming = new Topic { Name = "Programming", Description = "All about programming" };
            Topic movies = new Topic { Name = "Movies", Description = "All about movies" };
            Topic actors = new Topic { Name = "Actors", Description = "All about actory" };

            Question programmingQuestion1 = new Question { Text = "Is the C# syntax similiar to C?", IsMultipleChoice = false };
            Question programmingQuestion2 = new Question { Text = "Is SQL an object oriented language?" };
            Question programmingQuestion3 = new Question { Text = "1 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion4 = new Question { Text = "2 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion5 = new Question { Text = "3 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion6 = new Question { Text = "4 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion7 = new Question { Text = "5 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion8 = new Question { Text = "6 Which are linux systems? Can Be Deleted", IsMultipleChoice = true, LastUsed = new DateTime(2015, 1, 12) };
            Question programmingQuestion9 = new Question { Text = "7 Which are linux systems? Can Be Deleted", IsMultipleChoice = true, LastUsed = new DateTime(2015, 1, 12) };
            Question programmingQuestion10 = new Question { Text = "8 Which are linux systems? Can Be Deleted", IsMultipleChoice = true, LastUsed = new DateTime(2015, 1, 12) };
            Question programmingQuestion11 = new Question { Text = "9 Which are linux systems?", IsMultipleChoice = true };
            Question programmingQuestion12 = new Question { Text = "10 Which are linux systems?", IsMultipleChoice = true };

            Question movieQuestion1 = new Question { Text = "How many sequels does matrix have?" };
            Question movieQuestion2 = new Question { Text = "Are these Lord of the Rings characters?", IsMultipleChoice = true };
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

            Answer movieActorQuestion1Yes = new Answer { Text = "Yes", IsSolution = true };
            Answer movieActorQuestion1No = new Answer { Text = "No" };

            Answer movieQuestion2first = new Answer { Text = "Sauron", IsSolution = true };
            Answer movieQuestion2second = new Answer { Text = "Spiderman" };
            Answer movieQuestion2third = new Answer { Text = "Saruman", IsSolution = true };

            //math
            Question mathQuestion1 = new Question { Text = "Is 1 + 1 = 2?", Hint = "Yes it is" };
            Answer mathAnswer1Yes = new Answer { Text = "Yes", IsSolution = true };
            Answer mathAnswer1No = new Answer { Text = "No" };

            Question mathQuestion2 = new Question { Text = "What is 2 * 4?", Hint = "It is 8", IsMultipleChoice = true };
            Answer mathAnswer27 = new Answer { Text = "7" };
            Answer mathAnswer28 = new Answer { Text = "8" };

            Question mathQuestion3 = new Question { Text = "Is 1 + 1 = 3?", Hint = "No it isnt" };
            Answer mathAnswer3Yes = new Answer { Text = "Yes" };
            Answer mathAnswer3No = new Answer { Text = "No", IsSolution = true };

            Question mathQuestion4 = new Question { Text = "What is 2 ^ 3 ?", Hint = "8", IsMultipleChoice = true };
            Answer mathAnswer44 = new Answer { Text = "4" };
            Answer mathAnswer410 = new Answer { Text = "10" };
            Answer mathAnswer411 = new Answer { Text = "11" };

            Question mathQuestion5 = new Question { Text = "15 / 5 = 3?", Hint = "Yes it is" };
            Answer mathAnswer5Yes = new Answer { Text = "Yes", IsSolution = true };
            Answer mathAnswer5No = new Answer { Text = "No" };

            Question mathQuestion6 = new Question { Text = "20 * 20?", Hint = "400", IsMultipleChoice = true };
            Answer mathAnswer6399 = new Answer { Text = "399" };
            Answer mathAnswer6400 = new Answer { Text = "400" };
            Answer mathAnswer6401 = new Answer { Text = "401" };

            Question mathQuestion7 = new Question { Text = "Can you divide without rest? 20 / x ?", IsMultipleChoice = true };
            Answer mathAnswer710 = new Answer { Text = "10", IsSolution = true };
            Answer mathAnswer73 = new Answer { Text = "3" };
            Answer mathAnswer74 = new Answer { Text = "4", IsSolution = true };
            Answer mathAnswer79 = new Answer { Text = "9" };

            math.Questions = new HashSet<Question>
            {
                mathQuestion1,
                mathQuestion2,
                mathQuestion3,
                mathQuestion4,
                mathQuestion5,
                mathQuestion6,
                mathQuestion7
            };

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
                movieActorQuestion1,
                movieQuestion2
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

            programmingQuestion2.Answers = new HashSet<Answer>
              {
                  programmingAnwers2No,
                  programmingAnwers2Yes
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

            movieQuestion2.Topics = new HashSet<Topic>
            {
                movies
            };

            movieQuestion2.Answers = new HashSet<Answer>
            {
                movieQuestion2first,
                movieQuestion2second,
                movieQuestion2third
            };

            movieActorQuestion1.Topics = new HashSet<Topic>
            {
                movies,
                actors
            };

            movieActorQuestion1.Answers = new HashSet<Answer>
            {
                movieActorQuestion1Yes,
                movieActorQuestion1No
            };

            mathQuestion1.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion1.Answers = new HashSet<Answer>
            {
                mathAnswer1No,
                mathAnswer1Yes,
            };

            mathQuestion2.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion2.Answers = new HashSet<Answer>
            {
                mathAnswer27,
                mathAnswer28,
            };

            mathQuestion3.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion3.Answers = new HashSet<Answer>
            {
                mathAnswer3Yes,
                mathAnswer3No
            };

            mathQuestion4.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion4.Answers = new HashSet<Answer>
            {
                mathAnswer411,
                mathAnswer410,
                mathAnswer44
            };

            mathQuestion5.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion5.Answers = new HashSet<Answer>
            {
                mathAnswer5No,
                mathAnswer5Yes
            };

            mathQuestion6.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion6.Answers = new HashSet<Answer>
            {
                mathAnswer6399,
                mathAnswer6400,
                mathAnswer6401
            };

            mathQuestion7.Topics = new HashSet<Topic>
            {
                math
            };

            mathQuestion7.Answers = new HashSet<Answer>
            {
                mathAnswer710,
                mathAnswer73,
                mathAnswer74,
                mathAnswer79
            };

            HashSet<Topic> topics = new HashSet<Topic>
            {
                programming, movies, math
            };

            Quiz quiz1 = new Quiz
            {
                Text = "Programming quiz",
                Type = QuizType.Dynamic,
                MinQuestionCount = 3,
                MaxQuestionCount = 5,
                LastUsed = new DateTime(2016, 1, 22),
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
                Type = QuizType.Fix,
                MinQuestionCount = 1,
                MaxQuestionCount = 5,
                LastUsed = new DateTime(2017, 2, 22),
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
                MinQuestionCount = 1,
                MaxQuestionCount = 5,
                Questions = new HashSet<Question>
                {
                    movieQuestion1,
                    movieActorQuestion1
                }
            };

            Quiz quiz4 = new Quiz
            {
                Text = "Quiz about movies dynamic",
                Type = QuizType.Fix,
                MinQuestionCount = 2,
                MaxQuestionCount = 5,
                Questions = new HashSet<Question>
                {
                    movieQuestion1,
                    movieQuestion2,
                    movieActorQuestion1
                }
            };

            Quiz quiz5 = new Quiz
            {
                Text = "Math quiz dynamic",
                Type = QuizType.Dynamic,
                MinQuestionCount = 2,
                MaxQuestionCount = 4,
                Questions = new HashSet<Question>
                {
                    mathQuestion1,
                    mathQuestion2,
                    mathQuestion3,
                    mathQuestion4,
                    mathQuestion5,
                    mathQuestion6,
                    mathQuestion7,
                }
            };

            Quiz quiz6 = new Quiz
            {
                Text = "Math quiz fix",
                Type = QuizType.Fix,
                MinQuestionCount = 2,
                MaxQuestionCount = 3,
                Questions = new HashSet<Question>
                {
                    mathQuestion1,
                    mathQuestion2,
                    mathQuestion3,
                    mathQuestion4,
                    mathQuestion5,
                    mathQuestion6,
                    mathQuestion7,
                }
            };

            List<Quiz> quizzes = new List<Quiz>
            {
                quiz1,
                quiz2,
                quiz3,
                quiz4,
                quiz5,
                quiz6
            };

            foreach (Quiz quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }

            context.SaveChanges();


            //question without topic
            Question withoutQuestion = new Question
            {
                IsMultipleChoice = true,
                Text = "Without question"
            };

            context.Questions.Add(withoutQuestion);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
