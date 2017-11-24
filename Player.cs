using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    abstract class Player
    {       
        protected bool isWhite;
        protected bool myTurn;
        protected GameData data;

        public delegate void MoveDelegate(int fromX, int fromY, int toX, int toY);

        public Player(GameData data, bool isWhite)
        {
            this.data = data;
            this.isWhite = isWhite;
            //this.moveMade += moveFunc;
        }
        abstract public void setMoveFunc(MoveDelegate moveFunc);
        abstract public void getMoveComand();
    }
}
