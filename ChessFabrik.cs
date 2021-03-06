﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    static class ChessFabrik
    {
        public static Game TwoPlayersGame(int width, int height) //создает игру с двумя реальными игроками
        {
            GameData data = new GameData();
            GameField field = new GameField(data, width, height);
            Player white = new FormControl(data, true);
            Player black = new FormControl(data, false);
            Game game = Game.getInstance(data, field, white, black); //надеюсь тут более менее понятно
            return game;
        }
    }
}
