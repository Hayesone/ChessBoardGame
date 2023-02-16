using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Board
    {
        // the size of the board 8x8
        public int Size = 8;

        public Cell[,] theGrid { get; set; }

        // constructor
        public Board()
        {
            // create a new 2D array of the type Cell
            theGrid = new Cell[Size, Size];

            // fill the Cell 2D array with the Cell obj
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }

            SetUpBoard(theGrid);
        }

        private void SetUpBoard(Cell[,] b)
        {
            // Pawns
            for (int i = 0; i < Size; i++)
            {
                theGrid[i, 6].Piece = new Pawn { Colour = "White" };

                theGrid[i, 1].Piece = new Pawn { Colour = "Black" };
            }
        }

        public void MarkNextLegalMoves(Cell currentCell)
        {
            // Check legal moves for that piece. (Not to its own colour or out of bounds)
            if (currentCell.Piece is not null)
            {
                var list = currentCell.Piece.GetMoves();
                for (int i = 0; i < list.Count; i++)
                {
                    Tuple<int, int> move = list[i];

                    try
                    {
                        // TODO: Fix this so that the code isnt redundent.
                        if (currentCell.Piece.Colour == "White")
                        {
                            Cell destination = theGrid[currentCell.RowRank + -move.Item1, currentCell.ColumnFile + -move.Item2];

                            if (destination.Piece is not null && destination.Piece.Colour == currentCell.Piece.Colour)
                            {
                                continue;
                            }
                            else
                            {
                                destination.LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Cell destination = theGrid[currentCell.RowRank + move.Item1, currentCell.ColumnFile + move.Item2];

                            if (destination.Piece is not null && destination.Piece.Colour == currentCell.Piece.Colour)
                            {
                                continue;
                            }
                            else
                            {
                                destination.LegalNextMove = true;
                            }
                        }


                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }

                }
            }

        }

        public void LegalMove(Cell currentCell)
        {
            if (!currentCell.LegalNextMove)
            {
                return;
            } else
            {
                return;
                // TODO: Needs to swap Obj Piece from old to new cell.
                // TODO: If Piece is taken, show on the side of the game the Piece taken.
            }
        }
    }
    
}
