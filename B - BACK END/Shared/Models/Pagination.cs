namespace Shared.Models
{
    public class Pagination
    {
        #region Properties

        /// <summary>
        /// Page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Records per page.
        /// </summary>
        public int Records { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Non-parameterized constructor.
        /// </summary>
        public Pagination()
        {
            Page = 1;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        public Pagination(int page, int records)
        {
            Page = page;
            Records = records;
        }

        #endregion
    }
}
