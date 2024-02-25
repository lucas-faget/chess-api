namespace ChessApi.Chess;

public class Queen : MobilePiece
{
    public static readonly Coordinates[] QueenDirections = King.KingDirections;

    public Queen(Color color) : base(color) {
        Directions = QueenDirections;
    }

    public override char GetName()
    {
        return PieceName.Queen;
    }
}
