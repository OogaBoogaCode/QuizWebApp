using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizWebApp.Pages.Quiz
{
    [Authorize]
    public class ResultsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ResultsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public List<QuizWebApp.Types.Result> results { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            QuizWebApp.Domains.ResultDomain resultDomain = new Domains.ResultDomain(_configuration.GetConnectionString("DefaultConnection"));
            results = resultDomain.GetResultsByUserName(User.Identity.Name);

            return Page();
        }
    }
}
