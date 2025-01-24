using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace QuizWebApp.Pages.Quiz
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public List<QuizWebApp.Types.Quiz> quizzes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {   
            QuizWebApp.Domains.QuizDomain quizDomain = new Domains.QuizDomain(_configuration.GetConnectionString("DefaultConnection"));
            quizzes = quizDomain.GetQuizzes();

            return Page();
        }
    }
}
