namespace ChessApi.Chess;

public class Chess
{
    public static readonly string StandardFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    public Chessboard Chessboard { get; set; }

    public Player[] Players { get; set; } = [new(Color.White), new(Color.Black)];
    public int ActivePlayerIndex { get; set; }
    public string? EnPassantTarget { get; set; } = null;
    public int HalfmoveClock { get; set; }
    public int FullmoveNumber { get; set; }
    public LegalMoves LegalMoves { get; set; } = [];
    public List<Move> MoveHistory { get; set; } = [];

    public Chess(): this(StandardFenString)
    {
    }

    public Chess(string fenString)
    {
        FenRecord fenRecord = FenRecord.FromFenString(fenString);
        Chessboard = new Chessboard(fenRecord.PiecePlacement);
        ActivePlayerIndex = fenRecord.ActiveColor.Equals("w") ? 0 : 1;
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

    public void SwitchToNextPlayer()
    {
        ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;
    }

    public void SwitchToPreviousPlayer()
    {
        ActivePlayerIndex = (ActivePlayerIndex - 1) % Players.Length;
    }

    public void SetLegalMoves()
    {
        LegalMoves = [];
        LegalMoves = Chessboard.CalculateLegalMoves(Players[ActivePlayerIndex], EnPassantTarget);
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
        if (move.EnPassantTarget != null)
        {
            EnPassantTarget = move.EnPassantTarget;
        }
        MoveHistory.Add(move);
        Console.WriteLine(Chessboard);
        SwitchToNextPlayer();
        SetLegalMoves();
    }

    public void DeleteLastMove()
    {
        if (MoveHistory.Count > 0)
        {
            Move lastMove = MoveHistory[MoveHistory.Count - 1];
            Chessboard.UndoMove(lastMove);
            MoveHistory.RemoveAt(MoveHistory.Count - 1);
            if (MoveHistory.Count > 0)
            {
                lastMove = MoveHistory[MoveHistory.Count - 1];
                EnPassantTarget = lastMove.EnPassantTarget;
            }
            Console.WriteLine(Chessboard);
            SwitchToPreviousPlayer();
            SetLegalMoves();
        }
    }
}
