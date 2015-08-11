using System;
using System.Windows.Forms;

namespace SharpPixel
{
    public partial class MainForm : Form
    {
        private Controller controller = new Controller();
        private double dt;

        private void MainForm_Load(object sender, EventArgs e)
        {
            ClientSize = Utility.WindowSize;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Utility.KeyDown[e.KeyCode] = true;
            controller.OnKeyDown(e);            
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            controller.Update(dt);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }        

        public MainForm()
        {
            InitializeComponent();
            controller.SetRenderSurface(this.renderSurface);
            dt = mainTimer.Interval / 1000d;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Utility.KeyDown[e.KeyCode] = false;
        }                              
    }   
}
