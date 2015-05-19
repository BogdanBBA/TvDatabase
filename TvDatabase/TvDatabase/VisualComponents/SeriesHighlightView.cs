using System;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a graphic control that highlights relevant information about an associated Series.
    /// </summary>
    public class SeriesHighlightView : Control
    {
        /// <summary>The background color pair of the SeriesHighlightView class.</summary>
        protected static readonly BinaryVariants<Color> Backgrounds;
        /// <summary>The title font-with-color pair of the SeriesHighlightView class.</summary>
        public static readonly BinaryVariants<FontWithColor> Titles;
        /// <summary>The padding between the content of a SeriesHighlightView image and its borders.</summary>
        public const int SHVPadding = 5;

        /// <summary>A value indicating whether the mouse is pressed on the current SeriesHighlightView</summary>
        protected bool isMouseOver;
        /// <summary>The epViews corresponding to the current SeriesHighlightView</summary>
        private Series series;
        /// <summary>Gets or sets the image to be used by this SeriesView. Must be set externally.</summary>
        public Image SeriesImage { get; set; }

        /// <summary>Initializes the static members of the SeriesHighlightView class.</summary>
        static SeriesHighlightView()
        {
            Backgrounds = Colors.Backgrounds;
            Titles = new BinaryVariants<FontWithColor>(new FontWithColor("Segoe UI", 30, true, false, false, Colors.Accents[false]),
                new FontWithColor("Segoe UI", 30, true, false, false, Colors.Accents[true]));
        }

        /// <summary>Creates a new SeriesHighlightView object with the specified attributes.</summary>
        /// <param name="parent">the parent control in which to place the SeriesHighlightView</param>
        /// <param name="bounds">the bounds of the SeriesHighlightView</param>
        /// <param name="clickEH">the event handler that will be called when the SeriesHighlightView is clicked</param>
        public SeriesHighlightView(Control parent, Rectangle bounds, EventHandler clickEH)
            : base(parent, null)
        {
            this.Bounds = bounds;
            this.series = null;
            this.Click += clickEH;
            this.Cursor = Cursors.Hand;
        }

        /// <summary>Gets or sets (including graphical updates) the epViews corresponding to the current SeriesHighlightView.</summary>
        public Series Series
        {
            get { return this.series; }
            set
            {
                this.series = value;
                this.Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.series == null)
            {
                e.Graphics.Clear(Backgrounds[this.isMouseOver]);
                return;
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.Clear(Backgrounds[false]);
            e.Graphics.DrawImage(this.SeriesImage, Point.Empty);

            // title
            Rectangle titleR = new Rectangle(this.Width / 2 + SHVPadding, SHVPadding, this.Width / 2 - SHVPadding, this.Height / 2);
            e.Graphics.DrawString(this.Series.Name, Titles[this.isMouseOver].GetFont(), Titles[isMouseOver].GetBrush(), titleR);

            base.OnPaint(e);
        }
    }
}
