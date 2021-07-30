using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Queen : Piece
    {
        public Queen(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }
        public Queen(char collum, int row, Color color, Board board) : base(collum, row, color, board)
        {
        }
        public override Moves[,] PossibleMovements(Piece[,] pieces)
        {
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

            for (double a = 0; a < 2 * Math.PI; a += Math.PI / 4.0)
            {
                int i = Row - (int)Math.Round(Math.Sin(a));
                int j = Collum + (int)Math.Round(Math.Cos(a));
                while (Board.ValidPos(i, j))
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
            return "Q";
        }
    }
}
