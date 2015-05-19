using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a fancy button control, customized with bling-bling.
    /// </summary>
    public class FancyButton : BaseFancyControl
    {
        /// <summary>A value indicating whether the the current FancyButton should have color accents</summary>
        protected bool drawAccent;
        /// <summary>A value indicating whether the mouse is hovering over the current FancyButton</summary>
        protected bool isMousePressed;

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        public FancyButton(Control parent, string text, EventHandler clickEH)
            : base(parent, text, clickEH)
        {
            this.isMousePressed = false;
            this.drawAccent = true;
        }

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="bounds">the bounds of the button to be set withing the parent control</param>
        public FancyButton(Control parent, string text, EventHandler clickEH, Rectangle bounds)
            : base(parent, text, clickEH, bounds)
        {
            this.isMousePressed = false;
            this.drawAccent = true;
        }

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="accentColors">the pair of accent colors to be used</param>
        /// <param name="anyFocusBackColors">the pair of background colors to be used</param>
        /// <param name="mouseOverFonts">the pair of mouse-over fonts to be used</param>
        /// <param name="mousePressedFonts">the pair of mouse-pressed fonts to be used</param>
        public FancyButton(Control parent, string text, EventHandler clickEH, BinaryVariants<Color> accentColors, BinaryVariants<Color> anyFocusBackColors,
            BinaryVariants<FontWithColor> mouseOverFonts, BinaryVariants<FontWithColor> mousePressedFonts, bool drawAccent)
            : base(parent, text, clickEH, accentColors, anyFocusBackColors, mouseOverFonts, mousePressedFonts)
        {
            this.isMousePressed = false;
            this.drawAccent = drawAccent;
        }

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="accentColors">the pair of accent colors to be used</param>
        /// <param name="anyFocusBackColors">the pair of background colors to be used</param>
        /// <param name="mouseOverFonts">the pair of mouse-over fonts to be used</param>
        /// <param name="mousePressedFonts">the pair of mouse-pressed fonts to be used</param>
        /// <param name="bounds">the bounds of the button to be set withing the parent control</param>
        public FancyButton(Control parent, string text, EventHandler clickEH, BinaryVariants<Color> accentColors, BinaryVariants<Color> anyFocusBackColors,
            BinaryVariants<FontWithColor> mouseOverFonts, BinaryVariants<FontWithColor> mousePressedFonts, bool drawAccent, Rectangle bounds)
            : base(parent, text, clickEH, accentColors, anyFocusBackColors, mouseOverFonts, mousePressedFonts, bounds)
        {
            this.isMousePressed = false;
            this.drawAccent = drawAccent;
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
            Brush accentBrush = new LinearGradientBrush(new Rectangle(accentPad, this.Height - accentSize - accentPad, this.Width - 2 * accentPad, accentSize),
                this.accentColors[this.isMouseOver || this.isMousePressed], this.anyFocusBackColors[false], 0f);
            Brush textBrush = new SolidBrush(this.isMousePressed ? this.mousePressedFonts[true].FontColor : this.mouseOverFonts[this.isMouseOver].FontColor);
            Font font = this.isMousePressed ? this.mousePressedFonts[true].GetFont() : this.mouseOverFonts[this.isMouseOver].GetFont();

            e.Graphics.FillRectangle(bgBrush, 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
            if (this.drawAccent)
                e.Graphics.FillRectangle(accentBrush, accentPad, this.Height - accentSize - accentPad, this.Width - 2 * accentPad, accentSize);

            Size size = e.Graphics.MeasureString(this.Text, font).ToSize();
            e.Graphics.DrawString(this.Text, font, textBrush, new Point(e.ClipRectangle.Width / 2 - size.Width / 2, e.ClipRectangle.Height / 2 - size.Height / 2));

            base.OnPaint(e);
        }
    }
}
