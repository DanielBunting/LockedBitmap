using System.Drawing;

namespace LockedBitmapCore
{
    public interface IColorComparator
    {
        bool IsSame(Color left, Color right);
    }
}