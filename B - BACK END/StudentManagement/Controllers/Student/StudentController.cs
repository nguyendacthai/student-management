using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Enumerations;
using Shared.Models;
using Shared.Resources;
using StudentManagement.Attributes;
using StudentManagement.Enumerations;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.ViewModels.Student;

namespace StudentManagement.Controllers.Student
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiBasicController
    {
        #region Constructor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public StudentController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService) : base(unitOfWork, identityService, systemTimeService)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create new student
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ApiRole(new[]
        {
            UserRoles.Admin
        })]
        public async Task<IHttpActionResult> CreateStudent([FromBody] AddStudentViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new AddStudentViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var students = UnitOfWork.RepositoryStudent.Search();
            students =
                students.Where(x => x.Username.Equals(info.Username, StringComparison.InvariantCultureIgnoreCase));

            // Student exists.
            if (await students.AnyAsync())
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CannotBeDuplicated));

            var student = new Database.Models.Entities.Student
            {
                Username = info.Username,
                Password = IdentityService.HashPassword(info.Password),
                Fullname = info.Fullname,
                Gender = info.Gender,
                Phone = info.Phone,
                Status = MasterItemStatus.Active
            };

            student = UnitOfWork.RepositoryStudent.Insert(student);

            // Add roles for user
            if (info.Roles != null)
            {
                foreach (var roleId in info.Roles)
                {
                    var userRole = new Database.Models.Entities.UserRole
                    {
                        StudentId = student.Id,
                        RoleId = roleId
                    };

                    UnitOfWork.RepositoryUserRole.Insert(userRole);
                }
            }

            //await UnitOfWork.CommitAsync();

            return Ok(student);
        }

        /// <summary>
        /// Update student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ApiRole(new[]
        {
            UserRoles.Admin
        })]
        public async Task<IHttpActionResult> EditStudent([FromUri] int id, [FromBody] EditStudentViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new EditStudentViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Find class

            var students = UnitOfWork.RepositoryStudent.Search();

            var student = await students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (student == null)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.StudentNotFound));
            }

            #endregion

            #region Update student information

            // Check whether information has been updated or not.
            var bHasInformationChanged = false;
            
            // Phone number is specified.
            if (info.Phone != student.Phone)
            {
                student.Phone = info.Phone;
                bHasInformationChanged = true;
            }

            // Status is defined.
            if (info.Status != null && info.Status != student.Status)
            {
                student.Status = info.Status.Value;
                bHasInformationChanged = true;
            }

            // Information has been changed. Update the date time.
            if (bHasInformationChanged)
            {
                // Save information into database.
                await UnitOfWork.CommitAsync();
            }

            #endregion

            return Ok(student);
        }

        /// <summary>
        /// Search for a list of student
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("load-student")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoadStudent(SearchStudentViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new SearchStudentViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var identity = IdentityService.FindRequestIdentity(Request);
            
            // Get all student from database
            var students = UnitOfWork.RepositoryStudent.Search();

            // Id have been defined
            if (info.Ids != null && info.Ids.Count > 0)
            {
                info.Ids = info.Ids.Where(x => x > 0).ToList();
                if (info.Ids != null && info.Ids.Count > 0)
                {
                    students = students.Where(x => info.Ids.Contains(x.Id));
                }
            }

            // User name have been defined
            if (info.Usernames != null && info.Usernames.Count > 0)
            {
                info.Usernames = info.Usernames.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (info.Usernames.Count > 0)
                    students = students.Where(x => info.Usernames.Any(y => x.Username.Contains(y)));
            }

            // Full name have been defined
            if (info.Fullnames != null && info.Fullnames.Count > 0)
            {
                info.Fullnames = info.Fullnames.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (info.Fullnames.Count > 0)
                    students = students.Where(x => info.Fullnames.Any(y => x.Fullname.Contains(y)));
            }

            // Phone have been defined
            if (info.Phones != null && info.Phones.Count > 0)
            {
                info.Phones = info.Phones.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (info.Phones.Count > 0)
                    students = students.Where(x => info.Phones.Any(y => x.Phone.Contains(y)));
            }

            // Gender have been defined.
            if (info.Genders != null && info.Genders.Count > 0)
            {
                info.Genders =
                    info.Genders.Where(x => Enum.IsDefined(typeof(Gender), x)).ToList();
                if (info.Genders.Count > 0)
                    students = students.Where(x => info.Genders.Contains(x.Gender));
            }

            // Status have been defined.
            if (info.Statuses != null && info.Statuses.Count > 0)
            {
                info.Statuses =
                    info.Statuses.Where(x => Enum.IsDefined(typeof(MasterItemStatus), x)).ToList();
                if (info.Statuses.Count > 0)
                    students = students.Where(x => info.Statuses.Contains(x.Status));
            }

            // Do sorting.
            var sorting = info.Sort;
            if (sorting != null)
                students = UnitOfWork.Sort(students, sorting.Direction, sorting.Property);
            else
                students = UnitOfWork.Sort(students, SortDirection.Ascending,
                    StudentPropertySort.Username);

            // Paginate.
            var result = new SearchResult<IList<Database.Models.Entities.Student>>
            {
                Total = students.Count(),
                Records = await UnitOfWork.Paginate(students, info.Pagination).ToListAsync()
            };

            return Ok(result);
        }

        #endregion
    }
}