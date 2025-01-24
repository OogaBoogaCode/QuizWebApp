using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using QuizWebApp.Domains;
using QuizWebApp.Types;

namespace QuizWebApp.Pages.Quiz
{
    [Authorize]
    public class TakeQuizModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public TakeQuizModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public List<QuizWebApp.Types.Question> questions { get; set; }
        [BindProperty]
        public List<QuizWebApp.Types.Answer> answers { get; set; }
        public QuizWebApp.Types.Question currentQuestion { get; set; }
        public List<QuizWebApp.Types.Answer> currentAnswers { get; set; }

        [BindProperty]
        public int quizID { get; set; }

        public int score { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            quizID = id;
            QuizWebApp.Domains.QuestionDomain questionDomain = new Domains.QuestionDomain(_configuration.GetConnectionString("DefaultConnection"));
            QuizWebApp.Domains.AnswerDomain answerDomain = new Domains.AnswerDomain(_configuration.GetConnectionString("DefaultConnection"));
            questions = questionDomain.GetQuestionsByQuizID(id);
            
            answers = new List<QuizWebApp.Types.Answer>();

            foreach (var question in questions)
            {
                answers.AddRange(answerDomain.GetAnswersByQuestionID(question.QuestionID)); 

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            QuizWebApp.Domains.QuestionDomain questionDomain = new Domains.QuestionDomain(_configuration.GetConnectionString("DefaultConnection"));
            QuizWebApp.Domains.AnswerDomain answerDomain = new Domains.AnswerDomain(_configuration.GetConnectionString("DefaultConnection"));
            QuizWebApp.Domains.ResultDomain resultDomain = new Domains.ResultDomain(_configuration.GetConnectionString("DefaultConnection"));

            FormCollection form = (FormCollection)Request.Form;
            List<string> answerIDs = new List<string>();

            foreach (var key in form.Keys)
            {
                var value = form[key];
                answerIDs.Add(key);
            }

            // Remove quizId and Submit Keys from list.
            answerIDs.RemoveAt(0);
            answerIDs.RemoveAt(answerIDs.Count - 1);

            List<QuizWebApp.Types.Answer> playerAnswers = new List<QuizWebApp.Types.Answer>();

            foreach (var id in answerIDs)
            {
                playerAnswers.Add(answerDomain.GetAnswerByAnswerID(Int32.Parse(id)));
            }

            
            questions = questionDomain.GetQuestionsByQuizID(quizID);

            foreach (var answer in playerAnswers)
            {
                if(answer.IsCorrect)
                {
                    score++;
                }
                
            }

            var result = new QuizWebApp.Types.Result()
            {
                UserName = User.Identity.Name,
                QuizDate = DateTime.Now,
                Score = (decimal)score/(decimal)questions.Count(),
            };

            resultDomain.InsertResult(result);
           

            return new RedirectToPageResult("/Quiz/Results");
        }
        //public void NextQuestion(int currentQuestionIndex)
        //{
        //    QuizWebApp.Domains.AnswerDomain answerDomain = new Domains.AnswerDomain(_configuration.GetConnectionString("DefaultConnection"));
        //    currentQuestionIndex++;
        //    currentQuestion = questions[currentQuestionIndex - 1];
        //    currentAnswers = answerDomain.GetQuestionsByQuestionID(currentQuestion.QuestionID);

        //}
    }
}
