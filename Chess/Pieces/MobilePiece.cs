namespace ChessApi.Chess;

public abstract class MobilePiece : Piece
{
    public Coordinates[] Directions = [];

    public MobilePiece() {}
    public MobilePiece(Color color): base(color) {}

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard)
    {
        Moves moves = [];

        foreach (Coordinates direction in Directions)
        {
            Coordinates toPosition = fromPosition;
            toPosition.Move(direction);
            var square = chessboard.GetSquare(toPosition);

            while (square != null)
            {
                if (square.IsEmpty())
                {
                    Move move = new(fromPosition, toPosition);
                    moves.Add(square.Name, move);
                }
                else
                {
                    if (!square.IsOccupiedByColor(Color) && !square.IsOccupiedByPieceName(PieceName.King))
                    {
                        Move move = new(fromPosition, toPosition, square.Piece);
                        moves.Add(square.Name, move);
                    }
                    break;
                }

                toPosition.Move(direction);
                square = chessboard.GetSquare(toPosition);
            }
        }

        return moves;
    }
}
