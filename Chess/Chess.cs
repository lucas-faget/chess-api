namespace ChessApi.Chess;

public class Chess
{
    public static readonly string StandardFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public Player[] Players = [
        new("Whites", Color.White),
        new("Blacks", Color.Black)
    ];
    public int ActivePlayerIndex = 0;
    public Chessboard Chessboard { get; set; }
    public LegalMoves LegalMoves { get; set; } = [];
    public List<Move> SavedMoves { get; set; } = [];

    public Chess()
    {
        FenRecord fenRecord = new(StandardFenString);
        Chessboard = new Chessboard(fenRecord.PiecePlacement);
        Console.WriteLine(Chessboard);
        SetLegalMoves();
    }

    public Chess(string fenString)
    {
        FenRecord fenRecord = new(fenString);
        Chessboard = new Chessboard(fenRecord.PiecePlacement);
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
    }

    public void SetLegalMoves()
    {
        LegalMoves = [];
        LegalMoves = Chessboard.CalculateLegalMoves(Players[ActivePlayerIndex].Color);
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
        SavedMoves.Add(move);
        Console.WriteLine(Chessboard);
        ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;
        SetLegalMoves();
    }

    public void DeleteLastMove()
    {
        if (SavedMoves.Count > 0)
        {
            Move lastMove = SavedMoves[SavedMoves.Count - 1];
            Chessboard.UndoMove(lastMove);
            SavedMoves.RemoveAt(SavedMoves.Count - 1);
            Console.WriteLine(Chessboard);
            ActivePlayerIndex = (ActivePlayerIndex - 1) % Players.Length;
            SetLegalMoves();
        }
    }
}
