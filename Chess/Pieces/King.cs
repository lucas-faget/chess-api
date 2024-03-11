namespace ChessApi.Chess;

public class King : Piece
{
    public static readonly Coordinates[] KingDirections = [.. Bishop.BishopDirections, .. Rook.RookDirections];

    public King() : base(Color.White)
    {
    }

    public King(Color color) : base(color)
    {
    }

    public override char GetName()
    {
        return PieceName.King;
    }

    public override Moves GetMoves(Coordinates fromPosition, Chessboard chessboard, CastlingAvailability castlingAvailability, string? enPassantTarget = null)
    {
        Moves moves = [];

        foreach (Coordinates direction in KingDirections)
        {
            Coordinates toPosition = fromPosition;
            toPosition.Move(direction);
            var square = chessboard.GetSquare(toPosition);

            if (square != null && (square.IsEmpty() || (!square.IsOccupiedByColor(Color) && !square.IsOccupiedByPieceName(PieceName.King))))
            {
                Move move = new(fromPosition, toPosition, square.Piece);
                if (!chessboard.IsCheckedIfMoving(Color, move))
                {
                    moves.Add(square.Name, move);
                }
            }
        }

        foreach (Side side in Castling.Sides)
        {
            Move? move = GetCastlingMove(fromPosition, chessboard, castlingAvailability, side);
            if (move != null)
            {
                var square = chessboard.GetSquare(move.ToPosition);
                if (square != null)
                {
                    moves.Add(square.Name, move);
                }
            }
        }

        return moves;
    }

    public Move? GetCastlingMove(Coordinates fromPosition, Chessboard chessboard, CastlingAvailability castlingAvailability, Side side)
    {
        if (side == Side.Kingside && castlingAvailability.Kingside ||
            side == Side.Queenside && castlingAvailability.Queenside)
        {
            int spaces = side == Side.Kingside ? Castling.KingsideCastlingSpaces : Castling.QueensideCastlingSpaces;
            Coordinates rookPosition = fromPosition;
            Coordinates direction = side == Side.Kingside ? Direction.Right : Direction.Left;
            Square? square;

            for (int i = 0; i < spaces; i++)
            {
                rookPosition.Move(direction);
                square = chessboard.GetSquare(rookPosition);

                if (square == null || !square.IsEmpty())
                {
                    return null;
                }
            }

            rookPosition.Move(direction);
            square = chessboard.GetSquare(rookPosition);

            if (square != null && square.IsOccupiedByPieceName(PieceName.Rook) && square.IsOccupiedByColor(Color))
            {
                Coordinates toPosition = fromPosition;
                Move? move = null;
                
                for (int i = 0; i < 2; i++)
                {
                    toPosition.Move(direction);
                    move = new(fromPosition, toPosition);

                    if (chessboard.IsCheckedIfMoving(Color, move))
                    {
                        return null;
                    }
                }

                square = chessboard.GetSquare(toPosition);

                if (move != null && square != null)
                {
                    toPosition = fromPosition;
                    toPosition.Move(direction);
                    move.RookMove = new(rookPosition, toPosition);

                    return move;
                }
            }
        }

        return null;
    }
}
