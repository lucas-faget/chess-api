using System.Text;

namespace ChessApi.Chess;

public class LegalMoves : Dictionary<string, Moves>
{
    public override string ToString()
    {
        StringBuilder legalMoves = new();

        foreach (var moves in this)
        {
            legalMoves.Append($"{moves.Key} =>");
            foreach (var move in moves.Value)
            {
                legalMoves.Append($" {move.Key}");
            }
            legalMoves.AppendLine();
        }

        return legalMoves.ToString();
    }
}
