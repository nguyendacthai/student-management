using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.ViewModels.Specialized;
using Database.Models.Entities;
using Shared.Models;

namespace Business.Interfaces.Businesses
{
    public interface ISpecializedBusiness
    {
        #region Methods

        /// <summary>
        /// Create new specialized asynchronously
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Specialized> AddSpecializedAsync(AddSpecializedViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update specialized asynchronously
        /// </summary>
        /// <returns></returns>
        Task<Specialized> EditSpecializedAsync(int id, EditSpecializedViewModel model, CancellationToken cancellationToken = default (CancellationToken));

        /// <summary>
        /// Search specialized using specific condition asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<SearchResult<IList<Specialized>>> LoadSpecializedAsync(SearchSpecializedViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}
