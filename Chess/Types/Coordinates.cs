namespace ChessApi.Chess;

public struct Coordinates
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinates()
    {
    }

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(Coordinates direction)
    {
        X += direction.X;
        Y += direction.Y;
    }

    public override readonly string ToString()
    {
        return $"({X},{Y})";
    }
}
