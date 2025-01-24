using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApp.Types
{
    public class Answer
    {
        public int AnswerID { get; set; }

        public int QuestionID {  get; set; }
       
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

    }
}
