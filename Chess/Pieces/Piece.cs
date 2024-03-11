namespace ChessApi.Chess;

public abstract class Piece
{
    public Color Color { get; set; }

    public Piece() => Color = Color.White;
    public Piece(Color color) => Color = color;

    public abstract char GetName();

    public abstract Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, CastlingAvailability castlingAvailability, string? enPassantTarget = null);

    public override string ToString()
    {
        return Color == Color.White ? GetName().ToString().ToUpper() : GetName().ToString();
    }
}
