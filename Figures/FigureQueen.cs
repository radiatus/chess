using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    class FigureQueen : Figure //Ферзь
    {
        public FigureQueen(bool isWhite, GameData field, int row, int col)
            : base(isWhite, field, row, col)
        {

        }

        public override Bitmap GetSprite()
        {
            Figures.Sprites sprites = new Sprites();
            sprites.FillDictionary();
            if (white)
                return sprites.dictionarySprites["whiteQueen"];
            else
                return sprites.dictionarySprites["blackQueen"];
        }

        public override List<Point> GetPosibleSteps()
        {
            List<Point> result = new List<Point>();

            //От ладьи
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

            // От слона
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
