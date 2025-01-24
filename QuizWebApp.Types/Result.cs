using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApp.Types
{
    public class Result
    {
        public int ResultID { get; set; }
        public string UserName { get; set; }
        public DateTime QuizDate { get; set; }
        public Decimal Score { get; set; }

    }
}
