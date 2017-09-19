using Enceladus.UIToolbox;
namespace Enceladus
{
    partial class SearchResultPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlOptions = new Enceladus.UIToolbox.BorderedPanel();
            this.simpleButton4 = new Enceladus.UIToolbox.GradientIconButton();
            this.simpleButton3 = new Enceladus.UIToolbox.GradientIconButton();
            this.simpleButton2 = new Enceladus.UIToolbox.GradientIconButton();
            this.simpleButton1 = new Enceladus.UIToolbox.GradientIconButton();
            this.inputBoxLabel1 = new Enceladus.UIToolbox.InputBoxLabel();
            this.lbSelectedTractors = new System.Windows.Forms.ListBox();
            this.ShowAllLabelButton = new Enceladus.UIToolbox.InputBoxLabel();
            this.SearchResultLabel = new Enceladus.UIToolbox.InputBoxLabel();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOptions
            // 
            this.pnlOptions.AutoChildrenArrange = false;
            this.pnlOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlOptions.BorderWidth = 1F;
            this.pnlOptions.Caption = null;
            this.pnlOptions.Controls.Add(this.simpleButton4);
            this.pnlOptions.Controls.Add(this.simpleButton3);
            this.pnlOptions.Controls.Add(this.simpleButton2);
            this.pnlOptions.Controls.Add(this.simpleButton1);
            this.pnlOptions.Controls.Add(this.inputBoxLabel1);
            this.pnlOptions.Controls.Add(this.lbSelectedTractors);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOptions.InformResizer = null;
            this.pnlOptions.Location = new System.Drawing.Point(0, 406);
            this.pnlOptions.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlOptions.MaximalExpectedFontSize = 5;
            this.pnlOptions.MinimumSize = new System.Drawing.Size(566, 144);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.ShowBorder = false;
            this.pnlOptions.Size = new System.Drawing.Size(566, 144);
            this.pnlOptions.SupportResizing = true;
            this.pnlOptions.TabIndex = 4;
            // 
            // simpleButton4
            // 
            this.simpleButton4.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton4.Command = null;
            this.simpleButton4.Icon = global::Enceladus.Properties.Resources.tables_delete;
            this.simpleButton4.Location = new System.Drawing.Point(268, 107);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(190, 25);
            this.simpleButton4.TabIndex = 6;
            // 
            // simpleButton3
            // 
            this.simpleButton3.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton3.Command = null;
            this.simpleButton3.Icon = global::Enceladus.Properties.Resources.PrintList;
            this.simpleButton3.Location = new System.Drawing.Point(268, 79);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(190, 25);
            this.simpleButton3.TabIndex = 5;
            // 
            // simpleButton2
            // 
            this.simpleButton2.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton2.Command = null;
            this.simpleButton2.Icon = global::Enceladus.Properties.Resources.tables;
            this.simpleButton2.Location = new System.Drawing.Point(268, 51);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(190, 25);
            this.simpleButton2.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton1.Command = null;
            this.simpleButton1.Icon = global::Enceladus.Properties.Resources.Bookmark_Remove;
            this.simpleButton1.Location = new System.Drawing.Point(268, 23);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(190, 25);
            this.simpleButton1.TabIndex = 2;
            // 
            // inputBoxLabel1
            // 
            this.inputBoxLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.inputBoxLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.inputBoxLabel1.HorizontalAligment = System.Drawing.StringAlignment.Near;
            this.inputBoxLabel1.Label = "_markierte maschinen";
            this.inputBoxLabel1.Location = new System.Drawing.Point(6, 2);
            this.inputBoxLabel1.Name = "inputBoxLabel1";
            this.inputBoxLabel1.Pattern = null;
            this.inputBoxLabel1.Size = new System.Drawing.Size(150, 21);
            this.inputBoxLabel1.TabIndex = 1;
            // 
            // lbSelectedTractors
            // 
            this.lbSelectedTractors.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.lbSelectedTractors.FormattingEnabled = true;
            this.lbSelectedTractors.IntegralHeight = false;
            this.lbSelectedTractors.ItemHeight = 15;
            this.lbSelectedTractors.Location = new System.Drawing.Point(6, 23);
            this.lbSelectedTractors.Name = "lbSelectedTractors";
            this.lbSelectedTractors.Size = new System.Drawing.Size(256, 109);
            this.lbSelectedTractors.TabIndex = 0;
            // 
            // ShowAllLabelButton
            // 
            this.ShowAllLabelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowAllLabelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowAllLabelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ShowAllLabelButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ShowAllLabelButton.HorizontalAligment = System.Drawing.StringAlignment.Far;
            this.ShowAllLabelButton.Label = "_markierte maschinen";
            this.ShowAllLabelButton.Location = new System.Drawing.Point(232, 0);
            this.ShowAllLabelButton.Name = "ShowAllLabelButton";
            this.ShowAllLabelButton.Pattern = null;
            this.ShowAllLabelButton.Size = new System.Drawing.Size(328, 21);
            this.ShowAllLabelButton.TabIndex = 37;
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.SearchResultLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.SearchResultLabel.HorizontalAligment = System.Drawing.StringAlignment.Near;
            this.SearchResultLabel.Label = "";
            this.SearchResultLabel.Location = new System.Drawing.Point(0, 2);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.Pattern = null;
            this.SearchResultLabel.Size = new System.Drawing.Size(187, 21);
            this.SearchResultLabel.TabIndex = 35;
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            this.DataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGrid.EnableHeadersVisualStyles = false;
            this.DataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            this.DataGrid.Location = new System.Drawing.Point(0, 23);
            this.DataGrid.MultiSelect = false;
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ReadOnly = true;
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.ShowEditingIcon = false;
            this.DataGrid.Size = new System.Drawing.Size(560, 379);
            this.DataGrid.TabIndex = 36;
            this.DataGrid.VirtualMode = true;
            // 
            // SearchResultPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = null;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.ShowAllLabelButton);
            this.Controls.Add(this.SearchResultLabel);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.pnlOptions);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.MinimumSize = new System.Drawing.Size(566, 550);
            this.Name = "SearchResultPage";
            this.Size = new System.Drawing.Size(566, 550);
            this.pnlOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.BorderedPanel pnlOptions;
        internal UIToolbox.GradientIconButton simpleButton3;
        internal UIToolbox.GradientIconButton simpleButton2;
        internal UIToolbox.GradientIconButton simpleButton1;
        private UIToolbox.InputBoxLabel inputBoxLabel1;
        internal System.Windows.Forms.ListBox lbSelectedTractors;
        internal GradientIconButton simpleButton4;
        public InputBoxLabel ShowAllLabelButton;
        public InputBoxLabel SearchResultLabel;
        public System.Windows.Forms.DataGridView DataGrid;

    }
}
