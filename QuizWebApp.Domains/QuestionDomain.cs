namespace QuizWebApp.Domains
{
    public class QuestionDomain
    {
        private readonly string connectionString;

        public QuestionDomain(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<QuizWebApp.Types.Question> GetQuestionsByQuizID(int quizID)
        {
            QuizWebApp.Repositories.QuestionRepository questionRepository = new Repositories.QuestionRepository(connectionString);
            return questionRepository.GetQuestionsByQuizID(quizID);
        }
    }
}
