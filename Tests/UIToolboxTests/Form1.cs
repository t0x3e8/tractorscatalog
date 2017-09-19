using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;

namespace UIToolboxTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.dualScroller1.ValueChanged += new EventHandler(dualScroller1_ValueChanged);

            this.tabsBar1.ClearTabs();

            Tab tab3 = new Tab();
            tab3.Content = "General";
            this.tabsBar1.AddTab(tab3);

            Tab tab4 = new Tab();
            tab4.Content = "Advance";
            this.tabsBar1.AddTab(tab4);

            Tab tab5 = new Tab();
            tab5.Content = "Advance";
            this.tabsBar1.AddTab(tab5);
            this.tabsBar1.SelectedIndex = 2;
        }

        void dualScroller1_ValueChanged(object sender, EventArgs e)
        {
            this.dualScroller1.DisplayLeftValue = new DualScrollerDisplayValue(this.dualScroller1.ValueLeft.ToString(), ((int)(this.dualScroller1.ValueLeft / 3)).ToString());
            this.dualScroller1.DisplayRightValue = new DualScrollerDisplayValue(this.dualScroller1.ValueRight.ToString(), ((int)(this.dualScroller1.ValueRight / 3)).ToString()); 
        }

        private void gradientButton1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.disappearingLabel1.Hide(5);
        }
    }
}
