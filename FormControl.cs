using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class FormControl : Player
    {
        private int fromX;
        private int fromY;
        private bool figureChosen;
        public event MoveDelegate moveMade;


        public FormControl(GameData data, bool isWhite)
            : base(data, isWhite)
        {
            figureChosen = false;
        }
        public override void setMoveFunc(MoveDelegate moveFunc)
        {
            moveMade = moveFunc;
        }

        override public void getMoveComand()
        {
            myTurn = true;
        }

        public void Move(int row, int col)
        {
            if (!myTurn)
                return;

            if (figureChosen)
            {                
                if(data[row, col] != null && data[row, col].white == isWhite)
                {
                    figureChosen = true;
                    fromX = col;
                    fromY = row;
                    return;
                }

                List<Point> stepList = data[fromY, fromX].GetPosibleSteps();

                bool stepCorrect = false;
                for (int stepInd = 0; stepInd < stepList.Count; stepInd++)
                    if (stepList[stepInd].X == col && stepList[stepInd].Y == row)
                    {                        
                        stepCorrect = true;
                        break;
                    }

                if (stepCorrect)
                {
                    myTurn = false;
                    moveMade(fromX, fromY, col, row);
                }

                figureChosen = false;
            }
            else
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
