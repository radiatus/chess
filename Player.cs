using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    abstract class Player //Абстрактный класс игрока, может быть как реальным так и компьютером
    {       
        protected bool isWhite;
        protected bool myTurn;
        protected bool isKingChecked;
        protected GameData data;

        public bool IsKingChecked
        {
            get { return isKingChecked; }
        }

        public delegate void MoveDelegate(Command command);

        public Player(GameData data, bool isWhite)
        {
            this.data = data;
            this.isWhite = isWhite;
            isKingChecked = false;
        }
        abstract public void setMoveFunc(MoveDelegate moveFunc); //для привязывания функции к событию
        abstract public void getMoveComand(); //для получения события о том, что настало время ходить
        abstract public void getStopMoveComand(); //для получения события о том, что ходить уже не надо
        abstract public void setKingChecked();
    }
}
