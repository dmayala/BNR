using System;
namespace GeoQuiz
{
    public class Question
    {
        public int TextResId { get; set; }
        public bool AnswerTrue { get; set; }
        public bool AnswerShown { get; set; }

        public Question(int textResId, bool answerTrue, bool answerShown = false)
        {
            TextResId = textResId;
            AnswerTrue = answerTrue;
            AnswerShown = answerShown;
        }
    }
}

