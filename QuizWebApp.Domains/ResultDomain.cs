using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApp.Domains
{
    public class ResultDomain
    {
        private readonly string connectionString;

        public ResultDomain(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void InsertResult(QuizWebApp.Types.Result result)
        {
            QuizWebApp.Repositories.ResultRepository resultRepository = new Repositories.ResultRepository(connectionString);
            resultRepository.InsertResult(result);
        }

        public List<QuizWebApp.Types.Result> GetResultsByUserName(string userName)
        {
            QuizWebApp.Repositories.ResultRepository resultRepository = new Repositories.ResultRepository(connectionString);
            return resultRepository.GetResultsByUserName(userName);
        }
    }
}
