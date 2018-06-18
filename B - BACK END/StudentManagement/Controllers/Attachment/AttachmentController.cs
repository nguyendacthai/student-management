using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Resources;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.ViewModels.Attachment;

namespace StudentManagement.Controllers.Attachment
{
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

            //var document = info.Document;
            //var fileExtension = Path.GetExtension(document.Name);
            //if (!string.IsNullOrEmpty(fileExtension))
            //    fileExtension = fileExtension.Replace(".", "");

            //var attachment = new Database.Models.Entities.Attachment
            //{
            //    StudentId = info.StudentId,
            //    Name = info.Name,
            //    Type = fileExtension,
            //    Content = document.Buffer,
            //    Status = MasterItemStatus.Active
            //};

            //attachment = UnitOfWork.RepositoryAttachment.Insert(attachment);

            //await UnitOfWork.CommitAsync();

            return Ok();
        }

        #endregion
    }
}