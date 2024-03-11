namespace ChessApi.Chess;

public class Knight : Piece
{
    public static readonly Coordinates[] KnightDirections = [
        Direction.UpUpRight,
        Direction.UpRightRight,
        Direction.DownRightRight,
        Direction.DownDownRight,
        Direction.DownDownLeft,
        Direction.DownLeftLeft,
        Direction.UpLeftLeft,
        Direction.UpUpLeft
    ];

    public Knight() : base(Color.White)
    {
    }

    public Knight(Color color) : base(color)
    {
    }

    public override char GetName()
    {
        return PieceName.Knight;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, CastlingAvailability castlingAvailability, string? enPassantTarget = null)
    {
        Moves moves = [];

        foreach (Coordinates direction in KnightDirections)
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
