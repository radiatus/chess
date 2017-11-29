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
                figures[1, i] = new FigurePawn(false, this, 1, i);
                figures[6, i] = new FigurePawn(true, this, 6, i);
                figures[2, 2] = new FigurePawn(true, this, 2, 2);
            }

        }


    }
}
