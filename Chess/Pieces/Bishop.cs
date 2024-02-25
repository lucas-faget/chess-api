namespace ChessApi.Chess;

public class Bishop : MobilePiece
{
    public static readonly Coordinates[] BishopDirections = [
        Direction.UpRight,
        Direction.DownRight,
        Direction.DownLeft,
        Direction.UpLeft
    ];

    public Bishop(Color color) : base(color) {
        Directions = BishopDirections;
    }

    public override char GetName()
    {
        return PieceName.Bishop;
    }
}
