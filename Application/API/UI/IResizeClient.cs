using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Enceladus.Api.UI
{
    public delegate void PokeDelegate();

    public interface IResizableClient
    {
        /// <summary>
        /// This method is run on control which needs to adapt a new font size.
        /// </summary>
        /// <param name="fontSizeChange"> this is a font size from 1 to 5 where 1 is the smallest and 5 is the biggest</param>
        void ApplyFontSize(int fontSize);
        FontSize CurrentFontSize { get; }
        /// <summary>
        /// The maximal expected font size. This value can be from 1 to 5
        /// </summary>
        int MaximalExpectedFontSize { get; }
        PokeDelegate InformResizer { get; set; }
        bool SupportResizing { get; set; }
    }

    public enum FontSize
    {
        Tiny = 1, Small = 2, Normal = 3, Big = 4, Huge= 5
    }
}
