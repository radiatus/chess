using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class GameField //система визуализации
    {
        
        private GameData figures; // все фигурки
        private int size;
        private int indentX;
        private int indentY;
        private int cellSize;
        private int activeFigureRow;
        private int activeFigureCol;
        private List<Point> highlightPoints; //список фигурок кторые надо подсветить

        public GameField(GameData data, int width, int height)
        {
            Resize(width, height); 
            this.figures = data;
        }

        public void Draw(Graphics canvas)
        {
            DrawField(canvas, cellSize, indentX, indentY); //рисуем само поле
            DrawFigures(canvas, cellSize, indentX, indentY); //рисуем все фигурки на нем

            if (highlightPoints != null)
                DrawHighLights(canvas); //рисуем подсвеченые клетки
        }

        public Point getCord(int x, int y) // возвращает строку и столбец, получая реальные координаты
        {
            int col = (x - indentX) / cellSize;
            int row = (y - indentY) / cellSize;

            return new Point(col, row);
        }

        public void Resize(int width, int height) //изменяем размер поля
        {
            size = Math.Min(width, height);
            size = size - (size % 8); //размер должен быть кратен 8

            indentX = (size % 8 / 2) + (width - size);
            indentY = (size % 8 / 2) + (height - size);
            cellSize = size / 8;
        }
        public void ActivateFigure(Point ColAndRow) //перегрузочка
        {
            ActivateFigure(ColAndRow.X, ColAndRow.Y);         
        }
        
        public void ActivateFigure(int col, int row) //подсвечиваем фигурку и возможные ходы
        {
            if (highlightPoints != null)
                highlightPoints.Clear();
            if (figures[row, col] != null)
            {
                highlightPoints = figures[row, col].GetPosibleSteps();
                highlightPoints.Add(new Point(col, row));
            }
        }
        public void DisActivate() //перестаем подсвечивать
        {
            highlightPoints = null;
        }

        private void DrawField(Graphics canvas, int cellSize, int indentX, int indentY) //рисуем само поле
        {
            Brush black = new SolidBrush(Color.Brown); //Chocolate
            Brush white = new SolidBrush(Color.Gold);
            Brush[] cols = new Brush[] { white, black };

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    canvas.FillRectangle(cols[(row + col) % 2], indentX + col * cellSize, indentY + row * cellSize, cellSize, cellSize);

        }

        private void DrawFigures(Graphics canvas, int cellSize, int indentX, int indentY) //рисуем все фигурки на нем
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

        private void DrawHighLights(Graphics canvas) //рисуем подсвеченые клетки
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
