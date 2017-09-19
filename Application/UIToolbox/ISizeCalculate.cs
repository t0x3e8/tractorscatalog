using System.Drawing;

namespace Enceladus.UIToolbox
{
    public interface ILocationDepended
    {
        int UpdateSize(int freeWidthToUse);
        Point Location { get; set; }
        Size Size { get; set; }
        bool LastInRow { get; set; }
        int Order { get; set; }
    }
}
