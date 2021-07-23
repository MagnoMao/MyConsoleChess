using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Pawn : Piece
    {
        public bool Moved { get; private set; } = false;
        public Pawn(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public override Moves[,] PossibleMovements(Piece[,] pieces, Color currentPlayer)
        {
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

            //Takes into accout if the pawn is moving up (white piece) out moving down (black piece)
            int dir = 0;
            if (Color == Color.White) dir = - 1;  // Pawn is moving up
            else dir = 1;                      // Pawn is moving down


            //Move foward
            var forwardRow = Row + dir;
            if (Board.ValidPos(forwardRow, Collum) && pieces[forwardRow, Collum] == null)
            {
                possibleMovements[Row + dir, Collum] = Moves.Move;
                //Move foward 2 tiles if not moved
                if (!Moved)
                {
                    int twoFowardRow = forwardRow + dir;
                    if (Board.ValidPos(twoFowardRow, Collum) && pieces[twoFowardRow, Collum] == null) possibleMovements[twoFowardRow, Collum] = Moves.Move;
                }
            }

            //Capture diagonally
            int leftCollum = Collum - 1;
            if (Board.ValidPos(forwardRow, leftCollum)) {
                if (pieces[forwardRow, leftCollum] != null && pieces[forwardRow, leftCollum].Color != currentPlayer) possibleMovements[forwardRow, leftCollum] = Moves.Capture;
            }

            int rightCollum = Collum + 1;
            if (Board.ValidPos(forwardRow, rightCollum))
            {
                if (pieces[forwardRow, rightCollum] != null && pieces[forwardRow, rightCollum].Color != currentPlayer) possibleMovements[forwardRow, rightCollum] = Moves.Capture;
            }

            return possibleMovements;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
