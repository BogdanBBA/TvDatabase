using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a fancy scrollbar, to be used with FancyPanel.
    /// </summary>
    public class FancyScrollBar : PictureBox
    {
        /// <summary>Defines the size (if visible) of the FancyScrollBar contained by a FancyPanel.</summary>
        public const int DefaultScrollBarWidth = 12;

        protected static readonly Brush BackgroundBrush = new SolidBrush(Colors.Backgrounds[true]);
        protected static readonly Brush BarBrush = new SolidBrush(Colors.Accents[false]);

        /// <summary>Defines positions for a FancyScrollBar withing a FancyPanel.</summary>
        public enum FancyScrollBarPosition { Bottom, Right };

        public int ScrollBarWidth { get; set; }
        private FancyScrollBarPosition scrollBarPosition;

        /// <summary>Creates a new FancyScrollBar object with the specified position.</summary>
        public FancyScrollBar(int scrollBarWidth)
            : base()
        {
            this.scrollBarPosition = FancyScrollBarPosition.Right;
            this.ScrollBarWidth = scrollBarWidth;
            this.RedrawFancyScrollBar(0, 1, 1);
        }

        /// <summary>Gets or sets the position attribute of the current fancy scrollbar.</summary>
        public FancyScrollBarPosition ScrollBarPosition
        {
            get { return this.scrollBarPosition; }
            set { this.scrollBarPosition = value; }
        }

        /// <summary>Redraws the current fancy scrollbar according to the specified attributes.</summary>
        /// <param name="position">the starting position of the content view (where it is scrolled)</param>
        /// <param name="viewLength">the width (or height) of the content view</param>
        /// <param name="totalLength">the total width (or height) of the content</param>
        public void RedrawFancyScrollBar(int position, int viewLength, int totalLength)
        {
            if (this.Image != null)
                this.Image.Dispose();
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(BackgroundBrush, new Rectangle(Point.Empty, this.Size));
            int dimSize = this.scrollBarPosition == FancyScrollBarPosition.Bottom ? this.Width : this.Height;
            int pos = (int) ((position / (double) totalLength) * dimSize), barLength = (int) ((viewLength / (double) totalLength) * dimSize);
            //pos = pos > totalSize ? totalSize : pos; dim = dim > totalSize ? totalSize : dim;
            Rectangle barRect = this.scrollBarPosition == FancyScrollBarPosition.Bottom
                ? new Rectangle(pos, 0, barLength, this.Height)
                : new Rectangle(0, pos, this.Width, barLength);
            g.FillRectangle(BarBrush, barRect);
            this.Image = bmp;
        }
    }

    /// <summary>
    /// Defines a fancy panel-like container with fancy scrolling, customized with bling-bling.
    /// </summary>
    public class FancyPanel
    {
        /// <summary>The amount in pixels that the panel should scroll.</summary>
        public int ScrollAmount { get; set; }

        private Panel outerPanel;
        private Panel innerPanel;
        private FancyScrollBar scrollBar;
        private List<Control> controls;
        private int position;

        /// <summary>Creates a new FancyPanel in the specified container and with the specified initial attributes.</summary>
        /// <param name="containerPanel">the Panel that will contain the FancyPanel</param>
        /// <param name="scrollBarPosition">the position of the scrollbar, if visible</param>
        /// <param name="scrollBarWidth">the width in pixels of the scrollbar</param>
        /// <param name="scrollBarVisible">whether the scrollbar will be visible or not</param>
        public FancyPanel(Panel containerPanel, FancyScrollBar.FancyScrollBarPosition scrollBarPosition, int scrollBarWidth, bool scrollBarVisible)
        {
            this.ScrollAmount = 100;
            this.outerPanel = new Panel();
            this.outerPanel.Parent = containerPanel;
            this.innerPanel = new Panel();
            this.innerPanel.Parent = this.outerPanel;
            this.scrollBar = new FancyScrollBar(scrollBarWidth);
            this.scrollBar.Parent = containerPanel;
            this.scrollBar.Visible = scrollBarVisible;

            this.innerPanel.AutoScroll = false;
            this.outerPanel.AutoScroll = false;

            this.controls = new List<Control>();
            this.position = 0;

            this.ScrollBarPosition = scrollBarPosition;
        }

        /// <summary>Gets or sets (including graphical updates) the position of the scrollbar</summary>
        public FancyScrollBar.FancyScrollBarPosition ScrollBarPosition
        {
            get { return this.scrollBar.ScrollBarPosition; }
            set
            {
                this.position = 0;
                this.innerPanel.Location = Point.Empty;
                this.scrollBar.ScrollBarPosition = value;
                switch (value)
                {
                    case FancyScrollBar.FancyScrollBarPosition.Bottom:
                        this.outerPanel.SetBounds(0, 0, this.outerPanel.Parent.Width, this.outerPanel.Parent.Height - (this.scrollBar.Visible ? this.scrollBar.ScrollBarWidth : 0));
                        this.scrollBar.SetBounds(0, this.outerPanel.Parent.Height - this.scrollBar.ScrollBarWidth, this.outerPanel.Parent.Width, this.scrollBar.ScrollBarWidth);
                        this.scrollBar.RedrawFancyScrollBar(0, this.outerPanel.Parent.Width, this.MaxControlLocation().X);
                        break;
                    case FancyScrollBar.FancyScrollBarPosition.Right:
                        this.outerPanel.SetBounds(0, 0, this.outerPanel.Parent.Width - (this.scrollBar.Visible ? this.scrollBar.ScrollBarWidth : 0), this.outerPanel.Parent.Height);
                        this.scrollBar.SetBounds(this.outerPanel.Parent.Width - this.scrollBar.ScrollBarWidth, 0, this.scrollBar.ScrollBarWidth, this.outerPanel.Parent.Height);
                        this.scrollBar.RedrawFancyScrollBar(0, this.outerPanel.Parent.Height, this.MaxControlLocation().Y);
                        break;
                }
                this.ResizeInnerPanelToControls();
            }
        }

        /// <summary>Gets the visible (outer) panel's size</summary>
        public Size VisiblePanelSize
        {
            get
            {
                switch (this.ScrollBarPosition)
                {
                    case FancyScrollBar.FancyScrollBarPosition.Bottom:
                        return new Size(this.outerPanel.Size.Width, this.outerPanel.Size.Height + this.scrollBar.ScrollBarWidth);
                    case FancyScrollBar.FancyScrollBarPosition.Right:
                        return new Size(this.outerPanel.Size.Width + this.scrollBar.ScrollBarWidth, this.outerPanel.Size.Height);
                    default:
                        return this.outerPanel.Size;
                }
            }
        }

        /// <summary>Gets the (inner) content panel's size</summary>
        public Size TotalPanelSize
        { get { return this.innerPanel.Size; } }

        /// <summary>Adds a control to this FancyPanel at the specified location, followed by an optional refresh</summary>
        /// <param name="control">the control to add to the panel</param>
        /// <param name="location">the location at which to add the control</param>
        /// <param name="refreshPanelAfterwards">whether to resize and refresh the panel and scrollbar after adding</param>
        public void AddControlToFancyPanel(Control control, Point location, bool refreshPanelAfterwards)
        {
            control.Parent = this.innerPanel;
            control.Location = location;
            this.controls.Add(control);
            if (refreshPanelAfterwards)
                this.ResizeInnerPanelToControls();
        }

        /// <summary>Searches the current FancyPanel's children for the given control</summary>
        /// <param name="control">the control to search for</param>
        /// <returns>the control object if found, null otherwise</returns>
        public Control GetControl(Control control)
        {
            foreach (Control iCtrl in this.controls)
                if (iCtrl.Equals(control))
                    return iCtrl;
            return null;
        }

        private Point MaxControlLocation()
        {
            Point maxLocation = new Point(1, 1);
            foreach (Control control in this.controls)
                if (control.Visible)
                {
                    maxLocation.X = Math.Max(maxLocation.X, control.Right);
                    maxLocation.Y = Math.Max(maxLocation.Y, control.Bottom);
                }
            return maxLocation;
        }

        /// <summary>Resizes the content panel to fit to the contents perfectly and refreshes the panel and scrollbar</summary>
        public void ResizeInnerPanelToControls()
        {
            this.innerPanel.Size = new Size(this.MaxControlLocation());
            this.RefreshFancyPanel();
        }
        /// <summary>Refreshes the panel and scrollbar (but DOES NOT resize inner panel to controls!).</summary>
        public void RefreshFancyPanel()
        {
            this.innerPanel.Location = this.scrollBar.ScrollBarPosition == FancyScrollBar.FancyScrollBarPosition.Bottom
                ? new Point(-this.position, 0)
                : new Point(0, -this.position);
            if (this.scrollBar.ScrollBarPosition == FancyScrollBar.FancyScrollBarPosition.Bottom)
                this.scrollBar.RedrawFancyScrollBar(this.position, this.outerPanel.Width, this.innerPanel.Width);
            else
                this.scrollBar.RedrawFancyScrollBar(this.position, this.outerPanel.Height, this.innerPanel.Height);
        }

        /// <summary>The mouse wheel scrool event handler to be set to the containing form, so that the panel will scroll with the mouse</summary>
        public void MouseWheelScroll_EvtHandler(object sender, MouseEventArgs e) // e.Delta > 0 means "scroll up"
        {
            BinaryVariants<Int32> sizes = this.ScrollBarPosition == FancyScrollBar.FancyScrollBarPosition.Bottom
                ? new BinaryVariants<int>(this.innerPanel.Width, this.outerPanel.Width)
                : new BinaryVariants<int>(this.innerPanel.Height, this.outerPanel.Height);
            int amount = Math.Sign(e.Delta) * -ScrollAmount;
            int pos = this.position + amount, limit = sizes[false] - sizes[true];
            this.position = sizes[false] > sizes[true] ? pos <= 0 ? 0 : (pos >= limit ? limit : pos) : 0;
            this.RefreshFancyPanel();
        }
    }
}
