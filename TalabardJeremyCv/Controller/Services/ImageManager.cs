using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace TalabardJeremyCv.Controller.Services
{
    public static class ImageManager
    {

        private const int _downloadImageTimeoutInSeconds = 15;

        private static async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        //Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                return null;
            }
        }


        public static Bitmap GetBitmapFromPath(string path)
        {
            byte[] bytesImages = File.ReadAllBytes(path);
            return BitmapFactory.DecodeByteArray(bytesImages, 0, bytesImages.Length);
        }


        public static async Task<Bitmap> GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            byte[] imageBytes = await DownloadImageAsync("https://www.dropbox.com/s/3fhqsy8ex5994nu/jeremy_photo.JPG?dl=0");
            string download = Encoding.ASCII.GetString(imageBytes);
            Console.WriteLine(download);
            if (imageBytes != null && imageBytes.Length > 0)
            {
                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            return imageBitmap;
        }

        static Dictionary<string, Drawable> cache = new Dictionary<string, Drawable>();

        public static Drawable Get(Context context, string url)
        {
            if (!cache.ContainsKey(url))
            {
                var drawable = Drawable.CreateFromStream(context.Assets.Open(url), null);

                cache.Add(url, drawable);
            }

            return cache[url];
        }
    }
}