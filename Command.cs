using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class Command
    {
        private int fromX;
        private int fromY;
        private int toX;
        private int toY;
        private Figures.Figure diedFigure;
        private GameData data;
        public bool comandIsCorrect;

        public Command(GameData data, int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            this.data = data;
            comandIsCorrect = CheckMove(fromX, fromY, toX, toY);
        }

        public void Execute()
        {
            if (comandIsCorrect)
            {
                diedFigure = data[toY, toX]; //запоминаем выбранную фигуру
                Figures.Figure tmp = data[fromY, fromX]; // сохраняем перемещаемую фигуру
                tmp.moveTo(toY, toX); //меняем ее координаты
                data[fromY, fromX] = null; // стираем ее на старом месте
                data[toY, toX] = tmp; // ставим на новое
            }
        }

        public void Undo()
        {
            if (comandIsCorrect)
            {
                Figures.Figure tmp = data[toY, toX]; // сохраняем перемещаемую фигуру
                data[fromY, fromX] = tmp;//ставим ее на старом месте
                tmp.moveTo(fromY, fromX); 
                data[toY, toX] = diedFigure; //восстанавливаем убитую фигурку
            }
        }

        private bool CheckMove(int fromX, int fromY, int toX, int toY) // проверка на правильность хода 
        {
            if (data[fromY, fromX] == null) //ход из пустой клетки
                return false;

            // ход соответствует одному из возможных ходов фигуры 
            List<Point> stepList = data[fromY, fromX].GetPosibleSteps();
            bool stepCorrect = false;

            for (int stepInd = 0; stepInd < stepList.Count; stepInd++)
                if (stepList[stepInd].X == toX && stepList[stepInd].Y == toY)
                {
                    stepCorrect = true;
                    break;
                }

            return stepCorrect;
        }

    }
}
