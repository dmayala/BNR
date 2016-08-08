using System;
using System.Collections.Generic;
using UIKit;

namespace Quiz
{
    public partial class ViewController : UIViewController
    {
        private List<string> _questions = new List<string>
        {
            "From what is cognac made?",
            "What is 7+7?",
            "What is the capital of Vermont?"
        };

        private List<string> _answers = new List<string>
        {
            "Grapes",
            "14",
            "Montpelier"
        };

        private int _currentQuestionIndex = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            QuestionLabel.Text = _questions[_currentQuestionIndex];

            NextQuestionButton.TouchUpInside += ShowNextQuestion;
            ShowAnswerButton.TouchUpInside += ShowAnswer;
        }

        private void ShowNextQuestion(object sender, EventArgs e)
        {
            _currentQuestionIndex++;
            if (_currentQuestionIndex == _questions.Count)
            {
                _currentQuestionIndex = 0;
            }

            var question = _questions[_currentQuestionIndex];
            QuestionLabel.Text = question;
            AnswerLabel.Text = "???";
        }

        private void ShowAnswer(object sender, EventArgs e)
        {
            var answer = _answers[_currentQuestionIndex];
            AnswerLabel.Text = answer;
        }
    }
}