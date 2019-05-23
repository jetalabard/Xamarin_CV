using Android.Graphics;
using Cv_Core;
using System.IO;

namespace TalabardJeremyCv.Controller
{
    public class AndroidCreateImage : ICreateImage<Bitmap>
    {
        public Bitmap CreateImage(string imagePath)
        {
            byte[] bytesImages = File.ReadAllBytes(imagePath);
            return BitmapFactory.DecodeByteArray(bytesImages, 0, bytesImages.Length);
        }
    }
}