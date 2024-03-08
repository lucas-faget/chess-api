namespace ChessApi.Chess;

public class Chess
{
    public static readonly string StandardFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    public static readonly string WhiteKingsideRookSquare = "h1";
    public static readonly string WhiteQueensideRookSquare = "a1";
    public static readonly string BlackKingsideRookSquare = "h8";
    public static readonly string BlackQueensideRookSquare = "a8";
    
    public Chessboard Chessboard { get; set; }
    public Color ActiveColor;
    public string CastlingAvailability;
    public string? EnPassantTarget = null;
    public int HalfmoveClock;
    public int FullmoveNumber;
    public LegalMoves LegalMoves { get; set; } = [];
    public List<Move> SavedMoves { get; set; } = [];

    public Chess(): this(StandardFenString)
    {
    }

    public Chess(string fenString)
    {
        FenRecord fenRecord = FenRecord.FromFenString(fenString);
        Chessboard = new Chessboard(fenRecord.PiecePlacement);
        ActiveColor = fenRecord.ActiveColor.Equals("w") ? Color.White : Color.Black;
        CastlingAvailability = fenRecord.CastlingAvailability;
        EnPassantTarget = fenRecord.EnPassantTarget.Equals("-") ? null : fenRecord.EnPassantTarget;
        HalfmoveClock = int.Parse(fenRecord.HalfmoveClock);
        FullmoveNumber = int.Parse(fenRecord.FullmoveNumber);
        Console.WriteLine(Chessboard);
        SetLegalMoves();
    }

    public void Move(string fromSquareName, string toSquareName)
    {
        if (IsLegalMove(fromSquareName, toSquareName))
        {
            Move move = LegalMoves[fromSquareName][toSquareName];
            SaveMove(move);
        }
        else
        {
            Console.WriteLine("Not available move");
        }
    }

    public void ToggleColor()
    {
        ActiveColor = ActiveColor == Color.White ? Color.Black : Color.White;
    }

    public void SetLegalMoves()
    {
        LegalMoves = [];
        LegalMoves = Chessboard.CalculateLegalMoves(ActiveColor, CastlingAvailability, EnPassantTarget);
    }

    public bool IsLegalMove(string fromSquareName, string toSquareName)
    {
        if (LegalMoves.TryGetValue(fromSquareName, out Moves? moves))
        {
            if (moves.ContainsKey(toSquareName))
            {
                return true;
            }
        }

        return false;
    }

    public void SaveMove(Move move)
    {
        Chessboard.CarryOutMove(move);
        if (move.CastlingAvailability != null)
        {
            CastlingAvailability = move.CastlingAvailability;
        }
        if (move.EnPassantTarget != null)
        {
            EnPassantTarget = move.EnPassantTarget;
        }
        SavedMoves.Add(move);
        Console.WriteLine(Chessboard);
        ToggleColor();
        SetLegalMoves();
    }

    public void DeleteLastMove()
    {
        if (SavedMoves.Count > 0)
        {
            Move lastMove = SavedMoves[SavedMoves.Count - 1];
            Chessboard.UndoMove(lastMove);
            SavedMoves.RemoveAt(SavedMoves.Count - 1);
            if (SavedMoves.Count > 0)
            {
                lastMove = SavedMoves[SavedMoves.Count - 1];
                CastlingAvailability = lastMove.CastlingAvailability ?? CastlingAvailability;
                EnPassantTarget = lastMove.EnPassantTarget;
            }
            else
            {
                CastlingAvailability = "KQkq";
            }
            Console.WriteLine(Chessboard);
            ToggleColor();
            SetLegalMoves();
        }
    }
}
