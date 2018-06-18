using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Enumerations;

namespace Shared.Models
{
    public class Sorting<T>
    {
        #region Properties

        /// <summary>
        /// Property which should be used for sorting.
        /// </summary>
        public T Property { get; set; }

        /// <summary>
        /// Whether records should be sorted ascending or descending.
        /// </summary>
        public SortDirection Direction { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sorting()
        {

        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="direction"></param>
        public Sorting(T property, SortDirection direction)
        {
            Property = property;
            Direction = direction;
        }

        #endregion
    }
}
