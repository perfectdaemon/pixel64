using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    public partial class MainForm : Form
    {
        private GameMenu menu;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ClientSize = Utility.WindowSize;
            menu = new GameMenu(this.renderSurface);
            menu.LoadResources();
            menu.RenderSelf();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            menu.HandleKeys(e);
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
         
    }   
}
