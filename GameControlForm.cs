using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chess
{
    public partial class GameControlForm : Form
    {

        GameField myField;
        public GameControlForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myField = new GameField(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (myField != null)
                myField.Draw(e.Graphics);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (myField != null)
            {
                myField.Resize(pictureBox1.Width, pictureBox1.Height);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (myField != null)
            {
                Point p = myField.getCord(e.X, e.Y);
                myField.ActivateFigure(p.X, p.Y);

                pictureBox1.Refresh();
            }
        }
    }
}
