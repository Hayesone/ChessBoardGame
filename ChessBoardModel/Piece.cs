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

        // Has Piece moved (for castling) need to check whether other piece to castle with has also not moved, and that king is not in check after move
        public bool HasMoved { get; set; } = false;

        // Can the Piece promote
        public bool CanPromote { get; set; } = false;

        // Returns how that Piece can move.
        public abstract Dictionary<string, Tuple<int, int>> GetMoves();
        
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

        // Returns moves a Pawn can do.
        public override Dictionary<string, Tuple<int, int>> GetMoves()
        {
            Dictionary<string, Tuple<int, int>> movesDict = new Dictionary<string, Tuple<int, int>>()
            {
                {"forwardOne", new Tuple<int, int>(0, 1)},
                {"captureLeft", new Tuple<int, int>(1, 1)},
                {"captureRight", new Tuple<int, int>(-1, 1)},
            };

            if (!HasMoved) 
            {
                // TODO: Move this being set in Board instead. Falsely set to true if you click on a Piece then click to empty Cell, should only be set if it is moved.
                HasMoved = true;
                // TODO: In Board, handle when a Piece is blocking this pawn, and disable the option to "forwardTwo"
                movesDict.Add("forwardTwo", new Tuple<int, int>(0, 2));
                return movesDict;
            }

            return movesDict;
        }

        
    }


}
