using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    public class FancyTabView
    {
        public const int HeaderHeight = 64;

        protected FancyTabViewHeaderPictureBox headerPB;
        protected FancyCheckBoxCollection checkBoxes;
        protected FancyTabTab[] contentBindings;

        protected Rectangle headerBounds;
        protected Rectangle checkBoxesBounds;
        protected Rectangle contentBounds;

        public FancyTabView(Control parent, string[] captions, Control[] contentControls)
        {
            if (captions.Length != contentControls.Length)
                throw new ApplicationException("FancyTabView.FancyTabView() ERROR: captions.Length != contentControls.Length");

            this.headerBounds = new Rectangle(0, 0, parent.Width, HeaderHeight);
            this.checkBoxesBounds = new Rectangle(40, HeaderHeight / 2 - Sizes.TabViewCheckBox.Height / 2, parent.Width - 80, Sizes.TabViewCheckBox.Height);
            this.contentBounds = new Rectangle(0, this.headerBounds.Bottom + 1, parent.Width, parent.Height - this.headerBounds.Bottom);

            this.headerPB = new FancyTabViewHeaderPictureBox(parent, this.headerBounds);
            this.checkBoxes = new FancyCheckBoxCollection(parent, this.checkBoxesBounds, captions, this.CheckBox_Click, true);
            this.contentBindings = new FancyTabTab[checkBoxes.Count];
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                this.contentBindings[i] = new FancyTabTab(checkBoxes[i], contentControls[i]);
                contentControls[i].Parent = parent;
                contentControls[i].Bounds = this.contentBounds;
            }

            this.headerPB.SendToBack();
            this.CheckBox_Click(this.checkBoxes[0], null);
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            int senderIndex = this.checkBoxes.GetIndexOfFancyCheckBox(sender as FancyCheckBox);
            if (senderIndex != -1)
                for (int i = 0; i < this.contentBindings.Length; i++)
                {
                    this.contentBindings[i].Selected = i == senderIndex;
                    if (this.contentBindings[i].Selected)
                        this.contentBindings[i].Control.Update();
                }
        }
    }

    /// <summary>
    /// Binds a FancyCheckBox to a Control. To be used with FancyTabView.
    /// </summary>
    public class FancyTabTab
    {
        /// <summary>The checkbox of the binded pair.</summary>
        internal FancyCheckBox CheckBox;
        /// <summary>The control of the binded pair.</summary>
        internal Control Control;

        /// <summary>Creates a new binding between a FancyCheckBox and a Control.</summary>
        /// <param name="checkBox">the checkbox of the binded pair</param>
        /// <param name="control">the control of the binded pair</param>
        public FancyTabTab(FancyCheckBox checkBox, Control control)
        {
            this.CheckBox = checkBox;
            this.Control = control;
        }

        /// <summary>Gets or sets the checked value of the FancyCheckBox and the visibility of the binded control.</summary>
        public bool Selected
        {
            get { return this.CheckBox.Checked; }
            set
            {
                this.CheckBox.Checked = value;
                this.Control.Visible = value;
            }
        }
    }

    /// <summary>
    /// A header image for the FancyTabView.
    /// </summary>
    public class FancyTabViewHeaderPictureBox : Control
    {
        /// <summary>Creates a new FancyTabViewHeaderPictureBox in the specified parent and with the specified bounds.</summary>
        /// <param name="parent">the parent control</param>
        /// <param name="bounds">the bounds of the header</param>
        public FancyTabViewHeaderPictureBox(Control parent, Rectangle bounds)
            : base(parent, null)
        {
            this.Bounds = bounds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush bgBrush = new SolidBrush(Colors.Backgrounds[false]);
            Pen pen = new Pen(Colors.Accents[false], 1);
            e.Graphics.FillRectangle(bgBrush, 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
            e.Graphics.DrawLine(pen, 0, e.ClipRectangle.Bottom - 1, e.ClipRectangle.Width, e.ClipRectangle.Bottom - 1);
            base.OnPaint(e);
        }
    }
}
