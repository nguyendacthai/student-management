namespace Shared.Models
{
    public class SearchResult<T>
    {
        #region Properties

        /// <summary>
        /// List of searched records.
        /// </summary>
        public T Records { get; set; }

        /// <summary>
        /// Total record which meet the conditions.
        /// </summary>
        public int Total { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Non-parameterized constructor.
        /// </summary>
        public SearchResult()
        {

        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="total"></param>
        public SearchResult(T records, int total)
        {
            Records = records;
            Total = total;
        }
        #endregion
    }
}
