namespace ChessApi.Chess;

public class Chess
{
    public Chessboard Chessboard { get; set; }
    public LegalMoves LegalMoves { get; set; } = [];

    public Chess()
    {
        Chessboard = new Chessboard();
        Console.WriteLine(Chessboard);
        SetLegalMoves();
        Console.WriteLine(LegalMoves);
    }

    public void SetLegalMoves()
    {
        LegalMoves = Chessboard.CalculateLegalMoves(Color.White);
    }
}
