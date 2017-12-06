using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
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


        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            for (int i = 0; i < 8; i++)
            {
                //вверх вправо 
                if (row - i >= 0 && col + i < 8)
                {
                    if (field[row - i, col + i] == null)
                        result.Add(new Point(col + i, row - i));
                    else if (field[row - i, col + i] != null)
                    {
                        if (field[row - i, col + i].white != white)
                            result.Add(new Point(col + i, row - i));
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
                        result.Add(new Point(col - i, row - i));
                    else if (field[row - i, col - i] != null)
                    {
                        if (field[row - i, col - i].white != white)
                            result.Add(new Point(col - i, row - i));
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
                        result.Add(new Point(col - i, row + i));
                    else if (field[row + i, col - i] != null)
                    {
                        if (field[row + i, col - i].white != white)
                            result.Add(new Point(col - i, row + i));
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
                        result.Add(new Point(col + i, row + i));
                    else if (field[row + i, col + i] != null)
                    {
                        if (field[row + i, col + i].white != white)
                            result.Add(new Point(col + i, row + i));
                        break;
                    }
                }
            }
            return result;
        }
    }
}
