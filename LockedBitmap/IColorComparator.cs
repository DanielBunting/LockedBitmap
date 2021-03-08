using System.Drawing;

namespace LockedBitmapUtil
{
    public interface IColorComparator
    {
        bool IsSame(Color left, Color right);
    }
}