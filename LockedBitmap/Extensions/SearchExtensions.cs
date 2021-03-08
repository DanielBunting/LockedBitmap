using LockedBitmapUtil.ColorComparators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LockedBitmapUtil.Extensions
{
    public static class SearchExtensions
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

                    if (couldBeFound)
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
    }
}
