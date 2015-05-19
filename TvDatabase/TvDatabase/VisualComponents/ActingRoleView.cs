using System;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a graphic control with relevant information about an associated ActingRole.
    /// </summary>
    public class ActingRoleView : Control
    {
        private ActingRole actingRole;
        private Image image;

        /// <summary>Constructs a new ActingRoleView object.</summary>
        /// <param name="clickEH">the click event handler to be called when the control is clicked</param>
        public ActingRoleView(EventHandler clickEH)
            : base()
        {
            this.actingRole = null;
            this.image = null;

            this.Cursor = Cursors.Hand;
            this.Click += clickEH;
        }

        /// <summary>Gets and sets (including graphical updates) the ActingRole associated with this control.</summary>
        public ActingRole ActingRole
        {
            get { return this.actingRole; }
            set
            {
                this.actingRole = value;
                this.image = Utils.GetScaledImage(Paths.ImagesFolder + this.actingRole.ImageFilename, this.Size,
                    Utils.ScaleImage(Paths.UnknownActingRoleImage, this.Size, false));
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.image == null)
                e.Graphics.Clear(Colors.Backgrounds[false]);
            else
                e.Graphics.DrawImage(this.image, Point.Empty);
        }
    }
}
