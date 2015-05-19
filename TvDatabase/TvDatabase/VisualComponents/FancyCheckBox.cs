using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a fancy checkbox control, customized with bling-bling.
    /// </summary>
    public class FancyCheckBox : BaseFancyControl
    {
        /// <summary>A value indicating whether the mouse is hovering over the current FancyCheckBox</summary>
        protected bool isMousePressed;
        /// <summary>A value indicating whether the mouse checkbox is checked</summary>
        protected bool isChecked;

        /// <summary>Constructs a FancyCheckBox object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="bounds">the bounds of the button to be set withing the parent control</param>
        public FancyCheckBox(Control parent, string text, EventHandler clickEH, Rectangle bounds)
            : base(parent, text, clickEH)
        {
            this.Bounds = bounds;
            this.isMousePressed = false;

            this.accentColors = new BinaryVariants<Color>(Colors.Backgrounds[true], Colors.Accents[true]);
        }

        /// <summary>Gets or sets (including graphical updates) the checked value of the current FancyCheckBox.</summary>
        public bool Checked
        {
            get { return this.isChecked; }
            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.isMousePressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.isMousePressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int accentPad = 2, accentSize = 6;
            Brush bgBrush = new SolidBrush(this.anyFocusBackColors[this.isMouseOver || this.isMousePressed]);
            Brush accentBrush = new SolidBrush(this.accentColors[this.isChecked]);
            Brush textBrush = new SolidBrush(this.isMousePressed ? this.mousePressedFonts[true].FontColor : this.mouseOverFonts[this.isMouseOver].FontColor);
            Font font = this.isMousePressed ? this.mousePressedFonts[true].GetFont() : this.mouseOverFonts[this.isMouseOver].GetFont();

            e.Graphics.FillRectangle(bgBrush, 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
            e.Graphics.FillRectangle(accentBrush, accentPad, this.Height - accentSize - accentPad, this.Width - 2 * accentPad, accentSize);

            Size size = e.Graphics.MeasureString(this.Text, font).ToSize();
            e.Graphics.DrawString(this.Text, font, textBrush, new Point(e.ClipRectangle.Width / 2 - size.Width / 2, e.ClipRectangle.Height / 2 - size.Height / 2));

            base.OnPaint(e);
        }
    }
}
