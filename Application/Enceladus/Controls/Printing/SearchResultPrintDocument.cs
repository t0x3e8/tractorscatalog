using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using Enceladus.Api;
using System.Drawing;
using Enceladus.StringLibrary;
using System.Windows.Forms;

namespace Enceladus.Controls
{
    class SearchResultPrintDocument : PrintDocument
    {
        #region Fields
        private IList<TractorSearchResult> tractors;
        private int currentPage;
        private Font font;
        private Color foreColor = Color.Black;
        private float topPosition = 0;
        private List<RectangleF> columnPositions;
        private bool isOddRow = false;

        public static readonly int TractorsOnPage = 50;
        #endregion

        #region Properties
        public IList<TractorSearchResult> Tractors
        {
            get { return this.tractors; }
            set
            {
                this.tractors = value;
                this.CountPagesNumber();
            }
        }
        #endregion

        #region Constructors
        public SearchResultPrintDocument()
            : base()
        {
            this.font = PrintingHelper.BuildDefaultFont();
            this.InitializeCellSizes();
        }
        #endregion

        #region Methods
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            try
            {
                this.topPosition = 0;

                /// Draw Header and List Header names on each page
                this.DrawHeader(e);
                this.DrawListHeaders(e);

                /// Count the first page and last. If last is bigger then total number of tractors then last is equal the total number.
                /// If last is less then total number of tractors then it means that there exists more pages.
                int first = (this.currentPage - 1) * TractorsOnPage;
                int last = first + TractorsOnPage;
                if (last >= this.tractors.Count)
                    last = this.tractors.Count;
                else
                    e.HasMorePages = true;

                /// Print single record
                for (; first < last; first++)
                {
                    this.DrawSingleRecord(e, this.tractors[first]);
                }

                /// Draw footer on each page
                this.DrawFooter(e);
                this.currentPage++;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "SearchResultPrintDocument.OnPrintPage", "An exception happened while printing tractor overview: " + ex.ToString());
            }
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            base.OnQueryPageSettings(e);

