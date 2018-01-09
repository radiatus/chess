using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    interface IFormControlStrategy
    {
        //void Move(int row, int col, ref bool myTurn, ref bool figureChosen);
        bool IsStepCorrect(GameData data, int fromX, int fromY, int col, int row, bool white);
    }

    class FormControlNormalStrategy : IFormControlStrategy
    {
        public bool IsStepCorrect(GameData data, int fromX, int fromY, int col, int row, bool white)
        {
            List<Point> stepList = data[fromY, fromX].GetPosibleSteps();
            bool stepCorrect = false;
            for (int stepInd = 0; stepInd < stepList.Count; stepInd++)
            {
                if (stepList[stepInd].X == col && stepList[stepInd].Y == row)
                {
                    stepCorrect = true;
                    break;
                }
            }
            return stepCorrect;
        }

    }
    class FormControlСheckToKingStrategy : IFormControlStrategy
    {
        public FormControlСheckToKingStrategy() { }

        public bool IsStepCorrect(GameData data, int fromX, int fromY, int col, int row, bool white)
        {
            List<Point> stepList = data[fromY, fromX].GetPosibleSteps();
            bool stepCorrect = false;
            for (int stepInd = 0; stepInd < stepList.Count; stepInd++)
            {
                if (stepList[stepInd].X == col && stepList[stepInd].Y == row)
                {
                    stepCorrect = true;
                    break;
                }
            }            

            //если королю объявлен шах, то проверяем, избежим ли мы его этой командой            
            Command tmpCommand = new Command(data, fromX, fromY, col, row);
            tmpCommand.Execute();
            if (data.IsKingChecked(white)) //если после выполнения ничего не изменилось, то плохой ход
                stepCorrect = false;
            else
                stepCorrect = true;
            tmpCommand.Undo();  

            return stepCorrect;
        }
    }
}
