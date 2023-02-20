using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
     class Moves
    {
        FigureMoving fm;
        Board board;
        private Moves moves;

        public Moves( Board board)
        {
            this.board = board;
        }

        public Moves(Moves moves)
        {
            this.moves = moves;
        }

        public bool CanMove(FigureMoving fm) 
        {
            this.fm = fm;
            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }
        
        bool CanMoveFrom() //todo: 1) How to fix - 'Object reference not set to an instance of an object.'
        {
            return fm.from.OnBoard() &&
                   fm.figure.GetColor() == board.moveColor;
        }
        
        bool CanMoveTo() 
        {
            return fm.to.OnBoard() &&
                   fm.from != fm.to &&
                   board.GetFigureAt(fm.to).GetColor() != board.moveColor;
        }
        bool CanFigureMove() 
        {
            switch (fm.figure)
            {
                
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return false;

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return false;

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return false;   //return CanKhightMove(); 

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return false;

                default: return false;
            }
        }
        private bool CanKingMove() 
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1)
                return true;
            return false;
        }

        public bool CanStraightMove() 
        {
            Square at =fm.from;
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.to)
                    return true;
            } while (at.OnBoard() && 
                board.GetFigureAt(at) == Figure.none);
            return false;
        }
    }
}
