namespace ChessApi.Chess;

public class Bishop : Piece
{
    public static readonly Coordinates[] BishopDirections = [
        Direction.UpRight,
        Direction.DownRight,
        Direction.DownLeft,
        Direction.UpLeft
    ];

    public Bishop() {}
    public Bishop(Color color) : base(color) {}

    public override char GetName()
    {
        return PieceName.Bishop;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, string castlingAvailability, string? enPassantTarget = null)
    {
        Moves moves = [];

        foreach (Coordinates direction in BishopDirections)
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
