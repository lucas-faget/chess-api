namespace ChessApi.Chess;

public class King : Piece
{
    public static readonly Coordinates[] KingDirections = [.. Bishop.BishopDirections, .. Rook.RookDirections];

    public King() {}
    public King(Color color) : base(color) {}

    public override char GetName()
    {
        return PieceName.King;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard)
    {
        Moves moves = [];

        foreach (Coordinates direction in KingDirections)
        {
            Coordinates toPosition = fromPosition;
            toPosition.Move(direction);
            var square = chessboard.GetSquare(toPosition);

            if (square != null && (square.IsEmpty() || (!square.IsOccupiedByColor(Color) && !square.IsOccupiedByPieceName(PieceName.King))))
            {
                Move move = new(fromPosition, toPosition, square.Piece);
                if (!chessboard.IsCheckedIfMoving(Color, move))
                    moves.Add(square.Name, move);
            }
        }

        return moves;
    }
}
