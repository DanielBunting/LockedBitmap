using System.Drawing;
using System;
using System.Collections.Generic;
using LockedBitmapUtil.ColorComparators;

namespace LockedBitmapUtil.Extensions
{
    public static class LockedBitmapExtensions
    {
        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="firstOccurence">a variable that is set if the image is found, or defaults to -1, -1.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, out Point firstOccurence)
        => DoesImageExist(lockedHaystack, lockedNeedle, out firstOccurence, new DefaultColorComparator());

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="colorComparator"> a custom colour comparator, that allows different check types to be implemented.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, out Point firstOccurence, IColorComparator colorComparator)
        {
            for (int hayX = 0; hayX < lockedHaystack.Width; hayX++)           
            {
                for (int hayY = 0; hayY < lockedHaystack.Height; hayY++)
                {
                    var canBeFound = true;

                    for (int needleX = 0; needleX < lockedNeedle.Width && canBeFound; needleX++)
                        for (int needleY = 0; needleY < lockedNeedle.Height && canBeFound; needleY++)
                            canBeFound = colorComparator.IsSame(lockedHaystack.GetPixel(hayX + needleX, hayY + needleY), lockedNeedle.GetPixel(needleX, needleY));

                    if (canBeFound)
                    {
                        firstOccurence = new Point(hayX, hayY);
                        return true;
                    }
                }
            }

            firstOccurence = new Point(-1, -1);
            return false;
        }


        /// <summary>
        /// Gets all occurences of an image occuring.
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching within.</param>
        /// <param name="lockedNeedle">the image we are searching for.</param>
        /// <returns></returns>
        public static IEnumerable<Point> GetAllOccurences(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle)
        => lockedHaystack.GetAllOccurences(lockedNeedle, new DefaultColorComparator());


        /// <summary>
        /// Gets all occurences of an image occuring.
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching within.</param>
        /// <param name="lockedNeedle">the image we are searching for.</param>
        /// <param name="colorComparator">A custom colour comparator that allows custom rules to be set for the matching of colours.</param>
        /// <returns></returns>
        public static IEnumerable<Point> GetAllOccurences(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, IColorComparator colorComparator)
        {
            for (int hayX = 0; hayX < lockedHaystack.Width; hayX++)
            {
                for (int hayY = 0; hayY < lockedHaystack.Height; hayY++)
                {
                    bool couldBeFound = true;
                    for (int needleX = 0; needleX < lockedNeedle.Width && couldBeFound; needleX++)
                        for (int needleY = 0; needleY < lockedNeedle.Height && couldBeFound; needleY++)
                            couldBeFound = colorComparator.IsSame(lockedHaystack.GetPixel(hayX + needleX, hayY + needleY), lockedNeedle.GetPixel(needleX, needleY));

                    if(couldBeFound)
                        yield return new Point(hayX, hayY);
                }
            }
        }

        /// <summary>
        /// Creates a new bitmap of the secor specified.
        /// 
        /// note: This does not dispose the original locked image - it creates a new one. 
        /// </summary>
        /// <param name="source">the source image, we are taking a sector from.</param>
        /// <param name="xOffset">The x offset of where the sector should be calculated from.</param>
        /// <param name="yOffset">The y offset of where the sector should be calculated from.</param>
        /// <param name="width">The width of the cropped output.</param>
        /// <param name="height">The height of the cropped output.</param>
        /// <returns>A new currently locked LockedBitmap object, that is constructed of the specified sector.</returns>
        public static LockedBitmap Crop(this LockedBitmap source, int xOffset, int yOffset, int width, int height) 
        {
            if (source.Width < xOffset + width || source.Height < yOffset + height)
                throw new IndexOutOfRangeException("The specified sector exceeds the range of the source.");

            var newImage = new LockedBitmap(new Bitmap(width, height));
            newImage.LockBits();

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    newImage.SetPixel(x, y, source.GetPixel(x + xOffset, y + yOffset));

            return newImage;
        }

        /// <summary>
        /// Creates a new bitmap of the size specified.
        /// 
        /// note: This does not dispose the original locked image - it creates a new one. 
        /// </summary>
        /// <param name="source">the image which we are using as a source to create the new image.</param>
        /// <param name="width">Width of the new re-sized image.</param>
        /// <param name="height">height of the new re-sized image.</param>
        /// <returns>A new currently locked LockedBitmap object that is of the size specified.</returns>
        public static LockedBitmap Resize(this LockedBitmap source, int width, int height)
        {
            if (width < 1 || height < 1)
                throw new IndexOutOfRangeException("The re-sized image needs to be at least one pixel wide/high");

            float xScalar = source.Width / (float)width;
            float yScalar = source.Height / (float)height;

            var newImage = new LockedBitmap(new Bitmap(width, height));
            newImage.LockBits();

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    newImage.SetPixel(x, y, source.GetPixel((int)(x * xScalar), (int)(y * yScalar)));

            return newImage;
        }

        /// <summary>
        /// Returns a new, greyscaled version of the original image passed in. 
        /// 
        /// note: This does not dispose the original locked image - it creates a new one. 
        /// </summary>
        /// <param name="source">the image we wish to greyscale. </param>
        /// <returns> a new, currently locked LockedBitmap object that is greyscaled.</returns>
        public static LockedBitmap GreyScale(this LockedBitmap source)
        {
            var newImage = new LockedBitmap(new Bitmap(source.Width, source.Height));
            newImage.LockBits();

            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    var originalColor = source.GetPixel(x, y);
                    var greyScale = ColourToBrightness(originalColor);
                    newImage.SetPixel(x, y, Color.FromArgb(originalColor.A, greyScale, greyScale, greyScale));
                }
            }

            return newImage;
        }

        /// <summary>
        /// Converts an image from being a range of values, to a binary image of above, or below a brightness threshold. 
        /// 
        /// note: This does not dispose the original locked image - it creates a new one. 
        /// </summary>
        /// <param name="source">The image we will be 'binarizing'.</param>
        /// <param name="brightnessThreshold">The brightness threshold at which we decide a colour is darker/brighter.</param>
        /// <param name="brighter">the colour we set those darker than the threshold.</param>
        /// <param name="darker">The colour we set those darker than the threshold.</param>
        /// <returns> a new, currently locked LockedBitmap object that is binarized. </returns>
        public static LockedBitmap ToBinaryImage(this LockedBitmap source, int brightnessThreshold, Color brighter, Color darker)
        {
            var newImage = new LockedBitmap(new Bitmap(source.Width, source.Height));
            newImage.LockBits();

            for (int y = 0; y < source.Height; y++)
                for (int x = 0; x < source.Width; x++)
                    newImage.SetPixel(x, y, source.GetPixel(x, y)
                        .ColourToBrightness() > brightnessThreshold
                        ? brighter
                        : darker);
            return newImage;
        }

        private static int ColourToBrightness(this Color colour)
            => (int)(colour.R * 0.3 + colour.G * 0.59 + colour.B * 0.11);
    }
}
