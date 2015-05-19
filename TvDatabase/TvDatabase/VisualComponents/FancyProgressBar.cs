using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a fancy progress bar, customized with bling-bling. 
    /// </summary>
    public class FancyProgressBar : Control
    {
        /// <summary>The default background color to be used by new fancy bars that do not specify any</summary>
        public static readonly Color DefaultBackgroundColor = Colors.Backgrounds[false];
        /// <summary>The default pair of accent colors to be used by new fancy bars that do not specify any</summary>
        public static readonly BinaryVariants<Color> DefaultAccentColors = Colors.Accents;

        /// <summary>The background color of the current FancyProgressBar</summary>
        protected Color backgroundColor;
        /// <summary>The accent color pair of the current FancyProgressBar</summary>
        protected BinaryVariants<Color> accentColors;
        /// <summary>The accent brush pair of the current FancyProgressBar</summary>
        protected BinaryVariants<SolidBrush> accentBrushes;

        /// <summary>The percentage value of the current FancyProgressBar on the last percentage value change</summary>
        private double lastPercentage;
        /// <summary>The percentage value of the current FancyProgressBar</summary>
        private double percentage;
        /// <summary>Gets or sets the percentage value of the current FancyProgressBar</summary>
        public double Percentage
        {
            get { return this.percentage; }
            set
            {
                this.lastPercentage = this.percentage;
                this.percentage = value;
                this.Invalidate();
            }
        }

        /// <summary>Constructs a FancyProgressBar object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the bar</param>
        /// <param name="bounds">the bounds of the bar to be set withing the parent control</param>
        public FancyProgressBar(Control parent, Rectangle bounds)
            : this(parent, bounds, FancyProgressBar.DefaultBackgroundColor, FancyProgressBar.DefaultAccentColors)
        {
        }

        /// <summary>Constructs a FancyProgressBar object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the bar</param>
        /// <param name="bounds">the bounds of the bar to be set withing the parent control</param>
        /// <param name="backgroundColor">the background color of the bar</param>
        /// <param name="accentColors">the accent color pair of the bar</param>
        public FancyProgressBar(Control parent, Rectangle bounds, Color backgroundColor, BinaryVariants<Color> accentColors)
            : base(parent, null)
        {
            this.backgroundColor = backgroundColor;
            this.accentColors = accentColors;
            this.accentBrushes = new BinaryVariants<SolidBrush>(new SolidBrush(accentColors[true]), new SolidBrush(accentColors[false]));
            this.Bounds = bounds;
            this.Percentage = 0;
            this.lastPercentage = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.backgroundColor);
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, 0, 0, this.Width - 1, this.Height - 1);
            e.Graphics.FillRectangle(this.accentBrushes[this.percentage != 100], 2, 2, (int) ((this.Width - 4) * this.percentage / 100), this.Height - 4);
            base.OnPaint(e);
        }
    }
}
