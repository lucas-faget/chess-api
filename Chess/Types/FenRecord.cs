namespace ChessApi.Chess;

public class FenRecord
{
    public string PiecePlacement { get; set; } = null!;
    public string ActiveColor { get; set; } = null!;
    public string CastlingAvailability { get; set; } = null!;
    public string EnPassantTarget { get; set; } = null!;
    public string HalfmoveClock { get; set; } = null!;
    public string FullmoveNumber { get; set; } = null!;

    public static FenRecord FromFenString(string fenString)
    {
        string[] fen = fenString.Split(' ');

        return new()
        {
            PiecePlacement = fen[0],
            ActiveColor = fen[1],
            CastlingAvailability = fen[2],
            EnPassantTarget = fen[3],
            HalfmoveClock = fen[4],
            FullmoveNumber = fen[5]
        };
    }

    public string ToFenString()
    {
        return $"{PiecePlacement} {ActiveColor} {CastlingAvailability} {EnPassantTarget} {HalfmoveClock} {FullmoveNumber}";
    }
}
