using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class GameData //Вся информация о поле
    {
        private Point cordWhiteKing;
        private Point cordBlackKing;
        private Figures.Figure[,] figures; //массив фигурок
        private List<Figures.Figure> whiteDead = new List<Figures.Figure>(); //список мертвых БЕЛЫХ фигурок
        private List<Figures.Figure> blackDead = new List<Figures.Figure>(); //список мертвых ЧЕРНЫХ фигурок
        
        public Figures.Figure this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= 8 || col < 0 || col >= 8)
                    return null;

                return figures[row, col];

            } //добавить условия
            set
            {
                if (row < 0 || row >= 8 || col < 0 || col >= 8)
                    return;

                if (figures[row, col] != null && value != null) //проверка на убийство
                {
                    AddDeadFigure(row, col);
                    if (row == cordBlackKing.Y && col == cordBlackKing.X)
                        return; // выиграл белый
                    else if (row == cordWhiteKing.Y && col == cordWhiteKing.X)
                        return; // выиграл черный
                }
                
                if (value is Figures.FigureKing) // если это король
                {
                    if ((value as Figures.FigureKing).white)
                    {
                        cordWhiteKing.Y = row;
                        cordWhiteKing.X = col;
                    }
                    else
                    {
                        cordBlackKing.Y = row;
                        cordBlackKing.X = col;
                    }
                }

                figures[row, col] = value;
            }
        }

        public GameData()
        {
            InitFigures();
            // позже добавим возможность чтения из файла, передавая путь
        }

        public List<Figures.Figure> getDeadFig(bool white)
        {
            if (white)
                return whiteDead;
            return blackDead;
        }

        public bool IsKingChecked(bool white)
        {
            Point cordKing;
            if (white)
                cordKing = cordWhiteKing;
            else
                cordKing = cordBlackKing;

            List<Point> possibleSteps;            
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)                
                    if (figures[row, col] != null && figures[row, col].white)
                    {
                        possibleSteps = figures[row, col].GetPosibleSteps();
                        for (int i = 0; i < possibleSteps.Count; i++)
                            if (cordKing.X == possibleSteps[i].X && cordKing.Y == possibleSteps[i].Y)
                                return true;
                    }

            return false;
        }

        private void InitFigures() //стандартная инициализация
        {
            figures = new Figures.Figure[8, 8];


            for (int i = 0; i < 8; i++)
            {
                //Пешки
                this[1, i] = new Figures.FigurePawn(false, this, 1, i);
                this[6, i] = new Figures.FigurePawn(true, this, 6, i);
            }

            //Ладьи
            this[0, 0] = new Figures.FigureRook(false, this, 0, 0);
            this[0, 7] = new Figures.FigureRook(false, this, 0, 7);
            this[7, 0] = new Figures.FigureRook(true, this, 7, 0);
            this[7, 7] = new Figures.FigureRook(true, this, 7, 7);

            //Кони
            this[0, 1] = new Figures.FigureKnight(false, this, 0, 1);
            this[0, 6] = new Figures.FigureKnight(false, this, 0, 6);
            this[7, 1] = new Figures.FigureKnight(true, this, 7, 1);
            this[7, 6] = new Figures.FigureKnight(true, this, 7, 6);

            //Слоны
            this[0, 2] = new Figures.FigureBishop(false, this, 0, 2);
            this[0, 5] = new Figures.FigureBishop(false, this, 0, 5);
            this[7, 2] = new Figures.FigureBishop(true, this, 7, 2);
            this[7, 5] = new Figures.FigureBishop(true, this, 7, 5);

            //Ферзи
            this[0, 3] = new Figures.FigureQueen(false, this, 0, 3);
            this[7, 3] = new Figures.FigureQueen(true, this, 7, 3);

            //Короли
            this[0, 4] = new Figures.FigureKing(false, this, 0, 4);
            this[7, 4] = new Figures.FigureKing(true, this, 7, 4);
            cordBlackKing = new Point(4, 0);
            cordWhiteKing = new Point(4, 7);



            //figures[4, 3] = new Figures.FigureKnight(true, this, 4, 3);

            //figures[4, 4] = new Figures.FigureRook(false, this, 4, 4);
            //figures[3, 3] = new Figures.FigureRook(true, this, 3, 3);
        }

        private void AddDeadFigure(int row, int col)
        {
            if (figures[row, col].white)
                whiteDead.Add(figures[row, col]);

            if (!figures[row, col].white)
                blackDead.Add(figures[row, col]);

            figures[row, col] = null;
        }
    }
}
