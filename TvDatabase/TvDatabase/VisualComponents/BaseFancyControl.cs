using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a fancy control, customized with bling-bling. Abstract class.
    /// </summary>
    public abstract class BaseFancyControl : Control
    {
        /// <summary>The default pair of accent colors to be used by new fancy buttons that do not specify any</summary>
        public static readonly BinaryVariants<Color> DefaultAccentColors = Colors.Accents;
        /// <summary>The default pair of background colors (named any-focus because the True-value item will be 
        /// used if the mouse is either over or pressed) to be used by new fancy buttons that do not specify any</summary>
        public static readonly BinaryVariants<Color> DefaultAnyFocusBackColors = new BinaryVariants<Color>(Colors.Backgrounds[false], Colors.Backgrounds[false]);
        /// <summary>The default pair of mouse-over fonts to be used by new fancy buttons that do not specify any</summary>
        public static readonly BinaryVariants<FontWithColor> DefaultMouseOverFonts = new BinaryVariants<FontWithColor>(
            new FontWithColor("Segoe UI", 18, Colors.TextOrLines[false]), new FontWithColor("Segoe UI", 18, Colors.TextOrLines[true]));
        /// <summary>The default pair of mouse-pressed fonts to be used by new fancy buttons that do not specify any</summary>
        public static readonly BinaryVariants<FontWithColor> DefaultMousePressedFonts = new BinaryVariants<FontWithColor>(
            new FontWithColor("Segoe UI", 18, ColorTranslator.FromHtml("#fe0c8f")), new FontWithColor("Segoe UI", 18, ColorTranslator.FromHtml("#fe0c8f")));

        /// <summary>The click event handler of the current FancyButton</summary>
        protected EventHandler clickEH;
        /// <summary>The accent color pair of the current FancyButton</summary>
        protected BinaryVariants<Color> accentColors;
        /// <summary>The background color pair (named any-focus because the True-value item will be 
        /// used if the mouse is either over or pressed) of the current FancyButton</summary>
        protected BinaryVariants<Color> anyFocusBackColors;
        /// <summary>The mouse-over color pair of the current FancyButton</summary>
        protected BinaryVariants<FontWithColor> mouseOverFonts;
        /// <summary>The mouse-pressed color pair of the current FancyButton</summary>
        protected BinaryVariants<FontWithColor> mousePressedFonts;
        /// <summary>A value indicating whether the mouse is pressed on the current FancyButton</summary>
        protected bool isMouseOver;

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="bounds">the bounds of the button to be set withing the parent control</param>
        public BaseFancyControl(Control parent, string text, EventHandler clickEH, Rectangle bounds)
            : this(parent, text, clickEH)
        {
            this.Bounds = bounds;
        }

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        public BaseFancyControl(Control parent, string text, EventHandler clickEH)
            : this(parent, text, clickEH, BaseFancyControl.DefaultAccentColors, BaseFancyControl.DefaultAnyFocusBackColors,
                   BaseFancyControl.DefaultMouseOverFonts, BaseFancyControl.DefaultMousePressedFonts)
        {
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
        public BaseFancyControl(Control parent, string text, EventHandler clickEH, BinaryVariants<Color> accentColors, BinaryVariants<Color> anyFocusBackColors,
            BinaryVariants<FontWithColor> mouseOverFonts, BinaryVariants<FontWithColor> mousePressedFonts, Rectangle bounds)
            : this(parent, text, clickEH, accentColors, anyFocusBackColors, mouseOverFonts, mousePressedFonts)
        {
            this.Bounds = bounds;
        }

        /// <summary>Constructs a FancyButton object from the given parameters</summary>
        /// <param name="parent">the parent control (usually a Panel) of the button</param>
        /// <param name="text">the caption to be displayed on the button</param>
        /// <param name="clickEH">the click event handler to be called when the button is clicked</param>
        /// <param name="accentColors">the pair of accent colors to be used</param>
        /// <param name="anyFocusBackColors">the pair of background colors to be used</param>
        /// <param name="mouseOverFonts">the pair of mouse-over fonts to be used</param>
        /// <param name="mousePressedFonts">the pair of mouse-pressed fonts to be used</param>
        public BaseFancyControl(Control parent, string text, EventHandler clickEH, BinaryVariants<Color> accentColors, BinaryVariants<Color> anyFocusBackColors,
            BinaryVariants<FontWithColor> mouseOverFonts, BinaryVariants<FontWithColor> mousePressedFonts)
            : base(parent, text)
        {
            this.ClickEH = clickEH;
            this.accentColors = accentColors;
            this.anyFocusBackColors = anyFocusBackColors;
            this.mouseOverFonts = mouseOverFonts;
            this.mousePressedFonts = mousePressedFonts;
            this.isMouseOver = false;
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Gets or sets the click event handler for the current FancyButton object. 
        /// To remove the handler, pass the null value.
        /// </summary>
        public EventHandler ClickEH
        {
            get { return this.clickEH; }
            set
            {
                this.Click -= this.clickEH;
                if (value != null)
                {
                    this.clickEH = value;
                    this.Click += value;
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.isMouseOver = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.isMouseOver = false;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseUp(e);
        }
    }
}
