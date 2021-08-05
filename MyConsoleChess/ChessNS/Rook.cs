using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Rook : Piece
    {
        public bool Moved { get; private set; }
        public Rook(int row, int collum, Color color, Chess chess) : base(row, collum, color, chess)
        {
        }
        public Rook(char collum, int row, Color color, Chess chess) : base(collum, row, color, chess)
        {
        }
        public override void Move(int row, int collum)
        {
            base.Move(row, collum);
            Moved = true;
        }
        public override Moves[,] PossibleMovements()
        {
            Piece[,] pieces = Chess.Board.Pieces;

            //Mat init
            int rows = pieces.GetLength(0);
            int collums = pieces.GetLength(1);
            Moves[,] possibleMovements = new Moves[rows, collums];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < collums; j++)
                {
                    possibleMovements[i, j] = Moves.None;
                }
            }

            for (double a = 0; a < 2 * Math.PI; a += Math.PI / 2.0)
            {
                int i = Row - (int)Math.Round(Math.Sin(a));
                int j = Collum + (int)Math.Round(Math.Cos(a));
                while (Chess.Board.ValidPos(i, j))
                {
                    if (pieces[i, j] == null) possibleMovements[i, j] = Moves.Move;
                    else if (pieces[i, j].Color != Color)
                    {
                        possibleMovements[i, j] = Moves.Capture;
                        break;
                    }
                    else break;
                    i -= (int)Math.Round(Math.Sin(a));
                    j += (int)Math.Round(Math.Cos(a));
                }
            }

            return possibleMovements;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
