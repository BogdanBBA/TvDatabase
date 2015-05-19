using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TvDatabase.VisualComponents
{
    public class BorderPictureBox : PictureBox
    {
        public const int DefaultBorderWidth = 4;
        private static readonly Brush BgBr = new SolidBrush(Colors.Backgrounds[false]);
        private static readonly Brush BorderBr = new SolidBrush(ColorTranslator.FromHtml("#202020"));

        public int BorderWidth { get; private set; }

        public BorderPictureBox(Form form)
            : this(form, DefaultBorderWidth)
        {
        }

        public BorderPictureBox(Form form, int borderWidth)
            : base()
        {
            this.BorderWidth = borderWidth;
            this.Parent = form;
            this.StretchOut_SendToBack_Redraw();
        }

        public void StretchOut_SendToBack_Redraw()
        {
            this.SetBounds(0, 0, this.Parent.Width, this.Parent.Height);
            this.SendToBack();
            this.RedrawBorder();
        }

        public void RedrawBorder()
        {
            if (this.Image != null)
                this.Image.Dispose();

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(BorderBr, new Rectangle(0, 0, this.Width, this.Height));
            g.FillRectangle(BgBr, new Rectangle(BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2));
            this.Image = bmp;
        }
    }
}
