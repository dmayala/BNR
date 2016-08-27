using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Content;

namespace GeoQuiz
{
	[Activity(Label = "GeoQuiz", MainLauncher = true)]
	public class QuizActivity : AppCompatActivity
	{
        private const string KeyIndex = "index";
        private const string KeyIsCheater = "isCheater";
        private const int RequestCodeCheat = 0;

        private Button _trueButton;
        private Button _falseButton;
        private Button _nextButton;
        private Button _prevButton;
        private Button _cheatButton;
        private TextView _questionTextView;

        private readonly List<Question> _questionBank = new List<Question>()
        {
            new Question(Resource.String.question_oceans, true),
            new Question(Resource.String.question_mideast, false),
            new Question(Resource.String.question_africa, false),
            new Question(Resource.String.question_americas, true),
            new Question(Resource.String.question_asia, true),
        };

        private int _currentIndex = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Quiz);

            if (savedInstanceState != null)
            {
                _currentIndex = savedInstanceState.GetInt(KeyIndex, 0);
                _questionBank[_currentIndex].AnswerShown = savedInstanceState.GetBoolean(KeyIsCheater, false);
            }

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

            _cheatButton = FindViewById<Button>(Resource.Id.CheatButton);
            _cheatButton.Click += (sender, e) =>
            {
                bool answerIsTrue = _questionBank[_currentIndex].AnswerTrue;
                Intent i = CheatActivity.NewIntent(this, answerIsTrue);
                StartActivityForResult(i, RequestCodeCheat);
            };

            UpdateQuestion();
        }

        private void UpdateQuestion()
        {
            int question = _questionBank[_currentIndex].TextResId;
            _questionTextView.SetText(question);
        }

        private void CheckAnswer(bool userPressedTrue)
        {
            bool answerIsTrue = _questionBank[_currentIndex].AnswerTrue;

            int messageResId = 0;

            if (_questionBank[_currentIndex].AnswerShown)
            {
                messageResId = Resource.String.judgment_toast;
            }
            else 
            {
                if (userPressedTrue == answerIsTrue)
                {
                    messageResId = Resource.String.correct_toast;
                }
                else
                {
                    messageResId = Resource.String.incorrect_toast;
                }
            }

            Toast.MakeText(this, messageResId, ToastLength.Short).Show();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode != Result.Ok) return;

            if (requestCode == RequestCodeCheat)
            {
                if (data == null) return;
                bool isCheater = CheatActivity.WasAnswerShown(data);
                _questionBank[_currentIndex].AnswerShown = isCheater;

            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt(KeyIndex, _currentIndex);
            outState.PutBoolean(KeyIsCheater, _questionBank[_currentIndex].AnswerShown);
        }
	}
}


