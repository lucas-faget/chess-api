namespace ChessApi.Chess;

public class Player
{
    public Color Color { get; }
    public CastlingAvailability CastlingAvailability { get; set; }

    public Player()
    {
        Color = Color.White;
        CastlingAvailability = new();
    }

    public Player(Color color)
    {
        Color = color;
        CastlingAvailability = new();
    }
}
