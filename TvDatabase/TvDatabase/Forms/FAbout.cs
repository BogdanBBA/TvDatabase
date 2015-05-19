using System;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FAbout : Form
    {
        private FMain mainForm;

        public FAbout(FMain mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void FAbout_Load(object sender, EventArgs e)
        {
            new TvDatabase.VisualComponents.FancyButtonCollection(menuP, new string[] { "Close" }, MenuButton_Click, true, 0);
        }

        /*protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }*/

        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowOnlyFormAndHideAllOthers(null);
        }
    }
}
