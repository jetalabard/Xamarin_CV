using System.IO;
using Android.Graphics;

namespace Cv_Core
{
    public class ImageManager<T>
    {
        private static ImageManager<T> _Instance = null;
        private static string _ImagePath;

        private ICreateImage<T> _Creator;

        public ImageManager(string imagePath, ICreateImage<T> creator  = null)
        {
            _ImagePath = imagePath;
            _Creator = creator;
        }

        public static ImageManager<T> GetInstance(string imagePath="", ICreateImage<T> creator = null)
        {
            if(_Instance == null)
            {
                _Instance = new ImageManager<T>(imagePath, creator);
            }
            return _Instance;
        }

        public T GetImageFromPath(string ImageName)
        {
            T image = default;
            if (!string.IsNullOrEmpty(ImageName))
            {
                string imagePath = System.IO.Path.Combine(_ImagePath, ImageName);
                image= _Creator.CreateImage(imagePath);
            }
            return image;
            
        }


    }
}