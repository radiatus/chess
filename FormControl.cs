using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class FormControl : Player // Реальный игрок
    {
        private int fromX;
        private int fromY;
        private bool figureChosen;
        public event MoveDelegate moveMade; // сигнал


        public FormControl(GameData data, bool isWhite)
            : base(data, isWhite)
        {
            figureChosen = false;
        }
        public override void setMoveFunc(MoveDelegate moveFunc)
        {
            moveMade = moveFunc; //привязываем функцию к сигналу
        }

        override public void getMoveComand()
        {
            myTurn = true; //пришел сигнал что ход наш
        }

        public void Move(int row, int col) //клик по полю
        {
            if (!myTurn) //ничего не делаем, если не наш ход
                return;

            if (figureChosen) //если фигурка уже выбрана
            {                
                if(data[row, col] != null && data[row, col].white == isWhite) //клик по другой своей фигурке, выбираем ее
                {
                    figureChosen = true;
                    fromX = col;
                    fromY = row;
                    return;
                }

                //проверка на корректность хода
                List<Point> stepList = data[fromY, fromX].GetPosibleSteps();                
                bool stepCorrect = false;
                for (int stepInd = 0; stepInd < stepList.Count; stepInd++)
                    if (stepList[stepInd].X == col && stepList[stepInd].Y == row)
                    {                        
                        stepCorrect = true;
                        break;
                    }

                bool wasByKill = false;
                if (data[row, col] != null && data[row, col].white != isWhite)
                    wasByKill = true;

                if (stepCorrect)
                {
                    myTurn = false;
                    moveMade(fromX, fromY, col, row, wasByKill); //отправляем сигнал, что ход сделан
                }

                figureChosen = false;
            }
            else //выбираем фигурку, на которую мы кликнули
            {
                if (data[row, col] != null && data[row, col].white == isWhite)
                {
                    figureChosen = true;
                    fromX = col;
                    fromY = row;
                }
            }
        }


    }
}
