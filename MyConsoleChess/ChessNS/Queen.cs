using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Queen : Piece
    {
        public Queen(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
