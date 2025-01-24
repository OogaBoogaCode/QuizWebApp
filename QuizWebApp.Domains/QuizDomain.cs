namespace QuizWebApp.Domains
{
    public class QuizDomain
    {
        private readonly string connectionString;

        public QuizDomain(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<QuizWebApp.Types.Quiz> GetQuizzes()
        {
            QuizWebApp.Repositories.QuizRepository quizRepository = new Repositories.QuizRepository(connectionString);
            return quizRepository.GetQuizzes();
        }
    }
}
