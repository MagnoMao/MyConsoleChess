using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;
using System;

namespace MyConsoleChess.ChessNS
{
    class King : Piece
    {
        public King(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public King(char collum, int row, Color color, Board board) : base(collum, row, color, board)
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
            
            //Check every Tile near the king
            for (double a = 0; a < 2 * Math.PI; a += Math.PI / 4.0)
            {
                int i = Row - (int)Math.Round(Math.Sin(a));
                int j = Collum + (int)Math.Round(Math.Cos(a));
                if (!Board.ValidPos(i, j)) continue;
                if (pieces[i, j] == null) possibleMovements[i, j] = Moves.Move;
                else if(pieces[i,j].Color == Color.Black) possibleMovements[i, j] = Moves.Capture;
            }
            //Right
            //if (Board.ValidPos(Row, Collum + 1))
            //{
            //    if (pieces[Row, Collum + 1] == null) possibleMovements[Row, Collum + 1] = Moves.Move;
            //    else if(pieces[Row,Collum + 1].Color == Color.Black) possibleMovements[Row, Collum + 1] = Moves.Capture;
            //}

            return possibleMovements;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
