using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    abstract class Figure //Абстрактная фигурка
    {
        public bool white;
        protected GameData field;
        protected int row;
        protected int col;

        public Figure(bool isWhite, GameData field, int row, int col)
        {
            white = isWhite;
            this.field = field;
            this.row = row;
            this.col = col;
        }

        public abstract Bitmap GetSprite(); //дает спрайт фигурки
        public abstract List<Point> GetPosibleSteps(); //список клеток, в которые можно сделать ход
        public virtual void moveTo(int row, int col) //перемещение фигурки в новую точку, не контролируется фигуркой
        {
            this.row = row;
            this.col = col;
        }
    }
}
