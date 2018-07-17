using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Enumerations;
using Shared.Models;
using Shared.Resources;
using StudentManagement.Enumerations;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.Models.Attachment;
using StudentManagement.ViewModels.Attachment;

namespace StudentManagement.Controllers.Attachment
{
    [RoutePrefix("api/attachment")]
    public class AttachmentController : ApiBasicController
    {
        #region Constructor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public AttachmentController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService) : base(unitOfWork, identityService, systemTimeService)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Upload attachment
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> UploadAttachment([FromBody] AddAttachmentViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new AddAttachmentViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var students = UnitOfWork.RepositoryStudent.Search();

            // Find the first match request.
            var student = await students.Where(x => x.Id == info.StudentId).FirstOrDefaultAsync();

            // Check null
            if (student == null)
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.StudentNotFound));

            var document = info.Document;
            var fileExtension = Path.GetExtension(document.Name);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.Replace(".", "");

            var name = document.Name.Split('.');
            var documentName = name[0];

            var attachment = new Database.Models.Entities.Attachment
            {
                StudentId = info.StudentId,
                Name = documentName,
                Type = fileExtension,
                Content = document.Buffer,
                Status = MasterItemStatus.Active
            };

            //attachment = UnitOfWork.RepositoryAttachment.Insert(attachment);

            //await UnitOfWork.CommitAsync();

            return Ok(attachment);
        }

        /// <summary>
        ///     Load attachment Information
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("load-attachment")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoadAttachment(SearchAttachmentViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new SearchAttachmentViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            // Get all attachment from database
            var attachments = UnitOfWork.RepositoryAttachment.Search();

            // Id have been defined
            if (info.Ids != null && info.Ids.Count > 0)
            {
                info.Ids = info.Ids.Where(x => x > 0).ToList();
                if (info.Ids != null && info.Ids.Count > 0)
                {
                    attachments = attachments.Where(x => info.Ids.Contains(x.Id));
                }
            }

            // Student id have been defined
            if (info.StudentIds != null && info.StudentIds.Count > 0)
            {
                info.StudentIds = info.StudentIds.Where(x => x > 0).ToList();
                if (info.StudentIds != null && info.StudentIds.Count > 0)
                {
                    attachments = attachments.Where(x => info.StudentIds.Contains(x.StudentId));
                }
            }

            var attachmentDetails = attachments.Select(attachment => new AttachmentDetailsModel
            {
                Id = attachment.Id,
                StudentId = attachment.StudentId,
                Name = attachment.Name,
                Type = attachment.Type
        });

            // Do sorting.
            var sorting = info.Sort;
            if (sorting != null)
                attachmentDetails = UnitOfWork.Sort(attachmentDetails, sorting.Direction, sorting.Property);
            else
                attachmentDetails = UnitOfWork.Sort(attachmentDetails, SortDirection.Ascending,
                    AttachmentPropertySort.Name);

            // Paginate.
            var result = new SearchResult<IList<AttachmentDetailsModel>>
            {
                Total = attachmentDetails.Count(),
                Records = await UnitOfWork.Paginate(attachmentDetails, info.Pagination).ToListAsync()
            };


            return Ok(result);
        }

        /// <summary>
        ///     Get Attachment Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get-attachment/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAttachment([FromUri] int id)
        {
            #region FullSearch Attachment

            var attachments = UnitOfWork.RepositoryAttachment.Search();
            var attachment = await attachments.Where(x => x.Id == id && x.Status == MasterItemStatus.Active).FirstOrDefaultAsync();

            if (attachment == null)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    HttpMessages.AttachmentNotFound));

            var oAttachment = new AttachmentModel();

            oAttachment.Id = attachment.Id;
            if (attachment.Content != null)
                oAttachment.Content = Convert.ToBase64String(attachment.Content);
            oAttachment.StudentId = attachment.StudentId;
            oAttachment.Name = attachment.Name;
            oAttachment.Type = attachment.Type;

            #endregion

            return Ok(oAttachment);
        }

        #endregion
    }
}