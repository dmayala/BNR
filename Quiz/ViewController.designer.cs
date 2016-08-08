// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Quiz
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AnswerLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NextQuestionButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel QuestionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ShowAnswerButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnswerLabel != null) {
                AnswerLabel.Dispose ();
                AnswerLabel = null;
            }

            if (NextQuestionButton != null) {
                NextQuestionButton.Dispose ();
                NextQuestionButton = null;
            }

            if (QuestionLabel != null) {
                QuestionLabel.Dispose ();
                QuestionLabel = null;
            }

            if (ShowAnswerButton != null) {
                ShowAnswerButton.Dispose ();
                ShowAnswerButton = null;
            }
        }
    }
}