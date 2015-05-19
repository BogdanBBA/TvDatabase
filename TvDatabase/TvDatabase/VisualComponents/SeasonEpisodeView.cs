using System;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a control that has relevant information to a Series episode;
    /// </summary>
    public class SeasonEpisodeView : Control
    {
        /// <summary>Defines the view modes in which a SeasonEpisodeView can display information.</summary>
        public enum ViewModes { Season, Episode };

        /// <summary>The background color pair of the SeasonEpisodeView class.</summary>
        protected static readonly BinaryVariants<Color> Backgrounds;
        /// <summary>The season name font-with-color pair of the SeasonEpisodeView class.</summary>
        public static readonly BinaryVariants<FontWithColor> SeasonFwC;
        /// <summary>The episode number font-with-color pair of the SeasonEpisodeView class.</summary>
        public static readonly BinaryVariants<FontWithColor> EpisodeNumberFwC;
        /// <summary>The episode title font-with-color pair of the SeasonEpisodeView class.</summary>
        public static readonly BinaryVariants<FontWithColor> EpisodeTitleFwC;
        /// <summary>The episode title font-with-color pair of the SeasonEpisodeView class.</summary>
        public static readonly BinaryVariants<FontWithColor> BroadcastFwC;
        /// <summary>The episode title font-with-color pair of the SeasonEpisodeView class.</summary>
        public static readonly BinaryVariants<FontWithColor> WatchedFwC;

        /// <summary>A value indicating whether the mouse is pressed on the current SeasonEpisodeView</summary>
        protected bool isMouseOver;

        private string seasonName;
        /// <summary>Gets or sets (excluding graphical updates) the season name corresponding to the current SeasonEpisodeView</summary>
        public string SeasonName
        {
            get { return this.seasonName; }
            set
            {
                this.seasonName = value;
                //this.Invalidate();
            }
        }

        private Episode episode;
        /// <summary>Gets or sets (excluding graphical updates) the episode corresponding to the current SeasonEpisodeView</summary>
        public Episode Episode
        {
            get { return this.episode; }
            set
            {
                this.episode = value;
                //this.Invalidate();
            }
        }

        private ViewModes viewMode;
        /// <summary>Gets or sets (including graphical updates) the ViewMode of the current SeasonEpisodeView</summary>
        public ViewModes ViewMode
        {
            get { return this.viewMode; }
            set
            {
                this.viewMode = value;
                this.Cursor = this.viewMode == ViewModes.Episode ? Cursors.Hand : Cursors.Default;
                this.Invalidate();
            }
        }

        /// <summary>Initializes the static members of the SeasonEpisodeView class.</summary>
        static SeasonEpisodeView()
        {
            Backgrounds = Colors.Backgrounds;
            SeasonFwC = new BinaryVariants<FontWithColor>(
                new FontWithColor("Segoe UI", 16, true, false, false, Colors.Accents[false]),
                new FontWithColor("Segoe UI", 16, true, false, false, Colors.Accents[true]));
            EpisodeNumberFwC = new BinaryVariants<FontWithColor>(
                new FontWithColor("Segoe UI", 13, true, false, false, Colors.Accents[false]),
                new FontWithColor("Segoe UI", 13, true, false, false, Colors.Accents[true]));
            EpisodeTitleFwC = new BinaryVariants<FontWithColor>(
                new FontWithColor("Segoe UI", 12, false, true, false, Color.Wheat),
                new FontWithColor("Segoe UI", 12, false, true, false, Color.Wheat));
            BroadcastFwC = new BinaryVariants<FontWithColor>(
                new FontWithColor("Segoe UI", 12, false, false, false, Color.BlueViolet),
                new FontWithColor("Segoe UI", 12, false, false, false, Color.BlueViolet));
            WatchedFwC = new BinaryVariants<FontWithColor>(
                new FontWithColor("Segoe UI", 8, true, false, false, Colors.Accents[true]),
                new FontWithColor("Segoe UI", 8, true, false, false, Colors.Accents[true]));
        }

        /// <summary>Constructs a new SeasonEpisodeView object</summary>
        /// <param name="clickEH">the event handler to be called when the control is clicked</param>
        public SeasonEpisodeView(EventHandler clickEH)
            : base()
        {
            this.seasonName = "";
            this.episode = null;
            this.Click += clickEH;
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
            switch (this.viewMode)
            {
                case ViewModes.Season:

                    e.Graphics.Clear(Backgrounds[false]);
                    string text = " # Season " + this.seasonName;
                    Font font = SeasonFwC[false].GetFont();
                    Size size = e.Graphics.MeasureString(text, font).ToSize();
                    e.Graphics.DrawString(text, font, SeasonFwC[false].GetBrush(), new Point(0, this.Height / 2 - size.Height / 2));

                    break;

                case ViewModes.Episode:

                    e.Graphics.Clear(Backgrounds[this.isMouseOver]);

                    // episode number
                    int left = 0, width = this.Width / 8;
                    text = this.episode.FormatEpisode("%s%E");
                    font = EpisodeNumberFwC[this.isMouseOver].GetFont();
                    size = e.Graphics.MeasureString(text, font).ToSize();
                    e.Graphics.DrawString(text, font, EpisodeNumberFwC[this.isMouseOver].GetBrush(),
                        new Point(left + width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));

                    // episode title
                    left = this.Width / 8;
                    width = this.Width / 2;
                    text = this.episode.Name;
                    font = EpisodeTitleFwC[this.isMouseOver].GetFont();
                    size = e.Graphics.MeasureString(text, font).ToSize();
                    e.Graphics.DrawString(text, font, EpisodeTitleFwC[this.isMouseOver].GetBrush(),
                        new Point(left + width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));

                    // broadcast date
                    left = 5 * this.Width / 8;
                    width = this.Width / 4;
                    text = this.episode.FirstBroadcasted == null ? "unknown" : Utils.FormatDateTime((DateTime) this.episode.FirstBroadcasted, "d MMMM yyyy");
                    font = BroadcastFwC[this.isMouseOver].GetFont();
                    size = e.Graphics.MeasureString(text, font).ToSize();
                    e.Graphics.DrawString(text, font, BroadcastFwC[this.isMouseOver].GetBrush(),
                        new Point(left + width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));

                    // watched
                    left = 7 * this.Width / 8;
                    width = this.Width / 8;
                    text = this.episode.LastWatched != null ? "WATCHED" : "";
                    font = WatchedFwC[this.isMouseOver].GetFont();
                    size = e.Graphics.MeasureString(text, font).ToSize();
                    e.Graphics.DrawString(text, font, WatchedFwC[this.isMouseOver].GetBrush(),
                        new Point(left + width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));

                    break;
            }
        }
    }
}
