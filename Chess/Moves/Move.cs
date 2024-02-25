namespace ChessApi.Chess;

public class Move
{
    public Coordinates FromPosition { get; set; }
    public Coordinates ToDirection { get; set; }
    public Coordinates CapturePosition { get; set; }
    public Piece? CapturedPiece { get; set; }
    public Move? RookMove { get; set; }

    public Move() {}
    public Move(Coordinates fromPosition, Coordinates toPosition, Piece? capturedPiece = null, Coordinates? capturePosition = null, Move? rookMove = null)
    {
        FromPosition = fromPosition;
        ToDirection = toPosition;
        CapturedPiece = capturedPiece;
        CapturePosition = capturePosition ?? toPosition;
        RookMove = rookMove;
    }
}