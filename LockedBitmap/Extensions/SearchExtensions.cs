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
        /// <param name="searchRectangle">The Rectangle within we wish to search</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Rectangle searchRectangle)
        => DoesImageExist(lockedHaystack, lockedNeedle, searchRectangle, out Point _, new DefaultColorComparator());

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="inner">The inner point of the search rectangle.</param>
        /// <param name="outer">The outer point of the search rectangle.</param>
        /// <param name="colorComparator"> a custom colour comparator, that allows different check types to be implemented.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Point inner, Point outer, IColorComparator colorComparator)
        => DoesImageExist(lockedHaystack, lockedNeedle, new Rectangle(inner.X, inner.Y, outer.X - inner.X, outer.Y - inner.Y), out _, colorComparator);

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="inner">The inner point of the search rectangle.</param>
        /// <param name="outer">The outer point of the search rectangle.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Point inner, Point outer)
        => DoesImageExist(lockedHaystack, lockedNeedle, new Rectangle(inner.X, inner.Y, outer.X - inner.X, outer.Y - inner.Y), out _, new DefaultColorComparator());

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="inner">The inner point of the search rectangle.</param>
        /// <param name="outer">The outer point of the search rectangle.</param>
        /// <param name="firstOccurence">a variable that is set if the image is found, or defaults to -1, -1.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Point inner, Point outer, out Point firstOccurence)
        => DoesImageExist(lockedHaystack, lockedNeedle, new Rectangle(inner.X, inner.Y, outer.X - inner.X, outer.Y - inner.Y), out firstOccurence, new DefaultColorComparator());

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="inner">The inner point of the search rectangle.</param>
        /// <param name="outer">The outer point of the search rectangle.</param>
        /// <param name="firstOccurence">a variable that is set if the image is found, or defaults to -1, -1.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Point inner, Point outer, out Point firstOccurence, IColorComparator colorComparator)
        => DoesImageExist(lockedHaystack, lockedNeedle, new Rectangle(inner.X, inner.Y, outer.X - inner.X, outer.Y - inner.Y), out firstOccurence, colorComparator);

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="searchRectangle">The Rectangle within we wish to search</param>
        /// <param name="firstOccurence">a variable that is set if the image is found, or defaults to -1, -1.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Rectangle searchRectangle, out Point firstOccurence)
        => DoesImageExist(lockedHaystack, lockedNeedle, searchRectangle, out firstOccurence, new DefaultColorComparator());

        /// <summary>
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="searchRectangle">The Rectangle within we wish to search</param>
        /// <param name="colorComparator"> a custom colour comparator, that allows different check types to be implemented.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Rectangle searchRectangle, IColorComparator colorComparator)
        => DoesImageExist(lockedHaystack, lockedNeedle, searchRectangle, out _, colorComparator);

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
        /// A helper function that helps check if one LockedBitmap object, exists within another. 
        /// </summary>
        /// <param name="lockedHaystack">The image we are searching in.</param>
        /// <param name="lockedNeedle">The image we are searching for an occurence of.</param>
        /// <param name="searchRectangle">The Rectangle within we wish to search</param>
        /// <param name="colorComparator"> a custom colour comparator, that allows different check types to be implemented.</param>
        /// <returns>If the 'needle' was found within the 'haystack'.</returns>
        public static bool DoesImageExist(this LockedBitmap lockedHaystack, LockedBitmap lockedNeedle, Rectangle searchRectangle, out Point firstOccurence, IColorComparator colorComparator)
        {
            for (int hayX = searchRectangle.Left; hayX < lockedHaystack.Width || hayX > searchRectangle.Right; hayX++)
            {
                for (int hayY = searchRectangle.Top; hayY < lockedHaystack.Height || hayY > searchRectangle.Bottom; hayY++)
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
    }
}
