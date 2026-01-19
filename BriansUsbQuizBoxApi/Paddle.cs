using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Represents a quiz box paddle by color and number
    /// </summary>
    public class Paddle : IEquatable<Paddle>
    {
        /// <summary>
        /// Represents red paddle 1
        /// </summary>
        static readonly public Paddle RED_1 = new Paddle(PaddleColorEnum.Red, PaddleNumberEnum.One);
        /// <summary>
        /// Represents red paddle 2
        /// </summary>
        static readonly public Paddle RED_2 = new Paddle(PaddleColorEnum.Red, PaddleNumberEnum.Two);
        /// <summary>
        /// Represents red paddle 3
        /// </summary>
        static readonly public Paddle RED_3 = new Paddle(PaddleColorEnum.Red, PaddleNumberEnum.Three);
        /// <summary>
        /// Represents red paddle 4
        /// </summary>
        static readonly public Paddle RED_4 = new Paddle(PaddleColorEnum.Red, PaddleNumberEnum.Four);

        /// <summary>
        /// Represents green paddle 1
        /// </summary>
        static readonly public Paddle GREEN_1 = new Paddle(PaddleColorEnum.Green, PaddleNumberEnum.One);
        /// <summary>
        /// Represents green paddle 2
        /// </summary>
        static readonly public Paddle GREEN_2 = new Paddle(PaddleColorEnum.Green, PaddleNumberEnum.Two);
        /// <summary>
        /// Represents green paddle 3
        /// </summary>
        static readonly public Paddle GREEN_3 = new Paddle(PaddleColorEnum.Green, PaddleNumberEnum.Three);
        /// <summary>
        /// Represents green paddle 4
        /// </summary>
        static readonly public Paddle GREEN_4 = new Paddle(PaddleColorEnum.Green, PaddleNumberEnum.Four);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Paddle color</param>
        /// <param name="number">Paddle number</param>
        public Paddle(PaddleColorEnum color, PaddleNumberEnum number)
        {
            _color = color;
            _number = number;
        }

        private readonly PaddleColorEnum _color = PaddleColorEnum.None;

        /// <summary>
        /// Paddle color
        /// </summary>
        public PaddleColorEnum Color
        {
            get { return _color; }
        }

        private readonly PaddleNumberEnum _number = PaddleNumberEnum.None;

        /// <summary>
        /// Paddle number
        /// </summary>
        public PaddleNumberEnum Number
        {
            get { return _number; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Paddle);
        }

        public bool Equals(Paddle? p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case
            if (ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false
            if (GetType() != p.GetType())
            {
                return false;
            }

            return (Color == p.Color) && (Number == p.Number);
        }

        public override int GetHashCode() => (Color, Number).GetHashCode();

        public static bool operator ==(Paddle? lhs, Paddle? rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }

            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Paddle? lhs, Paddle? rhs) => !(lhs == rhs);

        public override string ToString()
        {
            return $"{Color} - {Number}";
        }
    }
}
