using QuizWebApp.Repositories;

namespace QuizWebApp.Domains
{
    public class AnswerDomain
    {
        private readonly string connectionString;

        public AnswerDomain(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<QuizWebApp.Types.Answer> GetAnswersByQuestionID(int quizID)
        {
            QuizWebApp.Repositories.AnswerRepository answerRepository = new Repositories.AnswerRepository(connectionString);
            return answerRepository.GetAnswersByQuestionID(quizID);
        }

        public QuizWebApp.Types.Answer GetAnswerByAnswerID (int id)
        {
            QuizWebApp.Repositories.AnswerRepository answerRepository = new Repositories.AnswerRepository(connectionString);
            return answerRepository.GetAnswerByAnswerID(id);
        }
    }
}
