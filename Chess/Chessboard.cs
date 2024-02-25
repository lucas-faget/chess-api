using System.Text;

namespace ChessApi.Chess;

public class Chessboard
{
    public static readonly char[] Ranks = ['1','2','3','4','5','6','7','8'];
    public static readonly char[] Files = ['a','b','c','d','e','f','g','h'];
    private readonly Square[,] Squares = new Square[Ranks.Length, Files.Length];

    public Chessboard()
    {
        for (int x = 0; x < Ranks.Length; x++)
        {
            for (int y = 0; y < Files.Length; y++)
            {
                Square square = new($"{Files[y]}{Ranks[x]}");

                if (x == 1 || x == Ranks.Length - 2)
                {
                    square.Piece = new Pawn(x == 1 ? Color.White : Color.Black);
                }
                else if (x == 0 || x == Ranks.Length - 1)
                {
                    Color color = x == 0 ? Color.White : Color.Black;
                    switch (y)
                    {
                        case 0:
                        case 7:
                            square.Piece = new Rook(color);
                            break;
                        case 1:
                        case 6:
                            square.Piece = new Knight(color);
                            break;
                        case 2:
                        case 5:
                            square.Piece = new Bishop(color);
                            break;
                        case 3:
                            square.Piece = new Queen(color);
                            break;
                        case 4:
                            square.Piece = new King(color);
                            break;
                    }
                }

                Squares[x,y] = square;
            }
        }
    }

    public Square? GetSquare(Coordinates position)
    {
        if (position.X >= 0 &&
            position.X < Ranks.Length && 
            position.Y >= 0 &&
            position.Y < Files.Length)
        {
            return Squares[position.X, position.Y];
        }
        else
        {
            return null;
        }
    }

    public Square? GetSquareByDirection(Coordinates position, Coordinates direction)
    {
        if (position.X + direction.X >= 0 &&
            position.X + direction.X < Ranks.Length &&
            position.Y + direction.Y >= 0 &&
            position.Y + direction.Y < Files.Length)
        {
            return Squares[position.X + direction.X, position.Y + direction.Y];
        }
        else
        {
            return null;
        }
    }

    public LegalMoves CalculateLegalMoves(Color color)
    {
        LegalMoves legalMoves = [];

        for (int x = 0; x < Ranks.Length; x++)
        {
            for (int y = 0; y < Files.Length; y++)
            {
                Square square = Squares[x,y];

                if (square.Piece != null && square.Piece.Color == color)
                {
                    Moves moves = square.Piece.GetMoves(new(x,y), this);

                    if (moves.Count > 0)
                    {
                        legalMoves.Add(square.Name, moves);
                    }
                }
            }
        }

        return legalMoves;
    }

    public override string ToString()
    {
        StringBuilder chessboard = new();

        chessboard.AppendLine();
        chessboard.Append("    a  b  c  d  e  f  g  h  ");
        chessboard.AppendLine();
        chessboard.Append("  +------------------------+");
        chessboard.AppendLine();

        for (int x = Ranks.Length - 1; x >= 0; x--)
        {
            chessboard.Append($"{x+1} |");

            for (int y = 0; y < Files.Length; y++)
            {
                chessboard.Append($" {Squares[x,y]} ");
            }

            chessboard.Append($"| {x+1}");
            chessboard.AppendLine();
        }

        chessboard.Append("  +------------------------+");
        chessboard.AppendLine();
        chessboard.Append("    a  b  c  d  e  f  g  h  ");
        chessboard.AppendLine();

        return chessboard.ToString();
    }
}
