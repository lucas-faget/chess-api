namespace ChessApi.Chess;

public static class Castling
{
    public static readonly Side[] Sides = [Side.Kingside, Side.Queenside];
    public const int KingsideCastlingSpaces = 2;
    public const int QueensideCastlingSpaces = 3;
    public const string WhiteKingsideAvailability = "K";
    public const string WhiteQueensideAvailability = "Q";
    public const string BlackKingsideAvailability = "k";
    public const string BlackQueensideAvailability = "q";
}
