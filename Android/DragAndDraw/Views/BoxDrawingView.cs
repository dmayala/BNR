using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using DragAndDraw.Models;
using System.Collections.Generic;
using System;

namespace DragAndDraw.Views
{
    [Register("la.daya.draganddraw.BoxDrawingView")]
    public class BoxDrawingView : View
    {
        const string TAG = "BoxDrawingView";

        private Box _currentBox;
        private List<Box> _boxen = new List<Box>();
        private Paint _boxPaint;
        private Paint _backgroundPaint;

        public BoxDrawingView(Context context) : base(context, null)
        {
        }

        public BoxDrawingView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            // Paint the boxes a nice semitransparent red (ARGB)
            _boxPaint = new Paint() { Color = new Color(0x22ff0000) };

            // Paint the background off-white
            _backgroundPaint = new Paint() { Color = new Color(0xf9f0e2) };
        }

        protected override void OnDraw(Canvas canvas)
        {
            // Fill the background
            canvas.DrawPaint(_backgroundPaint);

            _boxen.ForEach(b =>
            {
                var left = Math.Min(b.Origin.X, b.Current.X);
                var right = Math.Max(b.Origin.X, b.Current.X);
                var top = Math.Min(b.Origin.Y, b.Current.Y);
                var bottom = Math.Max(b.Origin.Y, b.Current.Y);

                canvas.DrawRect(left, top, right, bottom, _boxPaint);
            });
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var current = new PointF(e.GetX(), e.GetY());
            string action = string.Empty;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    action = "ACTION_DOWN";
                    _currentBox = new Box(current);
                    _boxen.Add(_currentBox);
                    break;
                case MotionEventActions.Move:
                    action = "ACTION_MOVE";
                    if (_currentBox != null)
                    {
                        _currentBox.Current = current;
                        Invalidate();
                    }
                    break;
                case MotionEventActions.Up:
                    action = "ACTION_UP";
                    _currentBox = null;
                    break;
                case MotionEventActions.Cancel:
                    action = "ACTION_CANCEL";
                    _currentBox = null;
                    break;
            }

            Log.Info(TAG, $"{action} at x={current.X}, y={current.Y}");

            return true;
        }
    }
}