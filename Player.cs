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
        protected GameData data;

        public delegate void MoveDelegate(int fromX, int fromY, int toX, int toY);

        public Player(GameData data, bool isWhite)
        {
            this.data = data;
            this.isWhite = isWhite;
        }
        abstract public void setMoveFunc(MoveDelegate moveFunc); //для привязывания функции к событию
        abstract public void getMoveComand(); //для получения события о том, что настало время ходить
    }
}
