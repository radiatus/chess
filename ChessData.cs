using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    class GameData //Вся информация о поле
    {
        private Figure[,] figures; //массив фигурок
        private List<Figure> whiteDead; //список мертвых БЕЛЫХ фигурок
        private List<Figure> blackDead; //список мертвых ЧЕРНЫХ фигурок


        public Figure this[int row, int col]
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
            figures = new Figure[8, 8];

            
            for (int i = 0; i < 8; i++)
            {
                //Пешки
                figures[1, i] = new FigurePawn(false, this, 1, i);
                figures[6, i] = new FigurePawn(true, this, 6, i);
            }
            
            ////Ладьи
            //figures[0, 0] = new FigureRook(false, this, 0, 0);
            //figures[0, 7] = new FigureRook(false, this, 0, 7);
            //figures[7, 0] = new FigureRook(true, this, 7, 0);
            //figures[7, 7] = new FigureRook(true, this, 7, 7);
            
            //Кони
            figures[0, 1] = new FigureKnight(false, this, 0, 1);
            figures[0, 6] = new FigureKnight(false, this, 0, 6);
            figures[7, 1] = new FigureKnight(true, this, 7, 1);
            figures[7, 6] = new FigureKnight(true, this, 7, 6);
            
            ////Слоны
            //figures[0, 2] = new FigureBishop(false, this, 0, 2);
            //figures[0, 5] = new FigureBishop(false, this, 0, 5);
            //figures[7, 2] = new FigureBishop(true, this, 7, 2);
            //figures[7, 5] = new FigureBishop(true, this, 7, 5);
            
            ////Ферзи
            //figures[0, 3] = new FigureQueen(false, this, 0, 3);
            //figures[7, 3] = new FigureQueen(true, this, 7, 3);
            
            //Короли
            figures[0, 4] = new FigureKing(false, this, 0, 4);
            figures[7, 4] = new FigureKing(true, this, 7, 4);
            

            //figures[3, 3] = new FigureKnight(false, this, 3, 3);
            //figures[4, 4] = new FigureKnight(true, this, 4, 4);

            //figures[2, 2] = new FigureKing(true, this, 2, 2);
            //figures[5, 5] = new FigureKing(false, this, 5, 5);
        }


    }
}
