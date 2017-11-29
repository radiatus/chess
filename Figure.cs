using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
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

    class FigurePawn : Figure //пешка
    {
        private bool firstStep;

        public FigurePawn(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {
            firstStep = true;
        }

        public override Bitmap GetSprite()
        {
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whitePawn.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackPawn.png"));
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            int stepForward = white ? -1 : 1;
            if (row + stepForward >= 0 && row + stepForward < 8)
            {
                if (field[row + 2 * stepForward, col] == null && firstStep) //первый раз вперед
                    result.Add(new Point(col, row + 2 * stepForward));
                if (field[row + stepForward, col] == null) //вперед
                    result.Add(new Point(col, row + stepForward));
                if (col - 1 >= 0 && field[row + stepForward, col - 1] != null && field[row + stepForward, col - 1].white != white)//вперед и влево
                    result.Add(new Point(col - 1, row + stepForward));
                if (col + 1 < 8 && field[row + stepForward, col + 1] != null && field[row + stepForward, col + 1].white != white)//вперед и вправо
                    result.Add(new Point(col + 1, row + stepForward));
                // первое условие - не выхождение за границу по столбцу, второе - наличие фигуры, третье - то что фигура противника
            }

            return result;
        }
        public override void moveTo(int row, int col)
        {
            base.moveTo(row, col);
            firstStep = false;
        }
    }
    
    class FigureRook : Figure //Ладья
    {
        public FigureRook(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            return null;
        }

        public override List<Point> GetPosibleSteps()
        {
            return null;
        }
    }


    class FigureQueen : Figure //Ферзь
    {
        public FigureQueen(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            return null;
        }

        public override List<Point> GetPosibleSteps()
        {
            return null;
        }
    }

    class FigureKnight : Figure //Конь
    {
        public FigureKnight(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            return null;
        }

        public override List<Point> GetPosibleSteps()
        {
            return null;
        }
    }

    class FigureKing : Figure //Король
    {
        public FigureKing(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            return null;
        }

        public override List<Point> GetPosibleSteps()
        {
            return null;
        }
    }

    class FigureBishop : Figure //Слон
    {
        public FigureBishop(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            return null;
        }

        public override List<Point> GetPosibleSteps()
        {
            return null;
        }
    }
    


}
