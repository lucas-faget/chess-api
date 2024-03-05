using System.Text;

namespace ChessApi.Chess;

public class Chessboard
{
    public static readonly char[] Ranks = ['1','2','3','4','5','6','7','8'];
    public static readonly char[] Files = ['a','b','c','d','e','f','g','h'];
    private readonly Square[,] Squares = new Square[Ranks.Length, Files.Length];

    public Chessboard(string PiecePlacement)
    {
        string[] rows = PiecePlacement.Split('/');

        for (int x = 0; x < Ranks.Length; x++)
        {
            int y = 0;
            foreach (char c in rows[Ranks.Length - 1 - x])
            {
                if (char.IsDigit(c))
                {
                    int spaces = (int)char.GetNumericValue(c);
                    for (int i = 0; i < spaces; i++)
                    {
                        Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}");
                        y++;
                    }
                }
                else
                {
                    Color color = char.IsUpper(c) ? Color.White : Color.Black;
                    switch (char.ToLower(c))
                    {
                        case 'p':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new Pawn(color));
                            y++;
                            break;
                        case 'n':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new Knight(color));
                            y++;
                            break;
                        case 'b':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new Bishop(color));
                            y++;
                            break;
                        case 'r':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new Rook(color));
                            y++;
                            break;
                        case 'q':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new Queen(color));
                            y++;
                            break;
                        case 'k':
                            Squares[x, y] = new Square($"{Files[y]}{Ranks[x]}", new King(color));
                            y++;
                            break;
                    }
                }
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

    public LegalMoves CalculateLegalMoves(Color color, string? enPassantTarget = null)
    {
        LegalMoves legalMoves = [];

        for (int x = 0; x < Ranks.Length; x++)
        {
            for (int y = 0; y < Files.Length; y++)
            {
                Square square = Squares[x,y];

                if (square.Piece != null && square.Piece.Color == color)
                {
                    Moves moves = square.Piece.GetMoves(new(x,y), this, enPassantTarget);

                    if (moves.Count > 0)
                    {
                        legalMoves.Add(square.Name, moves);
                    }
                }
            }
        }

        return legalMoves;
    }

    public void CarryOutMove(Move move)
    {
        var fromSquare = GetSquare(move.FromPosition);
        var captureSquare = GetSquare(move.CapturePosition);
        var toSquare = GetSquare(move.ToPosition);

        if (fromSquare != null && toSquare != null && captureSquare != null)
        {
            captureSquare.Piece = null;
            toSquare.Piece = fromSquare.Piece;
            fromSquare.Piece = null;

            if (move.RookMove != null)
            {
                CarryOutMove(move.RookMove);
            }
        }
    }

    public void UndoMove(Move move)
    {
        var fromSquare = GetSquare(move.FromPosition);
        var captureSquare = GetSquare(move.CapturePosition);
        var toSquare = GetSquare(move.ToPosition);

        if (fromSquare != null && toSquare != null && captureSquare != null)
        {
            if (move.RookMove != null)
            {
                UndoMove(move.RookMove);
            }

            fromSquare.Piece = toSquare.Piece;
            toSquare.Piece = null;
            captureSquare.Piece = move.CapturedPiece;
        }
    }

    public bool IsCheckedIfMoving(Color color, Move move)
    {
        CarryOutMove(move);
        bool isChecked = IsChecked(color);
        UndoMove(move);

        return isChecked;
    }

    public Coordinates? FindKingPosition(Color color)
    {
        for (int x = 0; x < Ranks.Length; x++)
        {
            for (int y = 0; y < Files.Length; y++)
            {
                Square square = Squares[x,y];

                if (square.Piece != null && square.Piece.GetName().Equals(PieceName.King) && square.Piece.Color == color)
                {
                    return new(x,y);
                }
            }
        }

        return null;
    }

    public bool IsChecked(Color color)
    {
        bool isChecked = false;

        var kingPosition = FindKingPosition(color);

        if (kingPosition.HasValue)
        {
            return IsCheckedByPawn(color, kingPosition.Value)
                || IsCheckedByKnight(color, kingPosition.Value)
                || IsCheckedByBishopOrRookOrQueenOrKing(color, kingPosition.Value);
        }

        return isChecked;
    }

    public bool IsCheckedByPawn(Color color, Coordinates kingPosition)
    {
        foreach (Coordinates direction in Bishop.BishopDirections)
        {
            if (direction.X == Pawn.DirectionByColor[color].X || direction.Y == Pawn.DirectionByColor[color].Y)
            {
                var square = GetSquareByDirection(kingPosition, direction);
                if (square != null && square.IsOccupiedByPieceName(PieceName.Pawn) && !square.IsOccupiedByColor(color))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsCheckedByKnight(Color color, Coordinates kingPosition)
    {
        foreach (Coordinates direction in Knight.KnightDirections)
        {
            var square = GetSquareByDirection(kingPosition, direction);
            if (square != null && square.IsOccupiedByPieceName(PieceName.Knight) && !square.IsOccupiedByColor(color))
            {
                return true;
            }

        }

        return false;
    }

    public bool IsCheckedByBishopOrRookOrQueenOrKing(Color color, Coordinates kingPosition)
    {
        foreach (Coordinates direction in Queen.QueenDirections)
        {
            Coordinates position = kingPosition;
            position.Move(direction);
            var square = GetSquare(position);
            bool isKingCheckDoable = true;

            while (square != null)
            {
                if (!square.IsEmpty())
                {
                    if (!square.IsOccupiedByColor(color))
                    {
                        if (Bishop.BishopDirections.Contains(direction))
                        {
                            if (square.IsOccupiedByPieceName(PieceName.Bishop) ||
                                square.IsOccupiedByPieceName(PieceName.Queen) ||
                                (square.IsOccupiedByPieceName(PieceName.King) && isKingCheckDoable))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (square.IsOccupiedByPieceName(PieceName.Rook) ||
                                square.IsOccupiedByPieceName(PieceName.Queen) ||
                                (square.IsOccupiedByPieceName(PieceName.King) && isKingCheckDoable))
                            {
                                return true;
                            }
                        }
                    }
                    break;
                }

                isKingCheckDoable = false;
                position.Move(direction);
                square = GetSquare(position);
            }
        }

        return false;
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
