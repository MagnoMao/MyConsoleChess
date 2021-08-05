using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;
using MyConsoleChess.ChessNS;

namespace MyConsoleChess.BoardNS
{
    abstract class Piece
    {
        public int Collum { get; private set; }
        public int Row { get; private set; }
        public Color Color { get; private set; }
        public Chess Chess { get; private set; }

        public Piece( int row, int collum, Color color, Chess chess)
        {
            Chess = chess;
            if (!Chess.Board.ValidPos(row, collum)) throw new BoardException("Position out of bounds.");
            Collum = collum;
            Row = row;
            Color = color;
        }

        public Piece(char collum, int row, Color color, Chess chess)
        {
            Chess = chess;
            Color = color;
            int[] coords = Chess.Board.PosConverter(collum + ""+ row );
            if(coords == null) throw new BoardException("Position out of bounds.");
            Row = coords[0];
            Collum = coords[1];
        }
        /// <summary>
        /// Update the piece coordinates
        /// </summary>
        /// <param name="row"></param>
        /// <param name="collum"></param>
        public virtual void Move(int row, int collum)
        {
            Row = row;
            Collum = collum;
        }
        public abstract Moves[,] PossibleMovements();        
    }
}
