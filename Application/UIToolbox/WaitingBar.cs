using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

namespace Enceladus.UIToolbox
{
    [ToolboxBitmap(typeof(WaitingBar), "WaitingBar")]
    public partial class WaitingBar : Control
    {
        #region Fields
        private int _startStick;
        private int _endStick;
        private int _stickThickness;
        private int _stickNumber;
        private int _dispStickNumber;
        private Color _firstColor;
        private Color _secondColor;
        private int _delayInSeconds;
        private bool _run;
        private PenStyles _penStyle;
        private SmoothingMode _smoothing;

        //private BackgroundWorker _bg;
        private System.Windows.Forms.Timer _timer;
        private int _startPos = 0;
        private int _curStick;
        private List<Pen> _pens;
        private List<float> _angles;
        private PointF _center;
        private LineCap _lineStyle;
        #endregion

        #region Properties
        [Description("Start or stop animation")]
        [Category("WaitingBar Apperance")]
        //[Browsable(false)]
        public bool Run
        {
            get { return this._run; }
            set
            {
                this._run = value;
                this.run();
            }
        }

        [Description("Get or set the position where the stick should be started drawing.")]
        [Category("WaitingBar Apperance")]
        public int StartStick
        {
            get { return this._startStick; }
            set
            {
                this._startStick = value;
                this.Invalidate();
            }
        }

        [Description("Get or set the position where the stick should be finished drawing.")]
        [Category("WaitingBar Apperance")]
        public int EndStick
        {
            get { return this._endStick; }
            set
            {
                this._endStick = value;
                this.Invalidate();
            }
        }

        [Description("Get or set the stick thickness.")]
        [Category("WaitingBar Apperance")]
        public int StickThickness
        {
            get { return this._stickThickness; }
            set
            {
                this._stickThickness = value;

                this.buildPenSet();
                this.Invalidate();
            }
        }

        [Description("Get or set the number of sticks in the circle.")]
        [Category("WaitingBar Apperance")]
        public int StickNumber
        {
            get { return this._stickNumber; }
            set
            {

                if (value < 1)
                    this._stickNumber = 1;
                else
                    this._stickNumber = value;

                try
                {
                    this.buildAnglesSet();
                    this.Invalidate();
                }
                catch{ }
            }
        }

        [Description("Get or set the number of displayed sticks.")]
        [Category("WaitingBar Apperance")]
        public int DisplayStickNumber
        {
            get { return this._dispStickNumber; }
            set
            {
                if (value < 1)
                    this._delayInSeconds = 1;
                else
                    this._dispStickNumber = value;

                try
                {
                    this.buildPenSet();
                    this.Invalidate();
                }
                catch { }
            }
        }

        [Description("Get or set delay between drawing sticks.")]
        [Category("WaitingBar Apperance")]
        public int DelayInSeconds
        {
            get { return this._delayInSeconds; }
            set
            {
                this._delayInSeconds = value;
                if (value > 0)
                    this._timer.Interval = value;
                this.Invalidate();
            }
        }

        [Description("First color which is necessery to draw the bar.")]
        [Category("WaitingBar Apperance")]
        public Color FirstColor
        {
            get { return this._firstColor; }
            set
            {
                this._firstColor = value;

                this.buildPenSet();
                this.Invalidate();
            }
        }

        [Description("Second color which is necessery to draw the bar. This color is not needed whith some Pen style.")]
        [Category("WaitingBar Apperance")]
        public Color SecondColor
        {
            get { return this._secondColor; }
            set
            {
                this._secondColor = value;

                this.buildPenSet();
                this.Invalidate();
            }
        }

        [Description("Get or set line style.")]
        [Category("WaitingBar Apperance")]
        public LineCap LineStyle
        {
            get { return this._lineStyle; }
            set
            {
                this._lineStyle = value;
                this.Invalidate();
            }
        }

        [Description("Get or set pen style.")]
        [Category("WaitingBar Apperance")]
        public PenStyles PenStyle
        {
            get { return this._penStyle; }
            set
            {
                this._penStyle = value;

                this.buildPenSet();
                this.Invalidate();
            }
        }

        [Description("Get or set sticks smoothing.")]
        [Category("WaitingBar Apperance")]
        public SmoothingMode Smoothing
        {
            get { return this._smoothing; }
            set
            {
                this._smoothing = value;

                this.Invalidate();
            }
        }
        #endregion

