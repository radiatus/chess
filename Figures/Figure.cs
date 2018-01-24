using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess.Figures
{
    abstract class Figure //Абстрактная фигурка
    {
        public bool white;
        protected GameData field;
        protected int row;
        protected int col;

        public Figure(bool isWhite, GameData field, int row, int col)
        {
            white = isWhite;
            this.field = field;
            this.row = row;
            this.col = col;
        }

        public abstract Bitmap GetSprite(); //дает спрайт фигурки
        public abstract List<Point> GetPosibleSteps(); //список клеток, в которые можно сделать ход
        public virtual void moveTo(int row, int col) //перемещение фигурки в новую точку, не контролируется фигуркой
        {
            this.row = row;
            this.col = col;
        }
    }

    public class Sprites
    {
        public Dictionary<string, Bitmap> dictionarySprites = new Dictionary<string, Bitmap>();
        public Bitmap this[string key]
        {
            get
            {
                return dictionarySprites[key];
            }
            set
            {
                dictionarySprites[key] = value;
            }
        }
        public void FillDictionary()
        {
            //пешки
            dictionarySprites.Add("whitePawn", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whitePawn.png")));
            dictionarySprites.Add("blackPawn", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackPawn.png")));
            //слоны
            dictionarySprites.Add("whiteBishop", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteBishop.png")));
            dictionarySprites.Add("blackBishop", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackBishop.png")));
            //кони
            dictionarySprites.Add("whiteKnight", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteKnight.png")));
            dictionarySprites.Add("blackKnight", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackKnight.png")));
            //ладьи
            dictionarySprites.Add("whiteRook", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteRook.png")));
            dictionarySprites.Add("blackRook", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackRook.png")));
            //ферзи
            dictionarySprites.Add("whiteQueen", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteQueen.png")));
            dictionarySprites.Add("blackQueen", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackQueen.png")));
            //короли
            dictionarySprites.Add("whiteKing", new Bitmap(Image.FromFile(@"..\..\figure_sprites\whiteKing.png")));
            dictionarySprites.Add("blackKing", new Bitmap(Image.FromFile(@"..\..\figure_sprites\blackKing.png")));
        }
    }
}
