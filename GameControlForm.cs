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

        Game myGame;
        public GameControlForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myGame = ChessFabrik.TwoPlayersGame(pictureBox1.Width, pictureBox1.Height); //создаем новую игру
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (myGame != null)
            {
                myGame.Draw(e.Graphics); // рисуем все поле
                label1.Text = myGame.getSomeStuff(); // получаем некторую инфу об игре
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (myGame != null)
            {
                myGame.ResizeField(pictureBox1.Width, pictureBox1.Height); // изменяем размер поля
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (myGame != null)
            {
                myGame.Click(e.X, e.Y); //клик по полю
                pictureBox1.Refresh();
            }
        }
    }
}
