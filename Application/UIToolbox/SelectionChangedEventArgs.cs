using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.UIToolbox
{
    public class SelectionChangedEventArgs : EventArgs
    {
        private Tab previousSelection;
        public Tab PreviousSelection
        {
            get { return this.previousSelection; }
        }

        private Tab selection;
        public Tab ActiveSelection
        {
            get { return this.selection; }
        }

        public SelectionChangedEventArgs(Tab previousSelection, Tab selection)
        {
            this.previousSelection = previousSelection;
            this.selection = selection;
        }
    }
}
