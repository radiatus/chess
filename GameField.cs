using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class GameField
    {
        private Figure[,] figures;
        private int size;
        private int indentX;
        private int indentY;
        private int cellSize;        
        private int activeFigureRow;
        private int activeFigureCol;
        private List<Point> highlightPoints;

        public Figure this[int row, int col, bool nonCell = false] //nonCell - значит фактические координаты, а не от 0 до 7
        {
            get
            {
                if (nonCell)
                {
                    Point p = getCord(row, col);
                    return figures[p.X, p.Y];
                }
                else
                {
                    return figures[row, col];
                }
            } //добавить условия
            set { }
        }

        public GameField(int width, int height)
        {
            InitFigures();
            Resize(width, height);
        }

        public void Draw(Graphics canvas ) // здесь хорошо бы сделать перечисление
        {
            DrawField(canvas, cellSize, indentX, indentY);
            DrawFigures(canvas, cellSize, indentX, indentY);

            if (highlightPoints != null)
                DrawHighLights(canvas);
        }

        public Point getCord(int x, int y)
        {
            int col = (x - indentX) / cellSize;
            int row = (y - indentY) / cellSize;

            return new Point(col, row);
        }

        public void Resize(int width, int height)
        {
            size = Math.Min(width, height);
            size = size - (size % 8); //размер должен быть кратен 

            indentX = (size % 8 / 2) + (width - size);
            indentY = (size % 8 / 2) + (height - size);
            cellSize = size / 8;
        }

        public void ActivateFigure(int col, int row)
        {
            if (highlightPoints != null)
                highlightPoints.Clear();
            if (figures[row, col] != null)
            {
                highlightPoints = figures[row, col].GetPosibleSteps();
                highlightPoints.Add(new Point(col, row));
            }
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

        private void DrawField(Graphics canvas, int cellSize, int indentX, int indentY)
        {
            Brush black = new SolidBrush(Color.Brown); //Chocolate
            Brush white = new SolidBrush(Color.Gold);
            Brush[] cols = new Brush[] { white, black };

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    canvas.FillRectangle(cols[(row + col) % 2], indentX + col * cellSize, indentY + row * cellSize, cellSize, cellSize);

        }

        private void DrawFigures(Graphics canvas, int cellSize, int indentX, int indentY)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (figures[row, col] != null)
                    {
                        Bitmap tmp = new Bitmap(figures[row, col].GetSprite(), cellSize, cellSize);
                        canvas.DrawImage(tmp, indentX + col * cellSize, indentY + row * cellSize);
                    }
                }
            }
        }

        private void DrawHighLights(Graphics canvas)
        {
            Pen pen = new Pen(Color.Blue, 5);
            int row;
            int col;
            for (int cont = 0; cont < highlightPoints.Count; cont++)
            {
                col = highlightPoints[cont].X;
                row = highlightPoints[cont].Y;

                if (figures[row, col] != null)
                    canvas.DrawRectangle(pen, indentX + col * cellSize + 2, indentY + row * cellSize + 2, cellSize - 4, cellSize - 4);
                else
                    canvas.FillEllipse(new SolidBrush(Color.Blue), indentX + (col * cellSize) + cellSize / 4, indentY + (row * cellSize) + cellSize / 4, cellSize / 2, cellSize / 2);
            }
        }
    }
}
