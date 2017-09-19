using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Enceladus.UIToolbox
{
    public delegate void TabSelectedEventHandler(object sender, SelectionChangedEventArgs e);

    public partial class TabsBar : UserControl
    {
        #region Fields and Propetries
        private Tab selectedTab = null;
        public Tab SelectedTab 
        {
            get { return this.selectedTab; }
        }

        [Browsable(true)]
        public int SelectedIndex
        {
            get { return this.Controls.IndexOf(this.selectedTab); }
            set
            {
                // don't do anything if there is no tabs
                if (this.Controls.Count == 0 && this.SelectedIndex == value)
                    return;

                // if the selected is out of range then select the first
                int active = value;
                if (value < 0 || value >= this.Controls.Count)
                    active = 0;

                Tab tab = this.Controls[active] as Tab;
                this.SelectTab(tab, this.selectedTab);
            }

        }

        public int TabWidth
        {
            set
            {
                foreach (Control control in this.Controls)
                {
                    (control as Tab).TabWidth = value;
                }
            }
        }

        public TabSize TabSize { get;set;}
        #endregion

        #region Constructors
        public TabsBar()
        {
            InitializeComponent();

            this.TabSize = UIToolbox.TabSize.Big;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.Controls.AddRange(new Control[2] { new Tab(TabType.Active), new Tab(TabType.Inactive) });
            this.BackColor = Color.Transparent;

            this.SelectedTabChanged += new TabSelectedEventHandler(TabsBar_SelectedTabChanged);
        }

        void TabsBar_SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ReCalculatePosition();
        }
        #endregion
        
        #region Methods
        public virtual void AddTab(Tab tab)
        {
            tab.TabSize = this.TabSize;
            tab.UpdateTab();
            this.Controls.Add(tab);
        }

        public virtual void ClearTabs()
        {
            this.Controls.Clear();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Tab tab = e.Control as Tab;
            tab.Click += new EventHandler(tab_Click);
            this.ReCalculatePosition();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {   
            base.OnControlRemoved(e);

            Tab tab = e.Control as Tab;
            tab.Click -= new EventHandler(tab_Click);
        }

        protected void tab_Click(object sender, EventArgs e)
        {
            this.SelectTab(sender as Tab, this.selectedTab); 
        }

        protected void SelectTab(Tab newSelectedTab, Tab currentlySelected)
        {
            this.SuspendLayout();
            if (newSelectedTab != currentlySelected)
            {
                if (newSelectedTab != null)
                {
                    newSelectedTab.TabType = TabType.Active;
                    newSelectedTab.UpdateTab();
                }

                if (currentlySelected != null)
                {
                    currentlySelected.TabType = TabType.Inactive;
                    currentlySelected.UpdateTab();
                }

                this.selectedTab = newSelectedTab;
                if (this.selectedTabChanged != null)
                    this.selectedTabChanged(this, new SelectionChangedEventArgs(currentlySelected, newSelectedTab));
            }
            this.ResumeLayout();
        }

        protected void ReCalculatePosition()
        {
            int nextControlXPosition = 0;

            foreach (Control control in this.Controls)
            {
                control.Location = new Point(nextControlXPosition + 1, this.Height - (control.Height + 3));
                nextControlXPosition += control.Width - 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int height = 31;
            using (Pen pen = new Pen(Color.FromArgb(211, 214, 211)))
            {
                e.Graphics.DrawLine(pen, new Point(0, height), new Point(this.Width, height));
            }

            if (this.DesignMode)
                e.Graphics.DrawRectangle(Pens.Violet, 0, 0, this.Width - 1, this.Height - 1);
        }
        #endregion

        #region Events
        private event TabSelectedEventHandler selectedTabChanged = null;
        public event TabSelectedEventHandler SelectedTabChanged
        {
            add { this.selectedTabChanged += value; }
            remove { this.selectedTabChanged -= value; }
        }
        #endregion
    }
}
