namespace ChessApi.Chess;

public static class Direction
{
    public static readonly Coordinates Up = new(1, 0);
    public static readonly Coordinates UpUpRight = new(2, 1);
    public static readonly Coordinates UpRightRight = new(1, 2);
    public static readonly Coordinates UpRight = new(1, 1);
    public static readonly Coordinates Right = new(0, 1);
    public static readonly Coordinates DownRightRight = new(-1, 2);
    public static readonly Coordinates DownRight = new(-1, 1);
    public static readonly Coordinates DownDownRight = new(-2, 1);
    public static readonly Coordinates Down = new(-1, 0);
    public static readonly Coordinates DownDownLeft = new(-2, -1);
    public static readonly Coordinates DownLeft = new(-1, -1);
    public static readonly Coordinates DownLeftLeft = new(-1, -2);
    public static readonly Coordinates Left = new(0, -1);
    public static readonly Coordinates UpLeftLeft = new(1, -2);
    public static readonly Coordinates UpLeft = new(1, -1);
    public static readonly Coordinates UpUpLeft = new(2, -1);
}
