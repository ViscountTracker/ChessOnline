using System.Collections.Generic;

namespace Chess
{
    public class Chess
    {
        public string fen { get; private set; }
        Board board;
        Moves moves;
        List<FigureMoving> allMoves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(moves);

        }
        Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(moves);
        }

        public Chess Move(string move)
        {

            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return this;
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {   //todo: 2) Variable name should be completely rewritten f (change the name)
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);//   todo: 3) f is the task of any position(need to change)
            return f == Figure.none ? '.' : (char)f;
        }
        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (FigureOnSquare fs in board.YieldFigures())
                foreach (Square to in Square.YieldsSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        allMoves.Add(fm);
                }
        }
        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach (FigureMoving fm in allMoves)
                list.Add(fm.ToString());
            return list;
        }
    }
}
