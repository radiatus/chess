using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    class FigurePawn : Figure //Пешка
    {
        private bool firstStep;

        public FigurePawn(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {
            firstStep = true;
        }
        
        public override Bitmap GetSprite()
        {
            Figures.Sprites sprites = new Sprites();
            sprites.FillDictionary();
            if (white)
                return sprites.dictionarySprites["whitePawn"];
            else
                return sprites.dictionarySprites["blackPawn"];
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
}
