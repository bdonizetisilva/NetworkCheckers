using System;
using System.Collections.Generic;

namespace NetworkCheckers.Jogo
{
    /// <summary>
    /// Encaspsulates a single checkers move.  This includes possible jumps via captures.
    /// </summary>
    public class Move
    {
        #region Constantes

        private const int INVALID_POSITION = -1;

        private const string BLANK_MOVE = "...";

        #endregion

        private readonly List<int> _Positions = new List<int>();


        /// <summary>
        /// Construct an empty move
        /// </summary>
        public Move()
        {
        }

        /// <summary>
        /// Construct a move based on the board postion notation
        /// </summary>
        /// <param name="first">The origin of the move</param>
        /// <param name="rest">The remaining moves.  Greater than one if this move contains jumps</param>
        public Move(int first, params int[] rest)
        {
            AddMoves(first, rest);
        }

        /// <summary>
        /// Construct a move with the given board locations
        /// </summary>
        /// <param name="first">The origin of the move</param>
        /// <param name="rest">The remaining moves.  Greater than one if this move contains jumps</param>
        public Move(Location first, params Location[] rest)
        {
            AddMoves(first, rest);
        }

        /// <summary>
        /// Get the board notation location at the given index.  This is zero-based.
        /// </summary>
        /// <param name="moveIndex">The index to get position for</param>
        /// <returns>The position at the given index</returns>
        public int this[int moveIndex]
        {
            get
            {
                if ((moveIndex < 0) || (moveIndex >= this._Positions.Count))
                {
                    throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
                }

                return this._Positions[moveIndex];
            }
        }

        /// <summary>
        /// Get the location at the given index.  This is zero-based.
        /// </summary>
        /// <param name="moveIndex"></param>
        /// <returns></returns>
        public Location GetLocation(int moveIndex)
        {
            if ((moveIndex < 0) || (moveIndex >= this._Positions.Count))
            {
                throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
            }

            return Location.FromNotation(this._Positions[moveIndex]);
        }

        /// <summary>
        /// Add the given locations to this move
        /// </summary>
        /// <param name="first">The first location to add</param>
        /// <param name="rest">The remaining positions to add</param>
        public void AddMoves(Location first, params Location[] rest)
        {
            this._Positions.Add(first.Notation());
            foreach (Location location in rest)
            {
                this._Positions.Add(location.Notation());
            }
        }

        /// <summary>
        /// Add the given locations to this move using board notation
        /// </summary>
        /// <param name="first">The first location to add</param>
        /// <param name="rest">The remaining positions to add</param>
        public void AddMoves(int first, params int[] rest)
        {
            this._Positions.Add(first);
            foreach (int position in rest)
            {
                this._Positions.Add(position);
            }
        }

        /// <summary>
        /// Get the number of positions in this move
        /// </summary>
        public int Count
        {
            get { return this._Positions.Count; }
        }

        /// <summary>
        /// Get the origin of this move in board notation.  This is the position the move originated at.
        /// </summary>
        public int? Origin
        {
            get
            {
                return (this._Positions.Count > 0) ? new int?(this._Positions[0]) : null;
            }
        }

        /// <summary>
        /// Get the final position of this move in board notation.
        /// </summary>
        public int? Destination
        {
            get
            {
                return (this._Positions.Count > 0) ? new int?(this._Positions[this._Positions.Count - 1]) : null;
            }
        }

        /// <summary>
        /// Get the origin of this move.  This is the position the move originated at.
        /// </summary>
        public Location OriginLocation
        {
            get
            {
                return (this._Positions.Count > 0) ? Location.FromNotation(this._Positions[0]) : null;
            }
        }

        /// <summary>
        /// Get the final position of this move.
        /// </summary>
        public Location DestinationLocation
        {
            get
            {
                return (this._Positions.Count > 0) ? Location.FromNotation(this._Positions[this._Positions.Count - 1]) : null;
            }
        }

        /// <summary>Is this a jumping move</summary>
        /// <returns><code>true</code> if this is a jumping move</returns>
        public bool IsJump()
        {
            if (this._Positions.Count > 2)
            {
                return true;
            }
            else
            {
                Location origin = Location.FromNotation(Origin ?? INVALID_POSITION);
                Location destination = Location.FromNotation(Destination ?? INVALID_POSITION);
                return (
                   (Math.Abs(origin.Row - destination.Row) > 1) ||
                   (Math.Abs(origin.Col - destination.Col) > 1)
                );
            }
        }

        /// <summary>Get the short notation formatted location</summary>
        /// <returns>the short notation formatted location</returns>
        public String ToShortNotationLocation()
        {
            if (this._Positions.Count == 0)
            {
                return BLANK_MOVE;
            }

            return Origin +
                  ((!this.IsJump()) ? "-" : "x") +
                  Destination;

        }

        /// <summary>String representation of move</summary>
        public override String ToString()
        {
            return ToShortNotationLocation();
        }
    }
}
