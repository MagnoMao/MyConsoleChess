using MyConsoleChess.BoardNS;

namespace MyConsoleChess.ChessNS
{
    class Tower : Piece
    {
        public Tower(int collum, int row, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
