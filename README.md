# ChessBoardClassicApp

This is a project aiming to build Chess in C# using WinForms.

## Requirements

* Have a 8x8 Chess board.
* Display piece(s) on a the board.
* Move a piece on the board.
* Needs to follow the [rules](https://en.wikipedia.org/wiki/Rules_of_chess) of Chess.

### Gameplay
* Inital setup
#### Movement
* Basic movement:
    - King
    - Rook
    - Bishop
    - Queen
    - Knight
    - Pawn
* Castling
* En passant
* Promotion
#### Check
* If king is in check disable the movement of other pieces, unless it removes the check condition.
#### End of game
* Checkmate
* Resigning
* Draws
* Dead position
* Flag-fall (only to be implemented if timing is added)

### Desirables
* Support [algebraic Chess notation](https://en.wikipedia.org/wiki/Algebraic_chess_notation) and display moves on side of screen.
* Add support for [Portable Game Notation](https://en.wikipedia.org/wiki/Portable_Game_Notation) (PGN)