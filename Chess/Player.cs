namespace ChessApi.Chess;

public class Player
{
    public string? Name { get; set; }
    public Color Color { get; set; }

    public Player() {}
    public Player(string name, Color color)
    {
        Name = name;
        Color = color;
    }
}
