namespace ChessApi.Chess;

public class Square
{
    public string Name { get; set; } = null!;
    public Piece? Piece { get; set; }

    public Square()
    {
    }

    public Square(string name, Piece? piece = null)
    {
        Name = name;
        Piece = piece;
    }

    public bool IsEmpty()
    {
        return Piece == null;
    }

    public bool IsOccupiedByColor(Color color)
    {
        return Piece?.Color == color;
    }

    public bool IsOccupiedByPieceName(char name)
    {
        return Piece?.GetName().Equals(name) ?? false;
    }

    public override string ToString()
    {
        return Piece?.ToString() ?? ".";
    }
}
