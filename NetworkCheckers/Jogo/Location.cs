using System.Globalization;

namespace NetworkCheckers.Jogo
{
    /// <summary>
    /// Localização no tabuleiro de damas
    /// </summary>
    public class Location
    {
        // Variáveis
        #region Variaveis

        /// <summary>
        /// Linha do tabuleiro
        /// </summary>
        private int _Row;

        /// <summary>
        /// Coluna do tabuleiro
        /// </summary>
        private int _Col;

        #endregion

        // Construtores
        #region Construtores

        /// <summary>
        /// Inicializa posição na origem (0,0) do tabuleiro
        /// </summary>
        public Location()
           : this(0, 0)
        {
        }

        /// <summary>Inicializa a posição com a coluna e linha</summary>
        /// <param name="row">Linha no tabuleiro</param>
        /// <param name="col">Coluna no tabuleiro</param>
        public Location(int row, int col)
        {
            this._Row = row;
            this._Col = col;
        }

        #endregion

        // Propriedades
        #region Propriedades

        /// <summary>
        /// Linha no tabuleiro
        /// </summary>
        public int Row
        {
            get { return this._Row; }
            set { this._Row = value; }
        }

        /// <summary>
        /// Coluna no trabuleiro
        /// </summary>
        public int Col
        {
            get { return this._Col; }
            set { this._Col = value; }
        }

        #endregion

        // Métodos públicos
        #region Publicos

        /// <summary>
        /// Converte em notação a posição do tabuleiro
        /// </summary>
        /// <returns>Notação da posição atual</returns>
        public int Notation()
        {
            return Notation(this);
        }

        /// <summary>
        /// Converte em notação a posição do tabuleiro
        /// </summary>
        /// <param name="location">Localizaçao para converter em notacão</param>
        /// <returns>Notação da posição atual</returns>
        public static int Notation(Location location)
        {
            return Notation(location.Row, location.Col);
        }

        /// <summary>
        /// Converte em notação a posição do tabuleiro
        /// </summary>
        /// <param name="row">Linha do quadrado</param>
        /// <param name="col">Coluna do quadrado</param>
        /// <returns>Notação da posição atual</returns>
        public static int Notation(int row, int col)
        {
            if (row % 2 == col % 2)
            {
                return -1;
            }
            else
            {
                return ((row * BoardConstants.Rows + col) / 2) + 1;
            }
        }

        /// <summary>
        /// Convert the given board notation position to a location
        /// </summary>
        /// <param name="notation">The board notation positon to convert to location</param>
        /// <returns>The location for the givne board notation position</returns>
        public static Location FromNotation(int notation)
        {
            int row = RowFromNotation(notation);
            int col = ColFromNotation(notation);
            return new Location(row, col);
        }

        /// <summary>Is this location equal to the specified location</summary>
        /// <param name="obj">the location to compare to</param>
        /// <returns><c>true</c> if the location are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Location pos)
            {
                return ((this._Row == pos.Row) && (this._Col == pos.Col));
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current location</returns>
        public override int GetHashCode()
        {
            return ConvertNotationToLocationIndex(this._Row, this._Col);
        }

        /// <summary>
        /// Returns a System.String that represents the current location
        /// </summary>
        /// <returns>A System.String that represents the current location</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", this._Row.ToString(CultureInfo.InvariantCulture), this._Col.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        // Métodos privados
        #region Privados

        /// <summary>Convert location to index based location (0 - 63)</summary>
        /// <param name="row">the block row</param>
        /// <param name="col">the block column</param>
        /// <returns>the indexed based location for the given location</returns>
        private static int ConvertNotationToLocationIndex(int row, int col)
        {
            return row * BoardConstants.Rows + col;
        }

        /// <summary>Convert location in notation format to indexed based location</summary>
        /// <param name="notation">the notation formatted location</param>
        /// <returns>the index based format for notation formatted location</returns>
        private static int ConvertNotationToLocationIndex(int notation)
        {
            if ((notation % BoardConstants.Rows > BoardConstants.Rows / 2) || (notation % BoardConstants.Rows == 0))
            {
                return (notation * 2) - 2;
            }
            else
            {
                return (notation * 2) - 1;
            }
        }

        /// <summary>Get row from notation location</summary>
        /// <param name="notation">the notation formatted location</param>
        /// <returns>the row from the notation location</returns>
        private static int RowFromNotation(int notation)
        {
            int location = ConvertNotationToLocationIndex(notation);

            return (location / BoardConstants.Rows);
        }

        /// <summary>Get col from notation location</summary>
        /// <param name="notation">the notation formatted location</param>
        /// <returns>the col from the notationt location</returns>
        private static int ColFromNotation(int notation)
        {
            int location = ConvertNotationToLocationIndex(notation);

            return (location % BoardConstants.Cols);
        }

        #endregion
    }
}
