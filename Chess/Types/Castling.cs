namespace ChessApi.Chess;

public static class Castling
{
    public static readonly Side[] Sides = [Side.Kingside, Side.Queenside];
    public const string WhiteKingsideAvailability = "K";
    public const string WhiteQueensideAvailability = "Q";
    public const string BlackKingsideAvailability = "k";
    public const string BlackQueensideAvailability = "q";

    public static bool IsCastlingAvailable(string castlingAvailability, Color color, Side side)
    {
        return castlingAvailability.Contains(
            color == Color.White ?
                (side == Side.Kingside ? WhiteKingsideAvailability : WhiteQueensideAvailability) :
                (side == Side.Kingside ? BlackKingsideAvailability : BlackQueensideAvailability));
    }
}
