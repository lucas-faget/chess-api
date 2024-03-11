namespace ChessApi.Chess;

public class Move
{
    public Coordinates FromPosition { get; set; }
    public Coordinates ToPosition { get; set; }
    public Coordinates CapturePosition { get; set; }
    public Piece? CapturedPiece { get; set; }
    public Move? RookMove { get; set; } = null;
    public string? EnPassantTarget = null;

    public Move()
    {
    }

    public Move(Coordinates fromPosition, Coordinates toPosition, Piece? capturedPiece = null, Coordinates? capturePosition = null)
    {
        FromPosition = fromPosition;
        ToPosition = toPosition;
        CapturedPiece = capturedPiece;
        CapturePosition = capturePosition ?? toPosition;
    }
}
