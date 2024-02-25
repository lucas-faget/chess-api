namespace ChessApi.Chess;

public class Rook : MobilePiece
{
    public static readonly Coordinates[] RookDirections = [
        Direction.Up,
        Direction.Right,
        Direction.Down,
        Direction.Left
    ];

    public Rook(Color color) : base(color) {
        Directions = RookDirections;
    }

    public override char GetName()
    {
        return PieceName.Rook;
    }
}
