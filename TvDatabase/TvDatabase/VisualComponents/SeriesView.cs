using System;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a graphic control with relevant information about an associated Series.
    /// </summary>
    public class SeriesView : Control
    {
        /// <summary>Gets the intended size of a SeriesView at the current moment.</summary>
        public static Size SeriesViewSize { get; internal set; }
        /// <summary>Gets the intended size of a SeriesView epViews image at the current moment.</summary>
        public static Size SeriesViewImageSize { get; internal set; }
        /// <summary>Gets the appropriately sized (at the current moment) image for an unknown epViews image.</summary>
        public static Image SeriesImageSized { get; internal set; }

        /// <summary>The background color pair of the SeriesView class.</summary>
        protected static readonly BinaryVariants<Color> Backgrounds;
        /// <summary>The title font-with-color pair of the SeriesView class.</summary>
        public static readonly BinaryVariants<FontWithColor> Titles;
        /// <summary>The last-watched font-with-color pair of the SeriesView class.</summary>
        public static readonly BinaryVariants<FontWithColor> LastWatcheds;
        /// <summary>The padding between the content of a SeriesView image and its borders.</summary>
        public const int SVPadding = 5;

        /// <summary>A value indicating whether the mouse is pressed on the current SeriesView</summary>
        protected bool isMouseOver;
        /// <summary>Gets or sets the epViews corresponding to the current SeriesView</summary>
        public Series Series { get; set; }
        /// <summary>Gets or sets the view mode in which this view will be drawn</summary>
        public Settings.EShowViewMode ViewMode { get; set; }
        /// <summary>Gets or sets the image to be used by this SeriesView. Must be set externally.</summary>
        public Image SeriesImage { get; set; }

        /// <summary>Initializes the static members of the SeriesView class.</summary>
        static SeriesView()
        {
            SeriesViewSize = new Size(300, 200);
            Backgrounds = Colors.Backgrounds;
            Titles = new BinaryVariants<FontWithColor>(new FontWithColor("Segoe UI", 1, true, false, false, Colors.Accents[false]),
                new FontWithColor("Segoe UI", 1, true, false, false, Colors.Accents[true]));
            FontWithColor lw = new FontWithColor("Segoe UI", 1, false, true, false, Colors.TextOrLines[true]);
            LastWatcheds = new BinaryVariants<FontWithColor>(lw, lw);
        }

        /// <summary>Constructs a new SeriesView object</summary>
        /// <param name="epViews">the Series to be associated with this SeriesView</param>
        /// <param name="viewMode">the initial show view mode</param>
        /// <param name="clickEH">the event handler to be called when the control is clicked</param>
        public SeriesView(Series series, Settings.EShowViewMode viewMode, EventHandler clickEH)
            : base()
        {
            this.Series = series;
            this.ViewMode = viewMode;
            this.Click += clickEH;
            this.Cursor = Cursors.Hand;
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
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.Clear(Backgrounds[false]);
            e.Graphics.FillRectangle(new SolidBrush(Backgrounds[this.isMouseOver]), 0, 0, this.Width - SVPadding, this.Height - SVPadding);

            switch (this.ViewMode)
            {
                case Settings.EShowViewMode.Weekdays:
                    // image
                    Rectangle imgR = new Rectangle(Point.Empty, SeriesViewImageSize);
                    e.Graphics.DrawImage(this.SeriesImage, Point.Empty);

                    // title
                    Rectangle titleR = new Rectangle(SVPadding, imgR.Bottom + SVPadding, this.Width - 2 * SVPadding, this.Height - imgR.Bottom - 2 * SVPadding);
                    e.Graphics.DrawString(this.Series.Name, Titles[this.isMouseOver].GetFont(), Titles[isMouseOver].GetBrush(), titleR);

                    // rating
                    Rectangle starR = new Rectangle(titleR.Left + (titleR.Width - Paths.Star.Width * this.Series.Rating.Value) / 2 - SVPadding / 2,
                        this.Height / 2, Paths.Star.Width * this.Series.Rating.Value, Paths.Star.Height);
                    for (int iStar = 0; iStar < this.Series.Rating.Value; iStar++)
                        e.Graphics.DrawImage(Paths.Star, new Point(starR.Left + iStar * Paths.Star.Width, starR.Top));

                    // last watched
                    Episode lastEp = this.Series.LastWatchedEpisode();
                    string text = lastEp == null
                        ? "unknown last episode"
                        : string.Format("episode {0}\non {1}",
                            lastEp.FormatEpisode("%s%E"),
                            Utils.FormatDateTime((DateTime) lastEp.LastWatched, Utils.StandardDateFormat));
                    Rectangle lastWatchedR = new Rectangle(titleR.Left, titleR.Bottom - this.Height / 4, titleR.Width, this.Height / 4);
                    e.Graphics.DrawString(text, LastWatcheds[this.isMouseOver].GetFont(), LastWatcheds[this.isMouseOver].GetBrush(), lastWatchedR);

                    break;

                case Settings.EShowViewMode.List:
                    // image
                    imgR = new Rectangle(Point.Empty, SeriesViewImageSize);
                    e.Graphics.DrawImage(this.SeriesImage, Point.Empty);

                    // title
                    titleR = new Rectangle(imgR.Right + SVPadding, 0, this.Width - imgR.Right - SVPadding, this.Height - SVPadding);
                    e.Graphics.DrawString(this.Series.Name, Titles[this.isMouseOver].GetFont(), Titles[isMouseOver].GetBrush(), titleR);

                    // rating
                    starR = new Rectangle(titleR.Left + (titleR.Width - Paths.Star.Width * this.Series.Rating.Value) / 2 - SVPadding / 2,
                       this.Height / 2, Paths.Star.Width * this.Series.Rating.Value, Paths.Star.Height);
                    for (int iStar = 0; iStar < this.Series.Rating.Value; iStar++)
                        e.Graphics.DrawImage(Paths.Star, new Point(starR.Left + iStar * Paths.Star.Width, starR.Top));

                    // last watched
                    lastEp = this.Series.LastWatchedEpisode();
                    text = lastEp == null
                       ? "unknown last episode"
                       : string.Format("episode {0}\non {1}",
                           lastEp.FormatEpisode("%s%E"),
                           Utils.FormatDateTime((DateTime) lastEp.LastWatched, Utils.StandardDateFormat));
                    lastWatchedR = new Rectangle(titleR.Left, titleR.Bottom - this.Height / 4, titleR.Width, this.Height / 4);
                    e.Graphics.DrawString(text, LastWatcheds[this.isMouseOver].GetFont(), LastWatcheds[this.isMouseOver].GetBrush(), lastWatchedR);

                    break;
            }

            base.OnPaint(e);
        }
    }
}
