namespace NetworkCheckers.Jogo
{
    /// <summary>
    /// The various pieces
    /// </summary>
    public enum Piece
    {
        /// <summary>
        /// This indicates an invalid piece.  i.e. Invalid square
        /// </summary>
        Illegal,

        /// <summary>
        /// This indicates that the square is empty and has no piece
        /// </summary>
        None,

        /// <summary>
        /// Black man piece
        /// </summary>
        Black,

        /// <summary>
        /// White man piece
        /// </summary>
        White,

        /// <summary>
        /// Black king piece
        /// </summary>
        BlackKing,

        /// <summary>
        /// White king piece
        /// </summary>
        WhiteKing
    }
}
