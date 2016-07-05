
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace GeoQuiz
{
    [Activity]
    public class CheatActivity : AppCompatActivity
    {
        private const string ExtraAnswerIsTrue = "la.daya.geoquiz.ExtraAnswerIsTrue";
        private const string ExtraAnswerShown = "la.daya.geoquiz.ExtraAnswerShown";

        private bool _answerIsTrue;

        private TextView _answerTextView;
        private Button _showAnswer;

        public static Intent NewIntent(Context packageContext, bool answerIsTrue)
        {
            Intent i = new Intent(packageContext, typeof(CheatActivity));
            i.PutExtra(ExtraAnswerIsTrue, answerIsTrue);
            return i;
        }

        public static bool WasAnswerShown(Intent result)
        {
            return result.GetBooleanExtra(ExtraAnswerShown, false);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Cheat);

            _answerIsTrue = Intent.GetBooleanExtra(ExtraAnswerIsTrue, false);

            _answerTextView = FindViewById<TextView>(Resource.Id.AnswerTextView);

            _showAnswer = FindViewById<Button>(Resource.Id.ShowAnswerButton);
            _showAnswer.Click += (sender, e) =>
            {
                _answerTextView.SetText(_answerIsTrue ? Resource.String.true_button : Resource.String.false_button);
                SetAnswerShownResult(true);
            };
        }

        private void SetAnswerShownResult(bool isAnswerShown)
        {
            Intent data = new Intent();
            data.PutExtra(ExtraAnswerShown, isAnswerShown);
            SetResult(Result.Ok, data);
        }
   }
}

