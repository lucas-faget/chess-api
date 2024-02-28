namespace ChessApi.Chess;

public class Chess
{
    public Player[] Players = [
        new("Whites", Color.White),
        new("Blacks", Color.Black)
    ];

    public int CurrentPlayerIndex = 0;
    public Chessboard Chessboard { get; set; }
    public LegalMoves LegalMoves { get; set; } = [];
    public List<Move> SavedMoves { get; set; } = [];

    public Chess()
    {
        Chessboard = new Chessboard();
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
        LegalMoves = Chessboard.CalculateLegalMoves(Players[CurrentPlayerIndex].Color);
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
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Length;
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
            CurrentPlayerIndex = (CurrentPlayerIndex - 1) % Players.Length;
            SetLegalMoves();
        }
    }
}
