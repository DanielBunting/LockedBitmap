using System.Drawing;

namespace LockedBitmapUtil.Extensions
{
    public static class FluentConversions
    {
        public static LockedBitmap ToLockedBitmap(this Bitmap bitmap)
        {
            var newLockedBitmap = new LockedBitmap(bitmap);
            newLockedBitmap.LockBits();

            return newLockedBitmap;
        }

        public static Bitmap ToBitmap(this LockedBitmap lockedBitmap)
        {
            try
            {
                lockedBitmap.UnlockBits();
                return lockedBitmap.Source;
            }
            finally
            {
                lockedBitmap.Dispose();
            }
        }
    }
}
