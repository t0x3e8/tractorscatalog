using System.Drawing;

namespace Enceladus.UIToolbox
{
    public interface ICheckBoxLogic
    {
        string Text { get; set; }
        Point TextLocation { get; set; }
        bool IsChecked { get; set; }
        Point Location { get; set; }
        string Key { get; set; }
        void ItemClicked();
    }
}
