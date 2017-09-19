using System.Drawing;

namespace Enceladus.UIToolbox
{
    internal class CheckBoxLogic : ICheckBoxLogic
    {
        public string Text { get; set; }
        public Point TextLocation { get; set; }
        public bool IsChecked { get; set; }
        public Point Location { get; set; }
        public string Key { get; set; }

        public CheckBoxLogic()
        {

        }

        public CheckBoxLogic(string text, string key, bool isChecked, Point location, Point textLocation)
        {
            this.Text = text;
            this.Key = key;
            this.IsChecked = isChecked;
            this.Location = location;
            this.TextLocation = textLocation;
        }

        public virtual void ItemClicked()
        {
            this.IsChecked = !this.IsChecked;
        }
    }
}
