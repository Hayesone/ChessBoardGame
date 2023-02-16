using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public abstract class Piece
    {
        public string Name {get; set;}
        // What Team the Piece is on white or black
        public string Colour { get; set; }

        // Removed location
        //

        // Has Piece moved (for castling) need to check whether other piece to castle with has also not moved, and that king is not in check after move
        public bool HasMoved { get; set; } = false;

        // Can the Piece promote
        public bool CanPromote { get; set; } = false;

        // Input Board, Cell: Calculate the moves for that piece on the board and set valid moves to Legal
        public abstract List<Tuple<int, int>> GetMoves();


        public void SetCurrentCell(Cell newCell)
        {
            // Need to perform checks before this can be run (king not in check by move)
            //CurrentCell.CurrentlyOccupied = false;
            //CurrentCell = newCell;
            //CurrentCell.CurrentlyOccupied = true;

        }
        
    }

    public class Pawn : Piece
    {
        
        // TODO: Need to set this to be TRUE when pawn moves two sqaures on the first move, then false on its move after.
        public bool EnPassantible { get; set; } = false;
        public Pawn()
        {
            Name = "Pawn";
            CanPromote = true;
        }


        // Take the currentCell Row and Column, and return only the moves that are within the Board.
        public override List<Tuple<int, int>> GetMoves()
        {
            // TODO: Handle Black moves, convert transform vectors to negatives.
            var listMoves = new List<Tuple<int, int>>();

            // Pawn can move one or two squres on inital move, and one thereafter. Or, take on diagonal directly or using en-passant
            // Move forward one Rank
            listMoves.Add(new Tuple<int, int>(1, 0));

            // Move forward two Ranks, set as able to be en-passant
            // TODO: Check if this move if done, if done set EnPassantible = true;
            listMoves.Add(new Tuple<int, int>(2, 0));

            // Capture on diagonal
            listMoves.Add(new Tuple<int, int>(1, 1));
            listMoves.Add(new Tuple<int, int>(1, -1));

            return listMoves;
        }

        
    }


}
