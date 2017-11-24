using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    class GameData
    {
        private Figure[,] figures;
        private List<Figure> whiteDead;
        private List<Figure> blackDead;


        public Figure this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= 8 && col < 0 && col >= 8)
                    return null;

                return figures[row, col];
                
            } //добавить условия
            set
            {
                if (row < 0 || row >= 8 && col < 0 && col >= 8)
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
        }

        private void InitFigures()
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
