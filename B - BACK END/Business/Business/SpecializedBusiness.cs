using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces.Businesses;
using Business.ViewModels.Specialized;
using Database.Enumerations;
using Database.Models.Entities;
using Shared.Enumerations;
using Shared.Enumerations.Specialized;
using Shared.Models;
using Shared.Resources;
using SharedService.Exceptions;
using SharedService.Interfaces.Repositories;

namespace Business.Business
{
    public class SpecializedBusiness : ISpecializedBusiness
    {
        #region Properties

        /// <summary>
        /// Instance Unit Of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public SpecializedBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create new specialized
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<Specialized> AddSpecializedAsync(AddSpecializedViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Search specialized
            var specializeds = _unitOfWork.RepositorySpecialized.Search();
            specializeds =
                specializeds.Where(x => x.Name.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase));

            // Check Specialized exists or not.
            if (await specializeds.AnyAsync(cancellationToken))
                throw new ApiException(HttpMessages.CannotBeDuplicated, HttpStatusCode.Conflict);

            var specialized = new Specialized
            {
                Name = model.Name,
                Status = MasterItemStatus.Active
            };

            _unitOfWork.RepositorySpecialized.Insert(specialized);

            // Commit to db.
            await _unitOfWork.CommitAsync();

            return specialized;
        }

        /// <summary>
        /// Edit specialized
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<Specialized> EditSpecializedAsync(int id, EditSpecializedViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Get all specialized
            var specializeds = _unitOfWork.RepositorySpecialized.Search();

            var specialized = await specializeds.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

            // Check item exist or not
            if (specialized == null)
                throw new ApiException(HttpMessages.SpecializedNotFound, HttpStatusCode.NotFound);

            #endregion

            #region Update specialized information

            // Check whether information has been updated or not.
            var bHasInformationChanged = false;

            // Name is specified.
            if (model.Name != null && model.Name != specialized.Name)
            {
                specialized.Name = model.Name;
                bHasInformationChanged = true;
            }

            // Status is defined.
            if (model.Status != null && model.Status != specialized.Status)
            {
                specialized.Status = model.Status.Value;
                bHasInformationChanged = true;
            }

            // Information has been changed. Update the date time.
            if (bHasInformationChanged)
            {
                // Save information into database.
                await _unitOfWork.CommitAsync();
            }

            return specialized;
        }

        /// <summary>
        /// Search specialized using specific condition asynchronously.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<SearchResult<IList<Specialized>>> LoadSpecializedAsync(SearchSpecializedViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Get all specialized from database
            var specializeds = _unitOfWork.RepositorySpecialized.Search();

            // Id have been defined
            if (model.Ids != null && model.Ids.Count > 0)
            {
                model.Ids = model.Ids.Where(x => x > 0).ToList();
                if (model.Ids != null && model.Ids.Count > 0)
                {
                    specializeds = specializeds.Where(x => model.Ids.Contains(x.Id));
                }
            }

            // Name have been defined
            if (model.Names != null && model.Names.Count > 0)
            {
                model.Names = model.Names.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (model.Names.Count > 0)
                    specializeds = specializeds.Where(x => model.Names.Any(y => x.Name.Contains(y)));
            }

            // Statuses have been defined.
            if (model.Statuses != null && model.Statuses.Count > 0)
            {
                model.Statuses =
                    model.Statuses.Where(x => Enum.IsDefined(typeof(MasterItemStatus), x)).ToList();
                if (model.Statuses.Count > 0)
                    specializeds = specializeds.Where(x => model.Statuses.Contains(x.Status));
            }

            // Do sorting.
            var sorting = model.Sort;
            if (sorting != null)
                specializeds = _unitOfWork.Sort(specializeds, sorting.Direction, sorting.Property);
            else
                specializeds = _unitOfWork.Sort(specializeds, SortDirection.Ascending,
                    SpecializedPropertySort.Name);

            // Paginate.
            var result = new SearchResult<IList<Specialized>>
            {
                Total = specializeds.Count(),
                Records = await _unitOfWork.Paginate(specializeds, model.Pagination).ToListAsync(cancellationToken)
            };

            return result;
        }

        #endregion

    }
}