            e.PageSettings.Landscape = true;
            e.PageSettings.Margins = new Margins(40, 40, 35, 35);
        }

        private void DrawSingleRecord(PrintPageEventArgs e, TractorSearchResult tractor)
        {
            /// drawLine
            RectangleF insideDoc = new RectangleF(e.MarginBounds.Left, this.topPosition, e.MarginBounds.Width, this.columnPositions[0].Height);

            // every alternative row color
            if (this.isOddRow)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240, 240)), insideDoc);
            isOddRow = !isOddRow;
            
            e.Graphics.DrawLine(new Pen(Brushes.Black, 1), insideDoc.Left, insideDoc.Bottom, insideDoc.Right, insideDoc.Bottom);


            Font f = new Font(this.font.FontFamily, 7, FontStyle.Regular);
            for (int i = 0; i < this.columnPositions.Count; i++)
            {
                this.columnPositions[i] = new RectangleF(this.columnPositions[i].Left,
                                                          this.topPosition,
                                                          this.columnPositions[i].Width,
                                                          f.GetHeight() + 3);
            }

            e.Graphics.DrawString(tractor.Schlepperhersteller, f, new SolidBrush(this.foreColor), this.columnPositions[0], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Schleppertyp, f, new SolidBrush(this.foreColor), this.columnPositions[1], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.LetzteAktualisierung, f, new SolidBrush(this.foreColor), this.columnPositions[2], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.NennleistungkW, f, new SolidBrush(this.foreColor), this.columnPositions[3], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.NennleistungPS, f, new SolidBrush(this.foreColor), this.columnPositions[4], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Gesamtgewicht, f, new SolidBrush(this.foreColor), this.columnPositions[5], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Nutzlast, f, new SolidBrush(this.foreColor), this.columnPositions[6], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Wendekreis, f, new SolidBrush(this.foreColor), this.columnPositions[7], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Hoehe, f, new SolidBrush(this.foreColor), this.columnPositions[8], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.LS_Getriebe, f, new SolidBrush(this.foreColor), this.columnPositions[9], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.Kriechgetriebe, f, new SolidBrush(this.foreColor), this.columnPositions[10], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.FronthubwerkundZW, f, new SolidBrush(this.foreColor), this.columnPositions[11], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.HubkraftmaximaldaN, f, new SolidBrush(this.foreColor), this.columnPositions[12], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(tractor.PreisvonEuro, f, new SolidBrush(this.foreColor), this.columnPositions[13], PrintingHelper.GetBottomRightAligment());
            
            this.topPosition = insideDoc.Height + insideDoc.Top;
        }

        internal void DrawListHeaders(PrintPageEventArgs e)
        {
            Font f = new Font(this.font.FontFamily, 8, FontStyle.Regular);
            for (int i = 0; i < this.columnPositions.Count; i++)
            {
                this.columnPositions[i] = new RectangleF(this.columnPositions[i].Left,
                                                          this.topPosition,
                                                          this.columnPositions[i].Width,
                                                          f.GetHeight() + 10);
            }

            /// Header list text
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colSchlepperhersteller"), f, new SolidBrush(this.foreColor), this.columnPositions[0], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colSchleppertyp"), f, new SolidBrush(this.foreColor), this.columnPositions[1], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colLetzteAktualisierung"), f, new SolidBrush(this.foreColor), this.columnPositions[2], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colNennleistungKW"), f, new SolidBrush(this.foreColor), this.columnPositions[3], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colNennleistungPS"), f, new SolidBrush(this.foreColor), this.columnPositions[4], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colGesamtgewicht"), f, new SolidBrush(this.foreColor), this.columnPositions[5], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colLeergewicht"), f, new SolidBrush(this.foreColor), this.columnPositions[6], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colWendekreis"), f, new SolidBrush(this.foreColor), this.columnPositions[7], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colHoehe"), f, new SolidBrush(this.foreColor), this.columnPositions[8], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colLsGetriebe"), f, new SolidBrush(this.foreColor), this.columnPositions[9], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colKriechgetriebe"), f, new SolidBrush(this.foreColor), this.columnPositions[10], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colFronthubwerkUndZW"), f, new SolidBrush(this.foreColor), this.columnPositions[11], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colHubkraftMaximalDan"), f, new SolidBrush(this.foreColor), this.columnPositions[12], PrintingHelper.GetBottomLeftAligment());
            e.Graphics.DrawString(ResourceReader.GetString("DataGrid_colPreisVonEuro"), f, new SolidBrush(this.foreColor), this.columnPositions[13], PrintingHelper.GetBottomRightAligment());

            /// drawLine
            RectangleF insideDoc = new RectangleF(e.MarginBounds.Left, this.topPosition, e.MarginBounds.Width, this.columnPositions[0].Height);
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), insideDoc.Left, insideDoc.Bottom, insideDoc.Right, insideDoc.Bottom);

            this.topPosition = insideDoc.Height + insideDoc.Top;
        }

        internal void DrawHeader(PrintPageEventArgs e)
        {
            Rectangle insideDoc = e.MarginBounds;

            Font f = new Font(this.font.FontFamily, 12, FontStyle.Regular);
            float labelHeight = f.GetHeight();
            /// Superkatalog ab 1998
            e.Graphics.DrawString("Superkatalog ab 1988", f, new SolidBrush(this.foreColor),
                                  new RectangleF(insideDoc.Left, insideDoc.Top, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetTopLeftAligment());

            /// Suchergebnis
            e.Graphics.DrawString("Suchergebnis", f, new SolidBrush(this.foreColor),
                                  new RectangleF(insideDoc.Left, insideDoc.Top, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetTopRightAligment());

            /// drawLine
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), insideDoc.Left, insideDoc.Top + labelHeight, insideDoc.Right, insideDoc.Top + labelHeight);

            this.topPosition = insideDoc.Top + labelHeight;
        }

        internal void DrawFooter(PrintPageEventArgs e)
        {
            Rectangle insideDoc = e.MarginBounds;

            Font f = new Font(this.font.FontFamily, 10, FontStyle.Regular | FontStyle.Italic);
            float labelHeight = f.GetHeight();
            /// Page number
            e.Graphics.DrawString(this.currentPage.ToString(), f, new SolidBrush(this.foreColor),
                                  new RectangleF(insideDoc.Left, insideDoc.Bottom, insideDoc.Width, labelHeight),
                                  PrintingHelper.GetBottomRightAligment());

            /// drawLine
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), insideDoc.Left, insideDoc.Bottom, insideDoc.Right, insideDoc.Bottom);
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
            catch { }
        }

        private void InitializeCellSizes()
        {
            this.columnPositions = new List<RectangleF>(13);
            this.columnPositions.Add(new RectangleF(40, 0, 110, 0));                                                                   //Hersteller
            this.columnPositions.Add(new RectangleF(this.columnPositions[0].Left + this.columnPositions[0].Width, 0, 130, 0));       //typ
            this.columnPositions.Add(new RectangleF(this.columnPositions[1].Left + this.columnPositions[1].Width, 0, 30, 0));        //Year
            this.columnPositions.Add(new RectangleF(this.columnPositions[2].Left + this.columnPositions[2].Width, 0, 30, 0));        //kW
            this.columnPositions.Add(new RectangleF(this.columnPositions[3].Left + this.columnPositions[3].Width, 0, 30, 0));        //PS
            this.columnPositions.Add(new RectangleF(this.columnPositions[4].Left + this.columnPositions[4].Width, 0, 70, 0));        //Gesamt
            this.columnPositions.Add(new RectangleF(this.columnPositions[5].Left + this.columnPositions[5].Width, 0, 70, 0));        //Nutzlast
            this.columnPositions.Add(new RectangleF(this.columnPositions[6].Left + this.columnPositions[6].Width, 0, 85, 0));        //Hoeha
            this.columnPositions.Add(new RectangleF(this.columnPositions[7].Left + this.columnPositions[7].Width, 0, 70, 0));        //Stufenlose
            this.columnPositions.Add(new RectangleF(this.columnPositions[8].Left + this.columnPositions[8].Width, 0, 75, 0));        //Lastschaltung
            this.columnPositions.Add(new RectangleF(this.columnPositions[9].Left + this.columnPositions[9].Width, 0, 80, 0));        //Kriechgetriebe
            this.columnPositions.Add(new RectangleF(this.columnPositions[10].Left + this.columnPositions[10].Width, 0, 80, 0));      //Fronthubwerk
            this.columnPositions.Add(new RectangleF(this.columnPositions[11].Left + this.columnPositions[11].Width, 0, 105, 0));     //Heckhubraft
            this.columnPositions.Add(new RectangleF(this.columnPositions[12].Left + this.columnPositions[12].Width, 0, 55, 0));      //Preis        
        }

        private int CountPagesNumber()
        {
            if (this.tractors != null)
            {
                this.currentPage = 1;
                return (this.tractors.Count / TractorsOnPage) + 1;
            }
            else
                return 1;
        }
        #endregion

        #region Structures
        private class Cell
        {
            public int Key;
            public RectangleF Bounds;

            public Cell(int key, RectangleF bounds)
            {
                this.Key = key;
                this.Bounds = bounds;
            }
        }
        #endregion
    }
}
