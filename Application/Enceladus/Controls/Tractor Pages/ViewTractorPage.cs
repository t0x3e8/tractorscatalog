using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api;
using System.IO;
using Enceladus.UIToolbox;
using Enceladus.StringLibrary;

namespace Enceladus
{
    partial class ViewTractorPage : TractorBasePage
    {
        #region Fields and Properties
        protected TractorPresenter presenter;
        private readonly string noImageText;
        private readonly string noDVDText;
        #endregion

        #region Constructors
        public ViewTractorPage(TractorPresenter presenter)
        {
            InitializeComponent();

            this.NoImageWarning.Font = Defines.BigBoldFont;
            this.NoImageWarning.Text = string.Empty;
            this.noDVDText = ResourceReader.GetString("MsgNoImageInsertDVD");
            this.noImageText = ResourceReader.GetString("MsgNoPictureAvailable");
            this.NoImageWarning.Visible = false;
            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.pbImage.Visible = false;
            this.pbImage.Image = null;

            if (!string.IsNullOrEmpty(tractor.Bild1))
            {
                FileInfo imagePath = ResourcesFinder.ResourcePath(tractor.Bild1, ResourceType.Picture);
                if (imagePath != null)
                {
                    this.pbImage.Visible = true;
                    this.NoImageWarning.Visible = false;
                    this.pbImage.Image = Image.FromFile(imagePath.FullName);
                }
                else
                {
                    this.NoImageWarning.Text = this.noDVDText;
                    this.NoImageWarning.Visible = true;
                }
            }
            else
            {
                this.NoImageWarning.Text = this.noImageText;
                this.NoImageWarning.Visible = true;
            }
        }
        #endregion
    }
}