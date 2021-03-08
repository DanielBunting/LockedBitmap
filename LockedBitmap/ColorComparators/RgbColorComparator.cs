using System.Drawing;

namespace LockedBitmapUtil.ColorComparators
{
    public class RgbColorComparator : IColorComparator
    {
        public bool IsSame(Color left, Color right)
        => left.R == right.R &&
           left.G == right.G &&
           left.B == right.B;
    }
}
