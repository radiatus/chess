using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    class FigureKing : Figure //Король
    {
        public FigureKing(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            Figures.Sprites sprites = new Sprites();
            sprites.FillDictionary();
            if (white)
                return sprites.dictionarySprites["whiteKing"];
            else
                return sprites.dictionarySprites["blackKing"];
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
}
