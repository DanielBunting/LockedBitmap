using System.Drawing;

namespace LockedBitmapUtil.ColorComparators
{
    public class DefaultColorComparator : IColorComparator
    {
        public bool IsSame(Color left, Color right)
        => left.R == right.R &&
           left.G == right.G &&
           left.B == right.B &&
           left.A == right.A;
    }
}
