namespace ChessApi.Chess;

public class Rook : Piece
{
    public static readonly Coordinates[] RookDirections = [
        Direction.Up,
        Direction.Right,
        Direction.Down,
        Direction.Left
    ];

    public Rook() : base(Color.White)
    {
    }

    public Rook(Color color) : base(color)
    {
    }

    public override char GetName()
    {
        return PieceName.Rook;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, CastlingAvailability castlingAvailability, string? enPassantTarget = null)
    {
        Moves moves = [];

        foreach (Coordinates direction in RookDirections)
        {
            Coordinates toPosition = fromPosition;
            toPosition.Move(direction);
            var square = chessboard.GetSquare(toPosition);

            while (square != null)
            {
                if (square.IsEmpty())
                {
                    Move move = new(fromPosition, toPosition);
                    if (!chessboard.IsCheckedIfMoving(Color, move))
                        moves.Add(square.Name, move);
                }
                else
                {
                    if (!square.IsOccupiedByColor(Color) && !square.IsOccupiedByPieceName(PieceName.King))
                    {
                        Move move = new(fromPosition, toPosition, square.Piece);
                        if (!chessboard.IsCheckedIfMoving(Color, move))
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
