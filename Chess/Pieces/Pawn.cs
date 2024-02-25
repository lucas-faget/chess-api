namespace ChessApi.Chess;

public class Pawn : Piece
{
    public static readonly Dictionary<Color, Coordinates> DirectionByColor = new()
    {
        [Color.White] = Direction.Up,
        [Color.Black] = Direction.Down
    };

    public Pawn() {}
    public Pawn(Color color) : base(color) {}

    public override char GetName()
    {
        return PieceName.Pawn;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard)
    {
        Moves moves = [];
        Coordinates toPosition = fromPosition;

        toPosition.Move(DirectionByColor[Color]);
        var square = chessboard.GetSquare(toPosition);

        if (square?.IsEmpty() ?? false)
        {
            Move move = new(fromPosition, DirectionByColor[Color]);
            moves.Add(square.Name, move);

            if (fromPosition.X == (Color == Color.Black ? Chessboard.Ranks.Length - 2 : 1))
            {
                toPosition.Move(DirectionByColor[Color]);
                square = chessboard.GetSquare(toPosition);

                if (square?.IsEmpty() ?? false)
                {
                    Move move2 = new(fromPosition, toPosition);
                    moves.Add(square.Name, move2);
                }
            }
        }

        return moves;
    }
}