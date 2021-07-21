using MyConsoleChess.BoardNS;

namespace MyConsoleChess.ChessNS
{
    class Pawn : Piece
    {
        public bool Moved { get; private set; } = false;
        public Pawn(int row,int collum, Color color, Board board) : base(row, collum, color, board)
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

            int dir = 0;
            if (Color == Color.White) dir = - 1;  // Pawn is moving up
            else dir = 1;                      // Pawn is moving down
            //Move foward
            if (Row + dir >= 0 && pieces[Row + dir, Collum] == null)
            {
                possibleMovements[Row + dir, Collum] = true;
                //Move foward 2 tiles if not moved
                if (!Moved)
                {
                    if (Row + 2 * dir >= 0 && pieces[Row + 2 * dir, Collum] == null) possibleMovements[Row + 2 * dir, Collum] = true;
                }
            }
            
            //Move foward diagonally if there is a piece to capture
            if (Row + dir >= 0 && Collum - 1 >=0 
                && pieces[Row + dir, Collum - 1] != null && pieces[Row + dir, Collum - 1].Color != currentPlayer) possibleMovements[Row + dir, Collum - 1] = true;
            if (Row + dir >= 0 && Collum + 1 < collums 
                && pieces[Row + dir, Collum + 1] != null && pieces[Row + dir, Collum + 1].Color != currentPlayer) possibleMovements[Row + dir, Collum + 1] = true;

            return possibleMovements;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
