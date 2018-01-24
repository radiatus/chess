using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    class FigureRook : Figure //Ладья
    {
        public FigureRook(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            Figures.Sprites sprites = new Sprites();
            sprites.FillDictionary();
            if (white)
                return sprites.dictionarySprites["whiteRook"];
            else
                return sprites.dictionarySprites["blackRook"];
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            for (int i = 0; i < 8; i++)
            {
                //вниз
                if (row + i < 8)
                {
                    if (field[row + i, col] == null)
                        result.Add(new Point(col, row + i));
                    else
                    if (field[row + i, col] != null)
                    {
                        if (field[row + i, col].white != white)
                            result.Add(new Point(col, row + i));
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
                        result.Add(new Point(col, row - i));
                    else
                    if (field[row - i, col] != null)
                    {
                        if (field[row - i, col].white != white)
                            result.Add(new Point(col, row - i));
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
                        result.Add(new Point(col + i, row));
                    else
                    if (field[row, col + i] != null)
                    {
                        if (field[row + i, col].white != white)
                            result.Add(new Point(col + i, row));
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
                        result.Add(new Point(col - i, row));
                    else
                    if (field[row, col - i] != null)
                    {
                        if (field[row, col - i].white != white)
                            result.Add(new Point(col - i, row));
                        break;
                    }
                }
            }

            return result;
        }
    }
}
