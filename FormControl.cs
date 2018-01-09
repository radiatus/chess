using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class FormControl : Player // Реальный игрок. это адаптер
    {
        public int fromX;
        public int fromY;
        public bool figureChosen;
        private IFormControlStrategy strategy;//стратегия, которая позволяет делать или не делать те или иные ходы (если королю поставлен шах)
        public event MoveDelegate moveMade; // сигнал
        
        public FormControl(GameData data, bool isWhite)
            : base(data, isWhite)
        {
            figureChosen = false;
            strategy = new FormControlNormalStrategy();
        }
        public override void setMoveFunc(MoveDelegate moveFunc)
        {
            moveMade = moveFunc; //привязываем функцию к сигналу
        }

        override public void getMoveComand()
        {
            myTurn = true; //пришел сигнал что ход наш
        }

        override public void getStopMoveComand()
        {
            myTurn = false; //пришел сигнал что ход не наш
            figureChosen = false;
        }
        override public void setKingChecked()
        {
            isKingChecked = true;
            strategy = new FormControlСheckToKingStrategy();
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
              
                if (strategy.IsStepCorrect(this.data, fromX, fromY, col, row, isWhite))//проверка на корректность хода
                {
                    isKingChecked = false;
                    strategy = new FormControlNormalStrategy();// если королю был объявлен шах, то ход был коректным и королю терь норм

                    myTurn = false;
                    moveMade(new Command(this.data, fromX, fromY, col, row)); //отправляем сигнал, что ход сделан
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
