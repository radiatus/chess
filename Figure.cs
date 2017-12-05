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
        public override void moveTo(int row, int col) //после первого хода
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
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteRook.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackRook.png"));
        }

        public List<Point> GetListRookSteps()
        {
            List<Point> listSteps = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                //вниз
                if (row + i < 8)
                {
                    if (field[row + i, col] == null)
                        listSteps.Add(new Point(col, row + i));
                    else
                    if (field[row + i, col] != null && field[row + i, col].white != white)
                    {
                        listSteps.Add(new Point(col, row + i));
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                //вверх
                if (row - i >= 0)
                {
                    if (field[row - i, col] == null)
                        listSteps.Add(new Point(col, row - i));
                    else
                    if (field[row - i, col] != null && field[row - i, col].white != white)
                    {
                        listSteps.Add(new Point(col, row - i));
                        break;
                    }

                }
            }
            for (int i = 0; i < 8; i++)
            {
                //вправо
                if (col + i < 8)
                {
                    if (field[row, col + i] == null)
                        listSteps.Add(new Point(col + i, row));
                    else
                    if (field[row, col + i] != null && field[row, col + i].white != white)
                    {
                        listSteps.Add(new Point(col + i, row));
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                //влево
                if (col - i >= 0)
                {
                    if (field[row, col - i] == null)
                        listSteps.Add(new Point(col - i, row));
                    else
                    if (field[row, col - i] != null && field[row, col - i].white != white)
                    {
                        listSteps.Add(new Point(col - i, row));
                        break;
                    }
                }
            }
            return listSteps;
        }


        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            result = GetListRookSteps();

            //for (int i = 0; i < 8; i++)
            //{
            //    //вниз
            //    if (row + i < 8)
            //    {
            //        if (field[row + i, col] == null)
            //            result.Add(new Point(col, row + i));
            //        else
            //        if (field[row + i, col] != null && field[row + i, col].white != white)
            //        {
            //            result.Add(new Point(col, row + i));
            //            break;
            //        }
            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //вверх
            //    if (row - i >= 0)
            //    {
            //        if (field[row - i, col] == null)
            //            result.Add(new Point(col, row - i));
            //        else
            //        if (field[row - i, col] != null && field[row - i, col].white != white)
            //        {
            //            result.Add(new Point(col, row - i));
            //            break;
            //        }

            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //вправо
            //    if (col + i < 8)
            //    {
            //        if (field[row, col + i] == null)
            //            result.Add(new Point(col + i, row));
            //        else
            //        if (field[row, col + i] != null && field[row, col + i].white != white)
            //        {
            //            result.Add(new Point(col + i, row));
            //            break;
            //        }
            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //влево
            //    if (col - i >= 0)
            //    {
            //        if (field[row, col - i] == null)
            //            result.Add(new Point(col - i, row));
            //        else
            //        if (field[row, col - i] != null && field[row, col - i].white != white)
            //        {
            //            result.Add(new Point(col - i, row));
            //            break;
            //        }
            //    }
            //}

            return result;
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
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteQueen.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackQueen.png"));
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();



            return result;
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
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteKnight.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackKnight.png"));
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            if (col + 2 < 8) // шаги вправо: вверх и вниз
            {
                if (field[row + 1, col + 2] == null)
                    result.Add(new Point(col + 2, row + 1));
                if (field[row - 1, col + 2] == null)
                    result.Add(new Point(col + 2, row - 1));

                if (field[row + 1, col + 2] != null && field[row + 1, col + 2].white != white)
                    result.Add(new Point(col + 2, row + 1));
                if (field[row - 1, col + 2] != null && field[row - 1, col + 2].white != white)
                    result.Add(new Point(col + 2, row - 1));
            }
            if (col - 2 >= 0) // шаги влево: вверх и вниз
            {
                if (field[row + 1, col - 2] == null)
                    result.Add(new Point(col - 2, row + 1));
                if (field[row - 1, col - 2] == null)
                    result.Add(new Point(col - 2, row - 1));

                if (field[row + 1, col - 2] != null && field[row + 1, col - 2].white != white)
                    result.Add(new Point(col - 2, row + 1));
                if (field[row - 1, col - 2] != null && field[row - 1, col - 2].white != white)
                    result.Add(new Point(col - 2, row - 1));
            }
            if (row + 2 < 8) // шаги вниз: вправо и влево
            {
                if (field[row + 2, col + 1] == null)
                    result.Add(new Point(col + 1, row + 2));
                if (field[row + 2, col - 1] == null)
                    result.Add(new Point(col - 1, row + 2));

                if (field[row + 2, col + 1] != null && field[row + 2, col + 1].white != white)
                    result.Add(new Point(col + 1, row + 2));
                if (field[row + 2, col - 1] != null && field[row + 2, col - 1].white != white)
                    result.Add(new Point(col - 1, row + 2));
            }
            if (row - 2 >= 0) // шаги вверх: вправо и влево
            {
                if (field[row - 2, col + 1] == null)
                    result.Add(new Point(col + 1, row - 2));
                if (field[row - 2, col - 1] == null)
                    result.Add(new Point(col - 1, row - 2));

                if (field[row - 2, col + 1] != null && field[row - 2, col + 1].white != white)
                    result.Add(new Point(col + 1, row - 2));
                if (field[row - 2, col - 1] != null && field[row - 2, col - 1].white != white)
                    result.Add(new Point(col - 1, row - 2));
            }
            return result;
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
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteKing.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackKing.png"));
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();
            //int[,] f = new int { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            // шаги вверх
            if (row + 1 < 8)
            {
                if (field[row + 1, col] == null)
                    result.Add(new Point(col, row + 1));
                if (col + 1 < 8 && field[row + 1, col + 1] == null)
                    result.Add(new Point(col + 1, row + 1));
                if (col - 1 >= 0 && field[row + 1, col - 1] == null)
                    result.Add(new Point(col - 1, row + 1));

                if (field[row + 1, col] != null && field[row + 1, col].white != white)
                    result.Add(new Point(col, row + 1));
                if (col + 1 < 8 && field[row + 1, col + 1] != null && field[row + 1, col + 1].white != white)
                    result.Add(new Point(col + 1, row + 1));
                if (col - 1 >= 0 && field[row + 1, col - 1] != null && field[row + 1, col - 1].white != white)
                    result.Add(new Point(col - 1, row + 1));
            }
            // шаг вправо
            if (col + 1 < 8)
            {
                if (field[row, col + 1] == null)
                    result.Add(new Point(col + 1, row));

                if (field[row, col + 1] != null && field[row, col + 1].white != white)
                    result.Add(new Point(col + 1, row));
            }
            // шаг влево
            if (col - 1 >= 0)
            {
                if (field[row, col - 1] == null)
                    result.Add(new Point(col - 1, row));

                if (field[row, col - 1] != null && field[row, col - 1].white != white)
                    result.Add(new Point(col - 1, row));
            }
            // шаги вниз
            if (row - 1 >= 0)
            {
                if (field[row - 1, col] == null)
                    result.Add(new Point(col, row - 1));
                if (col + 1 < 8 && field[row - 1, col + 1] == null)
                    result.Add(new Point(col + 1, row - 1));
                if (col - 1 >= 0 && field[row - 1, col - 1] == null)
                    result.Add(new Point(col - 1, row - 1));

                if (field[row - 1, col] != null && field[row - 1, col].white != white)
                    result.Add(new Point(col, row - 1));
                if (col + 1 < 8 && field[row - 1, col + 1] != null && field[row - 1, col + 1].white != white)
                    result.Add(new Point(col + 1, row - 1));
                if (col - 1 >= 0 && field[row - 1, col - 1] != null && field[row - 1, col - 1].white != white)
                    result.Add(new Point(col - 1, row - 1));
            }

            return result;
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
            if (white)
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteBishop.png"));
            else
                return new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackBishop.png"));
        }

        public List<Point> GetListBishopSteps()
        {
            List<Point> listSteps = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                //вверх вправо 
                if (row - i >= 0 && col + i < 8)
                {
                    if (field[row - i, col + i] == null)
                        listSteps.Add(new Point(col + i, row - i));
                    else if (field[row - i, col + i] != null && field[row - i, col + i].white != white)
                    {
                        listSteps.Add(new Point(col + i, row - i));
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                //вверх влево
                if (row - i >= 0 && col - i >= 0)
                {
                    if (field[row - i, col - i] == null)
                        listSteps.Add(new Point(col - i, row - i));
                    else if (field[row - i, col - i] != null && field[row - i, col - i].white != white)
                    {
                        listSteps.Add(new Point(col - i, row - i));
                        break;
                    }

                }
            }
            for (int i = 0; i < 8; i++)
            {
                //вниз влево
                if (row + i >= 0 && col - i >= 0)
                {
                    if (field[row + i, col - i] == null)
                        listSteps.Add(new Point(col - i, row + i));
                    else if (field[row + i, col - i] != null && field[row + i, col - i].white != white)
                    {
                        listSteps.Add(new Point(col - i, row + i));
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                //вниз вправо
                if (row + i >= 0 && col + i >= 0)
                {
                    if (field[row + i, col + i] == null)
                        listSteps.Add(new Point(col + i, row + i));
                    else if (field[row + i, col + i] != null && field[row + i, col + i].white != white)
                    {
                        listSteps.Add(new Point(col + i, row + i));
                        break;
                    }
                }
            }
            return listSteps;
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            result = GetListBishopSteps();

            //for (int i = 0; i < 8; i++)
            //{ 
            //    //вверх вправо 
            //    if (row - i >= 0 && col + i < 8)
            //    {
            //        if (field[row - i, col + i] == null)
            //            result.Add(new Point(col + i, row - i));
            //        else if (field[row - i, col + i] != null && field[row - i, col + i].white != white)
            //        {
            //            result.Add(new Point(col + i, row - i));
            //            break;
            //        }
            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //вверх влево
            //    if (row - i >= 0 && col - i >= 0)
            //    {
            //        if (field[row - i, col - i] == null)
            //            result.Add(new Point(col - i, row - i));
            //        else if (field[row - i, col - i] != null && field[row - i, col - i].white != white)
            //        {
            //            result.Add(new Point(col - i, row - i));
            //            break;
            //        }

            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //вниз влево
            //    if (row + i >= 0 && col - i >= 0)
            //    {
            //        if (field[row + i, col - i] == null)
            //            result.Add(new Point(col - i, row + i));
            //        else if (field[row + i, col - i] != null && field[row + i, col - i].white != white)
            //        {
            //            result.Add(new Point(col - i, row + i));
            //            break;
            //        }
            //    }
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    //вниз вправо
            //    if (row + i >= 0 && col + i >= 0)
            //    {
            //        if (field[row + i, col + i] == null)
            //            result.Add(new Point(col + i, row + i));
            //        else if (field[row + i, col + i] != null && field[row + i, col + i].white != white)
            //        {
            //            result.Add(new Point(col + i, row + i));
            //            break;
            //        }
            //    }
            //}
            return result;
        }
    }



}

