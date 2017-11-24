using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class Game
    {
        private bool isWhitTurn;
        private GameData data;
        private GameField field;
        private Player playerWhite;
        private Player playerBlack;
        private FormControl playerWhiteFormControl;
        private FormControl playerBlackFormControl;

        public delegate void MoveComand();
        MoveComand emitWhiteMove;
        MoveComand emitBlackMove;

        public Game(GameData data, GameField field, Player playerWhite, Player playerBlack)
        {
            this.data = data;
            this.field = field;
            this.playerWhite = playerWhite;
            this.playerBlack = playerBlack;

            this.playerWhite.setMoveFunc(WhitePlayerMoved);
            this.playerBlack.setMoveFunc(BlackPlayerMoved);
            emitWhiteMove = this.playerWhite.getMoveComand;
            emitBlackMove = this.playerBlack.getMoveComand;

            if (this.playerWhite is FormControl)
            {
                //playerWhiteFormControl = this.playerWhite as FormControl; //приводя их к такому виду, мы избавляемся от множества проверок
                playerWhiteFormControl = (FormControl)this.playerWhite;
            }
            if (this.playerBlack is FormControl)
            {
                //playerBlackFormControl = this.playerBlack as FormControl;
                playerBlackFormControl = (FormControl)this.playerBlack;
            }

            isWhitTurn = true;
            emitWhiteMove();
        }

        public string getSomeStuff()
        {
            string s = "";
            s += "turn: " + (isWhitTurn ? "white" : "black") + "\n";

            return s;
        }

        public void Click(int x, int y)
        {
            Point p = field.getCord(x, y);
            int row = p.Y;
            int col = p.X;

            field.ActivateFigure(p);

            if (playerWhiteFormControl != null && isWhitTurn)            
                playerWhiteFormControl.Move(row, col);            
            else if (playerBlackFormControl != null && !isWhitTurn)
                playerBlackFormControl.Move(row, col);
        }

        public void ResizeField(int width, int height)
        {
            field.Resize(width, height);
        }

        public void Draw(Graphics canvas)
        {
            field.Draw(canvas);
        }

        private bool CheckMove(int fromX, int fromY, int toX, int toY)
        {
            if (data[fromY, fromX] == null)//ход из пустой клетки
                return false;

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
        private void PlayerMoved(MoveComand emitActiv, MoveComand emitPassiv, int fromX, int fromY, int toX, int toY)
        {
            if (CheckMove(fromX, fromY, toX, toY)) // ход корректен
            {
                // Сделать тут проверку на убийство?
                Figure tmp = data[fromY, fromX]; // сохраняем перемещаемую фигуру
                tmp.moveTo(toY, toX); //меняем ее координаты
                data[fromY, fromX] = null; // стираем ее на старом месте
                data[toY, toX] = tmp; // ставим на новое
                field.DisActivate();

                isWhitTurn = !isWhitTurn;
                emitPassiv();//посылаем сигнал, что опять ходит тот, кто ждал
            }
            else //ход не удался
            {
                emitActiv(); //посылаем сигнал, что опять ходит тот, кто ходил
            }
        }

        private void WhitePlayerMoved(int fromX, int fromY, int toX, int toY)
        {
            PlayerMoved(emitWhiteMove, emitBlackMove, fromX, fromY, toX, toY);
        }
        private void BlackPlayerMoved(int fromX, int fromY, int toX, int toY)
        {
            PlayerMoved(emitBlackMove, emitWhiteMove, fromX, fromY, toX, toY);
        }

    }
}
