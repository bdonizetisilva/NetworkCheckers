namespace NetworkCheckers.Jogo
{
    /// <summary>
    /// Status possiveis dos movimentos
    /// </summary>
    public enum MoveStatus
    {
        /// <summary>O movimento é legal</summary>
        Legal,

        /// <summary>O movimento é legal</summary>
        Illegal,

        /// <summary>O movimento é legal mas não completo porque nao foi determinado que há multiplos saltos</summary>
        Incomplete
    }
}
