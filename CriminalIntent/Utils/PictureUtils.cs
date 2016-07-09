using System;
using Android.App;
using Android.Graphics;

namespace CriminalIntent.Utils
{
    public class PictureUtils
    {
        public static Bitmap GetScaledBitmap(String path, int destWidth, int destHeight)
        {
            // Read in the dimensions of the image on disk
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(path, options);

            float srcWidth = options.OutWidth;
            float srcHeight = options.OutHeight;

            // Figure out how much to scale down by
            int inSampleSize = 1;
            if (srcHeight > destHeight || srcWidth > destWidth)
            {
                if (srcWidth > srcHeight)
                {
                    inSampleSize = (int) Math.Round(srcHeight / destHeight);
                }
                else {
                    inSampleSize = (int) Math.Round(srcWidth / destWidth);
                }
            }

            options = new BitmapFactory.Options();
            options.InSampleSize = inSampleSize;

            // Read in and create final bitmap
            return BitmapFactory.DecodeFile(path, options);
        }

        public static Bitmap GetScaledBitmap(String path, Activity activity)
        {
            Point size = new Point(); 
            activity.WindowManager.DefaultDisplay.GetSize(size);
            return GetScaledBitmap(path, size.X, size.Y);
        }
    }
}

