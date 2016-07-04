using System;
namespace GeoQuiz
{
    public class Question
    {
        public int TextResId { get; set; }
        public bool AnswerTrue { get; set; }

        public Question(int textResId, bool answerTrue)
        {
            TextResId = textResId;
            AnswerTrue = answerTrue;
        }
    }
}

