using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;
using Enceladus.Api;
using Enceladus.Api.UI;

namespace Enceladus
{
    internal delegate void TractorUpdateCallback(Tractor tractor);
    internal delegate void TractorSetUpdateCallback(IList<TractorSearchResult> tractors);

    partial class TractorBasePage : PageBase
    {
        protected int currentFontSize;
        public virtual int CurrentFontSize
        {
            get { return this.currentFontSize; }
            set
            {
                if (this.currentFontSize != value)
                {
                    this.currentFontSize = value;
                    this.ApplyNewFontSize(this);                    
                }
            }
        }

        void ApplyNewFontSize(Control parentControl)
        {
            foreach (Control childControl in parentControl.Controls)
            {
                this.ApplyNewFontSize(childControl);
                if (childControl is IResizableClient && (childControl as IResizableClient).SupportResizing)
                {
                    (childControl as IResizableClient).ApplyFontSize(this.currentFontSize);
                }
            }
        }

        public TractorBasePage()
        {
            InitializeComponent();
            this.lblBrandName.Font = Defines.BigUnderlineFont;
            this.lblBrandName.ForeColor = Defines.CarrotColor;
            this.lblYear.Font = Defines.BigFont;
            this.lblYear.ForeColor = Defines.CarrotColor;
            this.lblBrandType.Font = Defines.BigFont;
            this.lblBrandType.ForeColor = Defines.CarrotColor;
        }

        public virtual void BindTractor(Tractor tractor)
        {
            this.lblBrandName.Label = tractor.DisplayName;
            this.lblBrandType.Label = tractor.Antriebsart;
            this.lblYear.Label = tractor.LetzteAktualisierung;
        }

        private void TractorBasePage_Load(object sender, EventArgs e)
        {

        }
    }
}
