using System;
using System.Drawing;
using System.IO;

namespace Lab_3_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] imageFiles = Directory.GetFiles("images");
            int k = 0;

            Func<Bitmap, Bitmap> grayScale = image =>
            {
                Bitmap grayBitmap = new Bitmap(image.Width, image.Height);

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                        grayBitmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                    }
                }
                return grayBitmap;
            };

            /*Func<Bitmap, Bitmap> flipHorizontal = image =>
            {
                Bitmap flippedBitmap = new Bitmap(image.Width, image.Height);

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        flippedBitmap.SetPixel(image.Width - i - 1, j, image.GetPixel(i, j));
                    }
                }

                return flippedBitmap;
            };*/
            
            Action<Bitmap> displayImage = image =>
            {
                image.Save($"images\\output_{Path.GetFileNameWithoutExtension(imageFiles[k])}.png");           
            };

            for (int i = 0; i < imageFiles.Length; i++)
            {
                Bitmap original = new Bitmap(imageFiles[i]);
                Bitmap gray = grayScale(original);
                //Bitmap flipped = flipHorizontal(original);

                displayImage(gray);
                //displayImage(flipped);

                k++;
            }

            Console.WriteLine("Image processing complete.");
        }
    }
}
