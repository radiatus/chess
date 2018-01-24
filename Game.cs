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
        private List<Command> gameHistory;
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
            gameHistory = new List<Command>();

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

        public void UndoStep() // отмена хода // тут нужна серьезная дороотка
        {

            if (gameHistory.Count > 0)
            {
                gameHistory[gameHistory.Count - 1].Undo();
                isWhitTurn = !isWhitTurn;

                if (isWhitTurn)
                {
                    playerBlack.getStopMoveComand();
                    emitWhiteMove();
                }
                else
                {
                    playerWhite.getStopMoveComand();
                    emitBlackMove();
                }
                gameHistory.RemoveAt(gameHistory.Count - 1);
            }            

        }

        public string getCountDead(bool white) //временная функция для получения некоторой информации о поле
        {
            string s = "";
            if (white)
                s += "white dead = " + data.getDeadFig(white).Count;
            if (!white)
                s += "black dead = " + data.getDeadFig(white).Count;

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

        private void PlayerMoved(MoveComand emitActiv, MoveComand emitPassiv, Command command)
        {

            if (command.comandIsCorrect) // ход корректен
            {
                gameHistory.Add(command);
                field.DisActivate(); // убираем подсветку
                command.Execute();
                isWhitTurn = !isWhitTurn; // меняем порядок шагов


                if (data.IsKingChecked(isWhitTurn))
                {
                    if (isWhitTurn)
                        playerWhite.setKingChecked();
                    else
                        playerBlack.setKingChecked();
                }

                emitPassiv();//посылаем сигнал, что ходит тот, кто ждал
            }
            else // ход не удался
            {
                emitActiv(); // посылаем сигнал, что опять ходит тот, кто ходил
            }
        }

        private void WhitePlayerMoved(Command command) // сигнал о том, что сходил белый
        {
            PlayerMoved(emitWhiteMove, emitBlackMove, command);
        }
        private void BlackPlayerMoved(Command command) // сигнал о том, что сходил черный
        {
            PlayerMoved(emitBlackMove, emitWhiteMove, command);
        }

        public GameMemento SaveGame()
        {
            return new GameMemento(data, field, playerWhite, playerBlack, playerWhiteFormControl, playerBlackFormControl);
        }
    }

    class GameMemento
    {
        public GameData Data { get; private set; }
        public GameField Field { get; private set; }
        public Player PlayerWhite { get; private set; }
        public Player PlayerBlack { get; private set; }
        public FormControl PlayerWhiteFormControl { get; private set; }
        public FormControl PlayerBlackFormControl { get; private set; }

        public GameMemento(GameData data, GameField field, Player playerWhite, Player playerBlack, FormControl playerWhiteFormControl, FormControl playerBlackFormControl)
        {
            this.Data = data;
            this.Field = field;
            this.PlayerWhite = playerWhite;
            this.PlayerBlack = playerBlack;
            this.PlayerWhiteFormControl = playerWhiteFormControl;
            this.PlayerBlackFormControl = playerBlackFormControl;
        }
    }

    class GameHistory
    {
        public Stack<GameMemento> History { get; private set; }
        public GameHistory()
        {
            History = new Stack<GameMemento>();
        }
    }
}

/*
 private bool isWhitTurn;
        private GameData data;
        private GameField field;
        private Player playerWhite;
        private Player playerBlack;
        private FormControl playerWhiteFormControl;
        private FormControl playerBlackFormControl;
 */
