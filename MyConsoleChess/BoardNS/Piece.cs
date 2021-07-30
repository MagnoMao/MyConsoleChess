using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.BoardNS
{
    abstract class Piece
    {
        public int Collum { get; private set; }
        public int Row { get; private set; }
        public Color Color { get; private set; }
        public Board Board { get; private set; }

        public Piece( int row, int collum, Color color, Board board)
        {
            if (!board.ValidPos(row, collum)) throw new BoardException("Position out of bounds.");
            Collum = collum;
            Row = row;
            Color = color;
            Board = board;
        }

        public Piece(char collum, int row, Color color, Board board)
        {
            Color = color;
            Board = board;
            int[] coords = Board.PosConverter(collum + ""+ row );
            if(coords == null) throw new BoardException("Position out of bounds.");
            Row = coords[0];
            Collum = coords[1];
        }
        public abstract Moves[,] PossibleMovements(Piece[,] pieces);
        public virtual bool MoveToLocation(int row, int collum, Moves[,] possibleMovements)
        {
            if (possibleMovements[row, collum] != Moves.None)
            {
                Row = row;
                Collum = collum;
                return true;
            }
            return false;
        }
    }
}