        #region Constructor
        public WaitingBar()
        {
            //InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this._bg = new BackgroundWorker();
            //this._bg.WorkerReportsProgress = true;
            //this._bg.WorkerSupportsCancellation = true;
            //this._bg.DoWork += new DoWorkEventHandler(_bg_DoWork);
            //this._bg.ProgressChanged += new ProgressChangedEventHandler(_bg_ProgressChanged);

            this._timer = new System.Windows.Forms.Timer();
            this._timer.Interval = 50;
            this._timer.Tick += new EventHandler(_timer_Tick);

            this._startStick = 90;
            this._endStick = 100;
            this._stickThickness = 20;
            this._stickNumber = 20;
            this._dispStickNumber = 20;
            this._delayInSeconds = 70;
            this._firstColor = Color.WhiteSmoke;
            this._secondColor = Color.Orange;
            this._run = false;
            this._lineStyle = LineCap.Round;
            this._penStyle = PenStyles.Gradient;


            this.buildAnglesSet();
            this.buildCenter();
            this.buildPenSet();
            this._curStick = 0;
        }

        #endregion

        #region Methods

        private void buildCenter()
        {
            this._center = new PointF(base.Width / 2, base.Height / 2);
        }

        private void buildPenSet()
        {
            switch (this._penStyle)
            {
                case PenStyles.Alpha:
                    this._pens = PenSetConstructor.AlphaPenSet(this._firstColor, this._dispStickNumber, this._stickThickness);
                    break;
                case PenStyles.Gradient:
                    this._pens = PenSetConstructor.GradiendPenSet(this._firstColor, this._secondColor, this._dispStickNumber, this._stickThickness);
                    break;
                case PenStyles.MixColors:
                    this._pens = PenSetConstructor.MixPenSet(this._firstColor, this._secondColor, this._dispStickNumber, this._stickThickness);
                    break;
            }
        }

        private void buildAnglesSet()
        {
            this._angles = new List<float>();

            float angle = 360f / (float)this._stickNumber;
            for (int i = 1; i <= this._stickNumber; i++)
            {
                this._angles.Add(i * angle);
            }
        }

        protected virtual void DrawStick(Graphics g, Pen pen, PointF p1, PointF p2)
        {
            try
            {
                pen.StartCap = this._lineStyle;
                pen.EndCap = this._lineStyle;

                g.DrawLine(pen, p1, p2);
            }
            catch { }
        }

        private PointF getPoint(int position, float angle)
        {
            double newAngle = Math.PI * angle / 180;

            PointF p = new PointF();
            p.X = this._center.X + position * (float)Math.Cos(newAngle);
            p.Y = this._center.Y + position * (float)Math.Sin(newAngle);

            return p;
        }

        //public void run()
        //{
        //    if (this._run)
        //    {
        //        if (!this._bg.IsBusy)
        //            this._bg.RunWorkerAsync();
        //    }
        //    else
        //        this._bg.CancelAsync();
        //}

        public void run()
        {
            if (this._run)
            {
                if (!this._timer.Enabled)
                    this._timer.Start();
            }
            else
                this._timer.Stop();
        }

        #endregion

        #region Events
        protected override void OnSizeChanged(EventArgs e)
        {
            this.buildCenter();
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                if (this._dispStickNumber > 0 && this._angles != null && this._pens != null)
                {
                    pe.Graphics.SmoothingMode = this._smoothing;
                    pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    for (int i = 0; i < this._dispStickNumber; i++)
                    {
                        Pen pen = this._pens[i];

                        float angle = this._angles[this._curStick];

                        PointF p1 = this.getPoint(this._startStick, angle);
                        PointF p2 = this.getPoint(this._endStick, angle);

                        this.DrawStick(pe.Graphics, pen, p1, p2);

                        this._curStick++;

                        if (this._curStick == this._stickNumber)
                            this._curStick = 0;
                    }
                }
            }
            catch { }

            base.OnPaint(pe);
        }

        private void _bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this._curStick = e.ProgressPercentage;
            this.Invalidate();
        }

        private void _bg_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;

            try
            {
                while (!(sender as BackgroundWorker).CancellationPending)
                {
                    i = (i + 1) % this._stickNumber;
                    (sender as BackgroundWorker).ReportProgress(i);

                    Thread.Sleep(this._delayInSeconds);
                }
            }
            catch { }
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            this.UpdateProgress();
        }

        private void RefreshControl()
        {
            this._curStick = this._startPos; ;
            this.Invalidate();
        }
        
        public void UpdateProgress()
        {
            this._startPos = (this._startPos + 1) % this._stickNumber;
            this.RefreshControl();
        }
        #endregion
    }
}
