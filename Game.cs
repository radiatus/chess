using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    class Game //Фасад, все взаимодействие и связь всех частей
    {
        private static Game instance;

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

        private Game(GameData data, GameField field, Player playerWhite, Player playerBlack)
        {
            this.data = data;
            this.field = field;
            this.playerWhite = playerWhite;
            this.playerBlack = playerBlack;

            this.playerWhite.setMoveFunc(WhitePlayerMoved); //привязываем функции к событиям
            this.playerBlack.setMoveFunc(BlackPlayerMoved);
            emitWhiteMove = this.playerWhite.getMoveComand;
            emitBlackMove = this.playerBlack.getMoveComand;

            if (this.playerWhite is FormControl) //если БЕЛЫЙ это formControl            
                playerWhiteFormControl = (FormControl)this.playerWhite; //приводя их к такому виду, мы избавляемся от множества проверок
            if (this.playerBlack is FormControl) //если ЧЕРНЫЙ это formControl            
                playerBlackFormControl = (FormControl)this.playerBlack;            

            isWhitTurn = true;
            emitWhiteMove();
        }

        public static Game getInstance(GameData data, GameField field, Player playerWhite, Player playerBlack)
        {
            if (instance == null)
                instance = new Game(data, field, playerWhite, playerBlack);
            return instance;
        }

        public string getSomeStuff() //временная функция для получения некоторой информации о поле
        {
            string s = "";
            s += "turn: " + (isWhitTurn ? "white" : "black") + "\n";

            return s;
        }

        public void Click(int x, int y) // щелчек по полю, в реальных координатах
        {
            Point p = field.getCord(x, y);
            int row = p.Y;
            int col = p.X;

            field.ActivateFigure(p); //подсвечиваем

            if (playerWhiteFormControl != null && isWhitTurn) //если БЕЛЫЙ это formControl и сейчас его ход     
                playerWhiteFormControl.Move(row, col);            
            else if (playerBlackFormControl != null && !isWhitTurn) //если ЧЕРНЫЙ это formControl и сейчас его ход   
                playerBlackFormControl.Move(row, col);
        }

        public void ResizeField(int width, int height) //изменяем размер
        {
            field.Resize(width, height);
        }

        public void Draw(Graphics canvas) //рисуем
        {
            field.Draw(canvas);
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
        private void PlayerMoved(MoveComand emitActiv, MoveComand emitPassiv, int fromX, int fromY, int toX, int toY)
        {
            if (CheckMove(fromX, fromY, toX, toY)) // ход корректен
            {
                // Сделать тут проверку на убийство?
                Figures.Figure tmp = data[fromY, fromX]; // сохраняем перемещаемую фигуру
                tmp.moveTo(toY, toX); //меняем ее координаты
                data[fromY, fromX] = null; // стираем ее на старом месте
                data[toY, toX] = tmp; // ставим на новое
                field.DisActivate(); // убираем подсветку

                isWhitTurn = !isWhitTurn;
                emitPassiv();//посылаем сигнал, что опять ходит тот, кто ждал
            }
            else // ход не удался
            {
                emitActiv(); // посылаем сигнал, что опять ходит тот, кто ходил
            }
        }

        private void WhitePlayerMoved(int fromX, int fromY, int toX, int toY) // сигнал о том, что сходил белый
        {
            PlayerMoved(emitWhiteMove, emitBlackMove, fromX, fromY, toX, toY);
        }
        private void BlackPlayerMoved(int fromX, int fromY, int toX, int toY) // сигнал о том, что сходил черный
        {
            PlayerMoved(emitBlackMove, emitWhiteMove, fromX, fromY, toX, toY);
        }

    }
}
