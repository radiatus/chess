using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
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
}
