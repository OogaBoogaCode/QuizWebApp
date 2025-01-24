using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApp.Types
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string Text { get; set; }
    }
}
