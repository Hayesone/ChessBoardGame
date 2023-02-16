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
                theGrid[i, 6].Piece = new Pawn { Colour="White"};
                theGrid[i, 1].Piece = new Pawn { Colour = "Black" };
            }
        }

        public void MarkNextLegalMoves(Cell currentCell, string chessPiece)
        {
            // Check if cell is occupied

            
        }

        
    }
    
}
