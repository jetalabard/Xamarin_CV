using System.IO;
using Android.Graphics;

namespace Cv_Core
{
    public class ImageManager
    {
        private static ImageManager _Instance = null;
        private static string _ImagePath;

        public ImageManager(string imagePath)
        {
            _ImagePath = imagePath;
        }

        public static ImageManager GetInstance(string imagePath="")
        {
            if(_Instance == null)
            {
                _Instance = new ImageManager(imagePath);
            }
            return _Instance;
        }

        public static Bitmap GetBitmapFromPath(string ImageName)
        {
            string imagePath = System.IO.Path.Combine(_ImagePath, ImageName);
            byte[] bytesImages = File.ReadAllBytes(imagePath);
            return BitmapFactory.DecodeByteArray(bytesImages, 0, bytesImages.Length);
        }


    }
}