namespace ChessApi.Chess;

public class FenRecord
{
    public string PiecePlacement;
    public string ActiveColor;
    public string CastlingAvailability;
    public string EnPassantTarget;
    public string HalfmoveClock;
    public string FullmoveNumber;

    public FenRecord(string fenString)
    {
        string[] fen = fenString.Split(' ');

        PiecePlacement = fen[0];
        ActiveColor = fen[1];
        CastlingAvailability = fen[2];
        EnPassantTarget = fen[3];
        HalfmoveClock = fen[4];
        FullmoveNumber = fen[5];
    }
}
