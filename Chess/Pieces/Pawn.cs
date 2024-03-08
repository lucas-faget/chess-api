namespace ChessApi.Chess;

public class Pawn : Piece
{
    public static readonly Dictionary<Color, Coordinates> DirectionByColor = new()
    {
        [Color.White] = Direction.Up,
        [Color.Black] = Direction.Down
    };
    public static readonly Dictionary<Color, Coordinates[]> CaptureDirectionsByColor = new()
    {
        [Color.White] = [Direction.UpLeft, Direction.UpRight],
        [Color.Black] = [Direction.DownLeft, Direction.DownRight]
    };
    public static readonly Coordinates[] EnPassantDirections = [Direction.Left, Direction.Right];

    public Pawn() {}
    public Pawn(Color color) : base(color) {}

    public override char GetName()
    {
        return PieceName.Pawn;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, string castlingAvailability, string? enPassantTarget = null)
    {
        Moves moves = [];
        Coordinates toPosition = fromPosition;

        toPosition.Move(DirectionByColor[Color]);
        var square = chessboard.GetSquare(toPosition);

        if (square?.IsEmpty() ?? false)
        {
            Move move = new(fromPosition, toPosition);
            if (!chessboard.IsCheckedIfMoving(Color, move))
                moves.Add(square.Name, move);

            if (fromPosition.X == (Color == Color.Black ? Chessboard.Ranks.Length - 2 : 1))
            {
                toPosition.Move(DirectionByColor[Color]);
                square = chessboard.GetSquare(toPosition);

                if (square?.IsEmpty() ?? false)
                {
                    Move move2 = new(fromPosition, toPosition)
                    {
                        EnPassantTarget = square.Name
                    };
                    if (!chessboard.IsCheckedIfMoving(Color, move))
                        moves.Add(square.Name, move2);
                }
            }
        }

        if (enPassantTarget != null)
        {
            foreach (Coordinates enPassantDirection in EnPassantDirections)
            {
                Coordinates capturePosition = fromPosition;
                capturePosition.Move(enPassantDirection);
                Square? captureSquare = chessboard.GetSquare(capturePosition);

                if (captureSquare?.Name == enPassantTarget)
                {
                    toPosition = capturePosition;
                    toPosition.Move(DirectionByColor[Color]);
                    square = chessboard.GetSquare(toPosition);

                    if (square?.IsEmpty() ?? false)
                    {
                        Move move = new(fromPosition, toPosition, captureSquare.Piece, capturePosition);
                        if (!chessboard.IsCheckedIfMoving(Color, move))
                            moves.Add(square.Name, move);
                    }
                }
            }
        }

        foreach (Coordinates captureDirection in CaptureDirectionsByColor[Color])
        {
            toPosition = fromPosition;
            toPosition.Move(captureDirection);
            square = chessboard.GetSquare(toPosition);
            
            if (square != null && !square.IsEmpty() && !square.IsOccupiedByColor(Color) && !square.IsOccupiedByPieceName(PieceName.King))
            {
                Move move = new(fromPosition, toPosition, square.Piece);
                if (!chessboard.IsCheckedIfMoving(Color, move))
                    moves.Add(square.Name, move);
            }
        }

        return moves;
    }
}
