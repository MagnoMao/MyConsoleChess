using System;

namespace MyConsoleChess.BoardNS
{
    class BoardException :  Exception
    {
        public BoardException(String message) : base(message)
        {

        }
    }
}
