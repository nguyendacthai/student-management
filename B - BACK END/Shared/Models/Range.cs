namespace Shared.Models
{
    public class Range<T>
    {
        #region Properties

        /// <summary>
        /// Minimum value.
        /// </summary>
        public T From { get; set; }

        /// <summary>
        /// Maximum value.
        /// </summary>
        public T To { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default initiator.
        /// </summary>
        public Range()
        {

        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Range(T from, T to)
        {
            From = from;
            To = to;
        }

        #endregion
    }
}
