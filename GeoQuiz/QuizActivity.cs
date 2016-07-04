using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V7.App;

namespace GeoQuiz
{
	[Activity(Label = "GeoQuiz", MainLauncher = true)]
	public class QuizActivity : AppCompatActivity
	{
        private Button _trueButton;
        private Button _falseButton;
        private Button _nextButton;
        private Button _prevButton;
        private TextView _questionTextView;

        private List<Question> _questionBank = new List<Question>() 
        {
            new Question(Resource.String.question_oceans, true),
            new Question(Resource.String.question_mideast, false),
            new Question(Resource.String.question_africa, false),
            new Question(Resource.String.question_americas, true),
            new Question(Resource.String.question_asia, true),
        };

        private int _currentIndex = 0;

        private void UpdateQuestion()
        {
            int question = _questionBank[_currentIndex].TextResId;
            _questionTextView.SetText(question);
        }

        private void CheckAnswer(bool userPressedTrue)
        {
            bool answerIsTrue = _questionBank[_currentIndex].AnswerTrue;

            int messageResId = 0;

            if (userPressedTrue == answerIsTrue)
            {
                messageResId = Resource.String.correct_toast;
            }
            else 
            {
                messageResId = Resource.String.incorrect_toast;
            }

            Toast.MakeText(this, messageResId, ToastLength.Short).Show();
        }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Quiz);

            _questionTextView = FindViewById<TextView>(Resource.Id.QuestionTextView);
            _questionTextView.Click += (sender, e) =>
            {
                _currentIndex = (_currentIndex + (_questionBank.Count - 1)) % _questionBank.Count;
                UpdateQuestion();
            };
    
            _trueButton = FindViewById<Button>(Resource.Id.TrueButton);
            _trueButton.Click += (sender, e) =>
            {
                CheckAnswer(true);
            };

            _falseButton = FindViewById<Button>(Resource.Id.FalseButton);
            _falseButton.Click += (sender, e) =>
            {
                CheckAnswer(false);
            };

            _nextButton = FindViewById<Button>(Resource.Id.NextButton);
            _nextButton.Click += (sender, e) =>
            {
                _currentIndex = (_currentIndex + 1) % _questionBank.Count;
                UpdateQuestion();
            };

            _prevButton = FindViewById<Button>(Resource.Id.PrevButton);
            _prevButton.Click += (sender, e) =>
            {
                _currentIndex = (_currentIndex - 1 + _questionBank.Count) % _questionBank.Count;
                UpdateQuestion();
            };

            UpdateQuestion();
		}
	}
}


