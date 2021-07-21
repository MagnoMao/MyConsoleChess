using MyConsoleChess.BoardNS;
using System;

namespace MyConsoleChess.ChessNS
{
    class King : Piece
    {
        public King(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public override bool[,] PossibleMovements(Piece[,] pieces, Color currentPlayer)
        {
            int rows = pieces.GetLength(0);
            int collums = pieces.GetLength(1);
            bool[,] possibleMovements = new bool[rows, collums];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < collums; j++)
                {
                    possibleMovements[i, j] = false;
                }
            }

            for(int a = 0; a < 360; a += 45)
            {
                int i, j;
                i = Row; j = Collum + 1;
                if(Board.ValidPos(Row, Collum))
                {
                    if(pieces[i,j] == null) 
                }
            }

            return base.PossibleMovements(pieces, currentPlayer);
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
