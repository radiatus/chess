using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    class GameData //Вся информация о поле
    {
        private Figures.Figure[,] figures; //массив фигурок
        private List<Figures.Figure> whiteDead = new List<Figures.Figure>(); //список мертвых БЕЛЫХ фигурок
        private List<Figures.Figure> blackDead = new List<Figures.Figure>(); //список мертвых ЧЕРНЫХ фигурок

        public List<Figures.Figure> getDeadFig(bool white)
        {
            if (white)
                return whiteDead;
            return blackDead;
        }

        public void AddDeadFigure(int row, int col)
        {
            if (figures[row, col].white)
                whiteDead.Add(figures[row, col]);

            if (!figures[row, col].white)
                blackDead.Add(figures[row, col]);

            figures[row, col] = null;
        }

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
                figures[row, col] = value;
                //if (figures[row, col] != null)// Сделать тут проверку на убийство?
                //{
                //    if (value.white)
                //    {

                //    }
                //}
            }
        }

        public GameData()
        {
            InitFigures();
            // позже добавим возможность чтения из файла, передавая путь
        }

        private void InitFigures() //стандартная инициализация
        {
            figures = new Figures.Figure[8, 8];


            for (int i = 0; i < 8; i++)
            {
                //Пешки
                figures[1, i] = new Figures.FigurePawn(false, this, 1, i);
                figures[6, i] = new Figures.FigurePawn(true, this, 6, i);
            }

            //Ладьи
            figures[0, 0] = new Figures.FigureRook(false, this, 0, 0);
            figures[0, 7] = new Figures.FigureRook(false, this, 0, 7);
            figures[7, 0] = new Figures.FigureRook(true, this, 7, 0);
            figures[7, 7] = new Figures.FigureRook(true, this, 7, 7);

            //Кони
            figures[0, 1] = new Figures.FigureKnight(false, this, 0, 1);
            figures[0, 6] = new Figures.FigureKnight(false, this, 0, 6);
            figures[7, 1] = new Figures.FigureKnight(true, this, 7, 1);
            figures[7, 6] = new Figures.FigureKnight(true, this, 7, 6);

            //Слоны
            figures[0, 2] = new Figures.FigureBishop(false, this, 0, 2);
            figures[0, 5] = new Figures.FigureBishop(false, this, 0, 5);
            figures[7, 2] = new Figures.FigureBishop(true, this, 7, 2);
            figures[7, 5] = new Figures.FigureBishop(true, this, 7, 5);

            //Ферзи
            figures[0, 3] = new Figures.FigureQueen(false, this, 0, 3);
            figures[7, 3] = new Figures.FigureQueen(true, this, 7, 3);

            //Короли
            figures[0, 4] = new Figures.FigureKing(false, this, 0, 4);
            figures[7, 4] = new Figures.FigureKing(true, this, 7, 4);





            figures[4, 3] = new Figures.FigureKnight(true, this, 4, 3);

            figures[4, 4] = new Figures.FigureRook(false, this, 4, 4);
            figures[3, 3] = new Figures.FigureRook(true, this, 3, 3);
        }


    }
}
