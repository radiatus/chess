﻿using System;
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
        GameHistory gameHistory;
        public GameControlForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myGame = ChessFabrik.TwoPlayersGame(pictureBox1.Width, pictureBox1.Height); //создаем новую игру
            gameHistory = new GameHistory();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (myGame != null)
            {
                myGame.Draw(e.Graphics); // рисуем все поле
                label1.Text = myGame.getSomeStuff(); // получаем некторую инфу об игре
                label2.Text = myGame.getCountDead(true);
                label3.Text = myGame.getCountDead(false);
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
                gameHistory.History.Push(myGame.SaveGame());
                pictureBox1.Refresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (myGame != null)
            {
                myGame.UndoStep();
                pictureBox1.Refresh();
            }
        }
    }
}
