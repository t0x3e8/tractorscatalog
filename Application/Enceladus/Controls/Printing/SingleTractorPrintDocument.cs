using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Enceladus.Api;
using Enceladus.StringLibrary;
using Enceladus.UIToolbox;

namespace Enceladus.Controls
{
    class SingleTractorPrintDocument: PrintDocument
    {
        #region Fields
        private int _currentPage;
        private Font _font;
        private float _topPosition = 0;
        private SolidBrush _fontSB;
        private readonly static int RowSpace = 5;
        #endregion

        #region Properties
        public Tractor Tractor { get; set; }
        #endregion

        #region Constructors
        public SingleTractorPrintDocument()
        {
            this._font = PrintingHelper.BuildDefaultFont();
            this._fontSB = new SolidBrush(Color.Black);
            this._currentPage = 1;
        }
        #endregion

        #region Methods
        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = false;
            e.PageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            try
            {
                this._topPosition = 0;

                this.DrawHeader(e);
                this.DrawTractorName(e);

                if (this._currentPage == 1)
                {
                    this.DrawMotorArea(e);
                    this.DrawGeriebeArea(e);
                    this.DrawZapfwelleArea(e);
                    this.DrawHydraulikArea(e);
                    this.DrawGewichteUndMasseArea(e);
                    this.DrawPreisArea(e);
                    this.DrawBesonderheitArea(e);
                    this.DrawMotorIIArea(e);
                    this.DrawKraftVerbrauchArea(e);
                    this.DrawGetriebeArea(e);

                    e.HasMorePages = true;
                }
                else if (this._currentPage == 2)
                {
                    this.DrawZapfwelle2Area(e);
                    this.DrawHubwerkArea(e);
                    this.DrawHydraulik2Area(e);
                    this.DrawAchsenArea(e);
                    this.DrawGewichtArea(e);
                    this.DrawWartungArea(e);
                    this.DrawKabineArea(e);
                }

                this.DrawFooter(e);
                this._currentPage++;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "SingleTractorPrintDocument.OnPrintPage", "An exception happened while printing tractor overview: " + ex.ToString());
            }
        }

        #region Second page
        private void DrawKabineArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_KabinePanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// kabine
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_KabineText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// kabine Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kabine, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Niedrig kabine
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_Niedrig_KabineText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Niedrig kabine Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Niedrig_Kabine, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// gefedert
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 90, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_KabinenfederungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// gefedert Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kabinenfederung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Lauftstarke extern
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 95, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_LautstaerkeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lauftstarke Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Lautstaerke, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lauftstarke unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.dB_A), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Klimaanlage
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_Klima_AnlageText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Klimaanlage Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Klima_Anlage, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Rueckfahreinrchtg
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 170, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_RueckfahreinrchtgText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Rueckfahreinrchtg Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Rueckfahreinrchtg, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// ISO-Bus
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_ISO_BusText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// ISO-Bus Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ISO_Bus, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// CAN-BUS
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 135, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_CAN_BusText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// CAN-BUS Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.CAN_Bus, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Automatische Anhangerkupplung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 340, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AutomatAHKText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Automatische Anhangerkupplung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AutomatAHK, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Schnellverstellbar
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AhkSchnellverstText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Schnellverstellbar Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AHKschnellverst, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Stutzlast
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_StuetzlastAHKText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Stutzlast Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.StuetzlastAHK, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Motor Getriebe Management
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 340, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_Motor_Getriebe_ManagementText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Motor Getriebe Management Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Motor_Getriebe_Management, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Zugpendel
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_ZugpendelText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zugpendel Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Zugpendel, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Auto-Lenksystem
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 135, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AutoLenksystemText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Auto-Lenksystem Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AutoLenksystem, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawWartungArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_WartungPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Olmenge motor
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelMotorText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olmenge motor Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelMotor, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olmenge motor unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Olwechsel
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelwechselMotorStdText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olwechsel Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelwechselMotorStd, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olwechsel unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l_h), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Olverrat extern
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelExternText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olverrat extern Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Oelextern, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olverrat extern unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Getriebe Hydraulike
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelGetriebeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Getriebe Hydraulike Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelGetriebe, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Getriebe Hydraulike unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Olwechsel2
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelwechselGetrStdText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olwechsel2 Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelwechselGetrStd, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olwechsel2 unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l_h), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3rd line
            /// Olmenge Hydraulik
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelHydraulikText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olmenge Hydraulik value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelHydraulikSerie, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Olmenge Hydraulik unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// OelHydraulikOption
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelHydraulikOptionText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// OelHydraulikOption value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelHydraulikOption, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// OelHydraulikOption unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Hour), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// OelwechselHydrStd
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 135, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_OelwechselHydrStdText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// OelwechselHydrStd value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.OelwechselHydrStd, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// OelwechselHydrStd unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Hour), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion
            
            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawGewichtArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_GewichtePanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Gesamtgewicht
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 170, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_GesamtgewichtText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gesamtgewicht Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gesamtgewicht, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gesamtgewicht unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Leergewicht
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_LeergewichtText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Leergewicht Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Leergewicht, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Leergewicht unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Maximal zulassiges Gesamtgewicht
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 250, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_ZulGesamtgewMaxText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximal zulassiges Gesamtgewicht Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZulGesamtgewmax, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximal zulassiges Gesamtgewicht unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nutzlast
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_NutzlastText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nutzlast Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Nutzlast, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nutzlast unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Lange
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 250, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_LaengeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lange Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Laenge, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lange unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Breite
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_BreiteText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Breite Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Breite, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Breite unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hoehe
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 65, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_HoeheText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hoehe Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Hoehe, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hoehe unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawAchsenArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AchsenPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Lenkhilfe
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_LenkhilfeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lenkhilfe Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Lenkhilfe, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hydrostatisch
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 215, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_HydrostatLenkungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hydrostatisch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HydrostatLenkung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Lenkrad verstellbar
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 260, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_LenkradVerstellbarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lenkrad verstellbar Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Lenkradverstellbar, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Wendekreis
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_WendekreisText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wendekreis Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Wendekreis, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wendekreis unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.m), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Radstand
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 135, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_RadstandText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Radstand Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Radstand, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Radstand unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Bodenfreiheit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 130, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_BodenfreiheitText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bodenfreiheit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bodenfreiheit, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bodenfreiheit unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Spurweite vorne
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_SpurweiteVorneText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Spurweite vorne Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Spurweitevorne, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Spurweite Vorne unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// hinten
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_SpurweiteHintenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hinten Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Spurweitehinten, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hinten unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Bereifung vorne
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_BereifungVorneText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bereifung vorne Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 200, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bereifungvorne, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Bereifung hinten
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 140, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_BereifunghintenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bereifung hinten Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 205, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bereifunghinten, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// Differentialsperre Heck
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_DifferentialspText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Differentialsperre Heck Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 140, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Differentialsp, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Ausfuhrung 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_SchaltbarkeitHADiffsperreText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Ausfuhrung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 205, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.SchaltbarkeitHADiffsperre, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 6-th Line
            /// Allradantrieb
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AllradantriebText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// allradantrieb Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Allradantrieb, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// VaDiffsperre
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 250, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_VADiffsperreText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// VaDiffsperre Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.VADiffsperre, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 7-th Line
            /// Vierradbremse
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_VierradbremseText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Vierradbremse Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Vierradbremse, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Druckluftbremse
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_DruckluftbremseText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// elektrihydraulisch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Druckluftbremse, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Vorderachsfederung
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 325, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_VA_FederungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Vorderachsfederung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString( this.Tractor.VA_Federung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 8-th Line
            /// Achsgewicht vorne
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AchsgewichtVorneText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achsgewicht vorne Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Achsgewichtvorne, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achsgewicht vorne Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Achsgewicht hinten
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AchsgewichtHintenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achsgewicht hinten Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Achsgewichthinten, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achsgewicht hinten Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Gewichtsverteilung
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 170, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_GewichtsverteilungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gewichtsverteilung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gewichtsverteilung, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gewichtsverteilung Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Percent), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 9-th Line
            /// Achlast vorne
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AchslastVorneText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achlast vorne Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Achslastvorne, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achlast vorne Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Achlast hinten
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsIII_AchslasthintenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achlast hinten Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Achslasthinten, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Achlast hinten Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawHydraulik2Area(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HyndraulicPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Pumpenleistung
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HydraulikPumpenleistungLProText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// pumpenleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HydraulikPumpenleistunglproMin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// pumpenleistung unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l_min), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nenndruck
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 155, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HydraulikNenndruckBarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// nenndruck Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HydraulikNenndruckbar, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// nenndruck unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.bar), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Bauart
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ArtHydrauliksystemText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bauart Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ArtHydrauliksystem, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Anzahl Steuergerate
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_AnzahlSteuerventileText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Anzahl Steuergerate Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AnzahlSteuerventile, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Abreisskupplungen
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 165, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_AbreisskupplungenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Abreisskupplungen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Abreisskupplungen, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawHubwerkArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HoistPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// maxHubkraft
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 165, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HubkraftMaximalDanText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// maxHubkraft Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HubkraftmaximaldaN, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// maxHubkraft unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.daN), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// durchgehend
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HubkraftDurchgehendDanText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// durchgehend Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HubkraftdurchgehenddaN, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// durchgehend unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.daN), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// EHR
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_EhrText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// EHR Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.EHR, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Kategorie
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 105, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_HubwerkKategorieText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Kategorie Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HubwerkKategorie, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Zusatzzylinder
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Zusatz_HubzylinderText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zusatzzylinder Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Zusatz_Hubzylinder, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Fernbedienung Heck
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_FernbedienungimHeckText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Fernbedienung Heck Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.FernbedienungimHeck, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Schnellkuppler 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_SchnellkupplerText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Schnellkuppler Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Schnellkuppler, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-th Line
            /// Regekung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 165, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_RegulungText"), new Font(f, FontStyle.Italic | FontStyle.Underline), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Oberlenker
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Oberlenker_RegelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Oberlenker Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Oberlenker_Regelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Unterlenker
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Unterlenker_RegelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Unterlenker Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Unterlenker_Regelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Lage
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_LageregelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lage Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Lageregelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Schwimm
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_SchwimmregelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Schwimm Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Schwimmregelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Zugkraft
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 415, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZugwiderstandsregelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zugkraft Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Zugwiderstandsregelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Misch
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_MischregelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Misch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Mischregelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Schlupf
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_SchlupfregelungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Schlupf Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Schlupfregelung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// Fronthubwerk / Frontzapfwelle
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 415, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_FronthubwerkUndZWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            ///  Fronthubwerk / Frontzapfwelle Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.FronthubwerkundZW, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Fronthubkraft
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_FronthubwerkHubkraftdaNText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Fronthubkraft Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.FronthubwerkHubkraftdaN, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Fronthubkraft
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.daN), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawZapfwelle2Area(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_PowerTakeOffPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Drehzahlen
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_DrehzahlenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Drehzahlen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 320, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Drehzahlen, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// drehzahlen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Maximale Drehzahlen
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_UminmaximalText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximale Drehzahlen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 250, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Front_ZWUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximale Drehzahlen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Maximale Zapfwellenleistung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_kWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximale Zapfwellenleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_kW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximale Zapfwellenleistung Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Bauart
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 105, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_BauartText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bauart Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Bauart, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Sparzapfwalle
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Spar_ZWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Sparzapfwalle Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Spar_ZW, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Wegzapfwelle
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 195, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Weg_ZWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wegzapfwelle Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Weg_ZW, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Fernbedienung Heck
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 220, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_FernbedienungimHeckText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wegzapfwelle Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZWFernbedienung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// Stummelzahl
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_StummelzahlText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Stummelzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Stummelzahl, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Zapfwellenprofil
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 195, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZW_ProfilText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zapfwellenprofil Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Profil, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// lastschaltbar
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_ZWlastschaltbarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// lastschaltbar Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZWlastschaltbar, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 6-th Line
            /// 540
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 405, curRect.Height);
            e.Graphics.DrawString("540", f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 540 Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Mot_Umin_540, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 540 unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// 540e
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString("540E", f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 540 Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Mot_Umin_540E, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 540 unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            #region Double Line Header
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 250, f.GetHeight() * 2.5f);
            string leftSideHeader = String.Format("PrnZWDoubleLine", Environment.NewLine);
            e.Graphics.DrawString(leftSideHeader, new Font(f, FontStyle.Italic | FontStyle.Underline), this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += f.GetHeight() + RowSpace;
            #region 7-th Line
            /// 1000
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 405, f.GetHeight());
            e.Graphics.DrawString("1000", f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 1000 Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Mot_Umin_1000, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 1000 unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// 1000e
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString("1000E", f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 1000e Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Mot_Umin_1000, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// 1000e unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 8-th Line
            /// Normdrehzahl der Frontzapfwelle
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 300, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsII_Front_ZWUminText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Normdrehzahl der Frontzapfwelle Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 175, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Front_ZWUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Normdrehzahl der Frontzapfwelle unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }
        #endregion

        #region First Page
        private void DrawGetriebeArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 70, f.GetHeight());

            //Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_GearsPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Hersteller
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GetriebeherstellerText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hersteller Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Getriebehersteller, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Typ
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GetriebetypText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Typ Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Getriebetyp, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// gange
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GaengeVorwaertsText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// gange Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaengevorwaerts, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// ruckwarts
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GaengeRueckwaertsText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// ruckwarts Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaengerueckwaerts, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hochstgeschwindigkeit 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 250, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_EndgeschwindigkeitKmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstgeschwindigkeit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Endgeschwindigkeitkmh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstgeschwindigkeit 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.km_h), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Synchronisation 
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_SynchronisationText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Synchronisation Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 200, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Synchronisation, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// x30km
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 60, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_C30kmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// x30km Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.C30kmh, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// x40km
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 60, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_C40kmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// x40km Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.C40kmh, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// x50km
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 60, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_C50kmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// x50km Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.C50kmh, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// x60km
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 80, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_C60kmhundmehrText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// x60km Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.C60kmhundmehr, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Wunsch
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GetriebeWunschText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wunsch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 330, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.GetriebeWunsch, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Zahl schalthebel
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_ZahlDerSchalthebelText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zahl schalthebel Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZahlderSchalthebel, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// Lastschaltung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_LS_GetriebeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lastschaltung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.LS_Getriebe, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Voll lastschaltung
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_Voll_LS_GetriebeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Voll lastschaltung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Getriebevolllastschaltbar, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Anzahl Stufen
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_LSAnzahlStufenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Anzahl Stufen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.LSAnzahlStufen, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Stufenlos
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_StufenlosesCvtText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Stufenlos Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.StufenlosesCVT, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 6-th Line
            /// Wendegetriebe
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_WendegetriebeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// wendegetriebe Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Wendegetriebe, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Wendeschaltung
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_WendeschaltungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wendeschaltung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Wendeschaltung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Lastschaltbar
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_WgLastschaltbarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lastschaltbar Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.WGLastschaltbar, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// vorwahlbar
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_WgVorwahlbarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Lastschaltbar Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.WGVorwahlbar, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 7-th Line
            /// Kriechgetriebe
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_KriechgetriebeText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Kriechgetriebe Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 20, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kriechgetriebe, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Geschwindigkeit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 260, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_KriechgetriebeabText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// geschwindigkeit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kriechgetriebeab, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// geschwindigkeit Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.m_h), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 8-th Line
            /// Gange
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_Gaenge4Bis12KmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gange Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaenge4bis12kmh, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// UberGange
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_GaengeUeber15KmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// UberGange Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaengeueber15kmh, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawKraftVerbrauchArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 70, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_FuelConsumptionPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            
            #region 1-st Line
            /// tankinhalt
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, f.GetHeight());
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_TankinhaltLText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// tankinhalt Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Tankinhaltl, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// tankinhalt unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// anzahl tanks
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 115, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_AnzahlTanksText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// anzahl tanks value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AnzahlTanks, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// maximall
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 175, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_VerbrauchMaximalGkwhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// maximall Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.VerbrauchmaximalgkWh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// maximall unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Bestverbrauch
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BestverbrauchGkwhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bestverbrauch value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BestverbrauchgkWh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bestverbrauch unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 175, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Bestverbrauch2 value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bestverbrauchlproh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bestverbrauch unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l_h), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Bestverbrauch3
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BestverbrauchBeiDrehzahlText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bestverbrauch3 value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BestverbrauchbeiDrehzahl, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bestverbrauch3 unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.U_min), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// mittlerer
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MittlererOECD_VerbrauchgkWhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// mittlerer Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MittlererOECD_VerbrauchgkWh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// mittlerer unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Powermixmittel
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 115, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_rPowermixmittelText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());
            
            /// Powermixmittel value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.PowermixMittel, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Powermixmittel unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// AdBlueTankinhalt
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 105, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_AdBlueTankinhalt"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// AdBlueTankinhalt value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.AdBlueTankinhaltL, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// AdBlueTankinhalt unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 10, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawMotorIIArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 70, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MotorPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Hersteller
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_HerstellerMotorText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hersteller Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HerstellerMotor, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Typ
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MotortypText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Mit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MotorTyp, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Bauart
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BauartMotorText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bauart Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 130, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BauartMotor, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennleistung KW
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_NennleistungKWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennleistung KW Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NennleistungkW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennleistung KW Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 35, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennenleistung PS Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NennleistungPS, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung PS Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 35, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.PS), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennenleistung nach 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_EceoderisoText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung nach Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ECEoderISO, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Nenndrehzahl
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_NenndrehzahlUminText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nenndrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NenndrehzahlUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nenndrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hochstleistung
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MaximalleistungKWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MaximalleistungkW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstleistung Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Motordrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_PmaxAbUminUntenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Motordrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MaxleistungbeiUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Motordrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 4-th Line
            /// Kuhlung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_KuehlungText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Kuhlung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 130, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kuehlung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Zylinder
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_ZylinderzahlText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zylinder Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Zylinderzahl, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hubraum
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_HubraumCcmText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hubraum Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Hubraumccm, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hubraum Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm3), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// DrehmomentMax
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MdmaxNMText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// DrehmomentMax Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MDmaxNm, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// DrehmomentMax Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Nm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// bei motordrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MdmaxBeiDrehzahlText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bei motordrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.MDmaxbeiDrehzahl, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bei motordrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// bis motordrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MaxleistungBeiUminText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bis motordrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.PmaxabUminunten, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bis Motordrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 6-th Line
            /// Drehmomentanstieg
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_DrehmomentanstiegProzentText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// drehmomentanstieg Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.DrehmomentanstiegProzent, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// DrehmomentMax Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Percent), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// drehzahlabfall
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_DrehzahlabfallProzentText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bei motordrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.DrehzahlabfallProzent, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bei motordrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Percent), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 7-th Line
            /// Konstantleistungsbereich
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_KonstantleistungProzentText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// konstantleistungsbereich Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.KonstantleistungProzent, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// konstantleistungsbereich Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Percent), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// uberleistung
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 125, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_UberleistungKWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// uberleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.UberleistungkW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// uberleistung Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 8-th Line
            /// Powerboost
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BoostleistungKWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Powerboost Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 90, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BoostleistungkW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Powerboost Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Powerboost bei motordrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 115, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BoostleistungBeiUminText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Powerboost Bei Motordrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BoostleistungbeiUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            ///Powerboost Bei Motordrehzahl Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.U_min), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 9-th Line
            /// BohrungHub
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BohrungXHubText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// BohrungHub Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 140, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BohrungxHub, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// BohrungHub Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.mm), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Abgasnorm
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_AbgasnormText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Abgasnorm Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BohrungxHub, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 10th Line
            /// SCRKatalysator
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_SCRKatalysator"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// SCRKatalysator Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.SCRKatalysator, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Dieseloxydationskatalysator
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 205, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_Dieseloxydationskatalysator"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Dieseloxydationskatalysator Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Dieseloxydationskatalysator, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Dieselpartikelfilter
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 195, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_Dieselpartikelfilter"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Dieselpartikelfilter Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Dieselpartikelfilter, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawBesonderheitArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;

            #region 1-st Line
            /// Besonderheiten Area header
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight());
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_DetailsPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height;
            #region 2-nd Line
            /// Besonderheiten Value 
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight() * 3);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Besonderes, f, this._fontSB, curRect, PrintingHelper.GetTopLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Ausstattung Area header
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight());
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_EquipmentPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height;
            #region 4-th Line
            /// Ausstattung Value 
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight() * 3);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Ausstattung, f, this._fontSB, curRect, PrintingHelper.GetTopLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 5-th Line
            /// Prufberichte Area header
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight());
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_TestReportPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height;
            #region 6-th Line
            /// Prufberichte Value 
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 725, f.GetHeight() * 2);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Pruefberichte, f, this._fontSB, curRect, PrintingHelper.GetTopLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawPreisArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_PricePanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// preis
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_PreisVonEuroText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// preis Value 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.PreisvonEuro, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// preis Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.euro), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// preis bis
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_PreisBisEuroText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// preis bis Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.PreisbisEuro, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// preis bis Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 60, curRect.Height); ;
            e.Graphics.DrawString(StringManager.GetUnit(Units.euro), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawGewichteUndMasseArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, 2 * f.GetHeight());

            /// Area header
            string gewichte = String.Format(ResourceReader.GetString("TractorOverview_WeightPanelCaption"), Environment.NewLine);
            e.Graphics.DrawString(gewichte, new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// gesamtgewicht
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 190, f.GetHeight());
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_GesamtgewichtText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// gesamtgewicht Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gesamtgewicht, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// gesamtgewicht unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kg), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// wendekreis
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 205, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_WendekreisText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// wendekreis value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Wendekreis, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// wendekreis Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.m), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// bereifung
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_BereifungVorneText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// bereifung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 230, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bereifungvorne, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// hinten
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 85, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_BereifungHintenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hinten value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 230, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Bereifunghinten, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawHydraulikArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_HydraulikPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// kategorie
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_HubwerkKategorieText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// kategorie Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 140, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HubwerkKategorie, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Pumpenleistung
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 275, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_HydraulikPumpenleistungLProText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Pumpenleistung value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HydraulikPumpenleistunglproMin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Pumpenleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.l_min), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// hubkraft
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);;
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_HubkraftMaximalDanText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// hubkraft Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 80, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HubkraftmaximaldaN, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// hubkraft unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.daN), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// nenndruck
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 235, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_HydraulikNenndruckbarText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// nenndruck value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.HydraulikNenndruckbar, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// nenndruck Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.bar), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawZapfwelleArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_ZapfwellePanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Drehzahlen
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_ZW_DrehzahlenText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Drehzahlen Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 280, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_Drehzahlen, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Maximalle leistung
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 200, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorComparison_ZW_kWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximalle leistung unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 45, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ZW_kW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Maximalle leistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawGeriebeArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 80, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_GearsPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Hochstgeschwindigkeit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_EndgeschwindigkeitkmhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstgeschwindigkeit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Endgeschwindigkeitkmh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hochstgeschwindigkeit Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.km_h), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bauart
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 120, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_BauartText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            ///Bauart Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 265, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Getriebetyp, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Gangzahl
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_GaengeVorwaertsText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gangzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaengevorwaerts, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// BackSlash
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 10, curRect.Height);
            e.Graphics.DrawString("/", f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Gangzahl Value 2
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Gaengerueckwaerts, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Wunsch
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 140, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorOverview_GetriebeWunschText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Wunsch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 325, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.GetriebeWunsch, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawMotorArea(PrintPageEventArgs e)
        {
            Font f = PrintingHelper.BuildDefaultFont();
            this._topPosition += 5;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 70, f.GetHeight());

            /// Area header
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MotorPanelCaption"), new Font(f, FontStyle.Bold | FontStyle.Italic), this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            #region 1-st Line
            /// Bauart
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BauartMotorText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Bauart Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BauartMotor, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Mit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_MitText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Mit Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 160, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Kuehlung, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            ///Nenndrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_NenndrehzahlUminText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            ///Nenndrehzahl Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NenndrehzahlUmin, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            ///Nenndrehzahl
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 70, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.min_1), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 2-nd Line
            /// Zylinder
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_ZylinderzahlText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Zylinder Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Zylinderzahl, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Hubraum
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 100, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_HubraumCcmText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hubraum Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 50, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.Hubraumccm, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Hubraum Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 35, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.cm3), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennenleistung KW
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 110, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_NennleistungKWText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung KW Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NennleistungkW, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung KW Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 35, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.kW), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennenleistung KW Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 30, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.NennleistungPS, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung KW Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 35, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.PS), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Nennenleistung nach 
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_EceoderisoText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Nennenleistung nach Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.ECEoderISO, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region 3-rd Line
            /// Verbrauch
            curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, 180, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_VerbrauchMaximalGkwhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Verbrauch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.VerbrauchmaximalgkWh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Verbrauch Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// BestVerbrauch
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 160, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_BestverbrauchGkwhText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// BestVerbrauch Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.BestverbrauchgkWh, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// BestVerbrauch Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.gKWh), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Konstantleistung
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 150, curRect.Height);
            e.Graphics.DrawString(ResourceReader.GetString("TractorDetailsI_KonstantleistungProzentText"), f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Konstantleistung Value
            curRect = new RectangleF(curRect.Left + curRect.Width, curRect.Y, 40, curRect.Height);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(curRect));
            e.Graphics.DrawString(this.Tractor.KonstantleistungProzent, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            /// Konstantleistung Unit
            curRect = new RectangleF(curRect.Left + curRect.Width, this._topPosition, 50, curRect.Height);
            e.Graphics.DrawString(StringManager.GetUnit(Units.Percent), f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            #endregion

            this._topPosition += curRect.Height + RowSpace;
            #region Draw Line
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, this._topPosition, e.MarginBounds.Right, this._topPosition);
            #endregion
        }

        private void DrawTractorName(PrintPageEventArgs e)
        {
            Font f = new Font(this._font.FontFamily, 22, FontStyle.Bold);

            this._topPosition += 40;
            RectangleF curRect = new RectangleF(e.MarginBounds.Left, this._topPosition, e.MarginBounds.Width, f.GetHeight());

            /// Draw Line
            e.Graphics.DrawLine(new Pen(Color.Black, 3), curRect.X, curRect.Y, curRect.Right, curRect.Y);

            /// Draw Tractor Name
            string tractorName = this.Tractor.DisplayName;
            e.Graphics.DrawString(tractorName, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            /// Draw Year and Type
            f = new Font(this._font.FontFamily, 10, FontStyle.Regular);
            curRect = new RectangleF(curRect.Left, curRect.Top + curRect.Height, curRect.Width, f.GetHeight());

            //string tractorKind = this.Tractor.Katalogteil;
            string tractorYear = String.Format(ResourceReader.GetString("prnKatalogjahr"), this.Tractor.LetzteAktualisierung);
            e.Graphics.DrawString(tractorYear, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());
            string tractorTyp = this.Tractor.Antriebsart;
            e.Graphics.DrawString(tractorTyp, f, this._fontSB, curRect, PrintingHelper.GetRightAligment());

            //Draw tractor kind
            curRect = new RectangleF(curRect.Left + 300, curRect.Top, curRect.Width - 300, f.GetHeight());
            string tractorKind = this.Tractor.Katalogteil;
            e.Graphics.DrawString(tractorKind, f, this._fontSB, curRect, PrintingHelper.GetLeftAligment());

            curRect = new RectangleF(curRect.Left - 300, curRect.Top, curRect.Width + 300, f.GetHeight());
            e.Graphics.DrawLine(Pens.Black, curRect.X, curRect.Y + curRect.Height, curRect.Right, curRect.Y + curRect.Height);

            this._topPosition = curRect.Y + curRect.Height;
        }
        #endregion

        internal void DrawHeader(PrintPageEventArgs e)
        {
            Rectangle insideDoc = e.MarginBounds;

            Font f = new Font(this._font.FontFamily, 18, FontStyle.Bold);
            float labelHeight = f.GetHeight();
            /// Superkatalog ab 1998
            e.Graphics.DrawString(ResourceReader.GetString("prnDocumentHeader"), f, this._fontSB,
                                  new RectangleF(insideDoc.Left, insideDoc.Top, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetTopLeftAligment());

            /// drawLine
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), insideDoc.Left, insideDoc.Top + labelHeight, insideDoc.Right, insideDoc.Top + labelHeight);
            this._topPosition = insideDoc.Top + labelHeight;
        }

        internal void DrawFooter(PrintPageEventArgs e)
        {
            Rectangle insideDoc = e.MarginBounds;

            Font f = new Font(this._font.FontFamily, 10, FontStyle.Bold | FontStyle.Italic);
            float labelHeight = f.GetHeight();

            /// Company address
            e.Graphics.DrawString(ResourceReader.GetString("prnDocumentFooterCompanyAds"), f, this._fontSB,
                                  new RectangleF(insideDoc.Left, insideDoc.Bottom, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetBottomLeftAligment());

            /// Page number
            string page = String.Format(ResourceReader.GetString("prnSeiteFormat"), this._currentPage);
            e.Graphics.DrawString(this._currentPage.ToString(), f, this._fontSB,
                                  new RectangleF(insideDoc.Left, insideDoc.Bottom, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetBottomRightAligment());

            /// drawLine
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), insideDoc.Left, insideDoc.Bottom - 3, insideDoc.Right, insideDoc.Bottom - 3);
        }

        public virtual void ShowPreview()
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = this;

            dlg.ShowDialog();
        }

        public virtual void ShowPrint()
        {
            try
            {
                PrintDialog dlg = new PrintDialog();
                dlg.Document = this;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Print();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "ShowPrint", "Error while printing docuement: " + ex.ToString());
            }
        }
        #endregion
        
        #region Events

        #endregion
    }
}
