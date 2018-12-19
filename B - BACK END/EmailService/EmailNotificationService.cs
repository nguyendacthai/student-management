using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using Database.Models.Contexts;
using log4net;
using SharedService.Interfaces.Repositories;
using SharedService.Interfaces.Services;
using SharedService.Repositories;
using SharedService.Services;

namespace EmailService
{
    public partial class EmailNotificationService : ServiceBase
    {
        #region Properties

        /// <summary>
        ///     Log module.
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        ///     Timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        ///     Unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Service which is for sending email.
        /// </summary>
        private readonly IEmailService _emailService;

        private readonly IIdentityService _identityService;

        #endregion

        #region Constructors

        public EmailNotificationService()
        {

            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            _unitOfWork = new UnitOfWork(new RelationalDbContext());

            _emailService = new SharedService.Services.EmailService();

            _identityService = new IdentityService();

            InitializeComponent();
        }

        #endregion

//        public void OnDebug()
//        {
//            OnStart(null);
//        }

        #region Methods

        /// <summary>
        /// Start service
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                _timer = new Timer();
                _timer.Interval = CalculateTimerTime();
                _timer.Elapsed += OnTimedEvent;
                _timer.Start();
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
            }
        }

        /// <summary>
        /// Calculate time to send mail every day
        /// </summary>
        /// <returns></returns>
        private double CalculateTimerTime()
        {
            // Get setting time
            var theTimeWillBeRequest = GetSettingTime();

            if (theTimeWillBeRequest < DateTime.Now)
            {
                var nextDay = theTimeWillBeRequest.AddDays(1);

                theTimeWillBeRequest = new DateTime(nextDay.Year, nextDay.Month,
                    nextDay.Day, nextDay.Hour, nextDay.Minute, 0);

            }

            var diff = theTimeWillBeRequest - DateTime.Now;

            var timerTick = diff.TotalMilliseconds;

            return timerTick;
        }

        /// <summary>
        /// Getting time
        /// </summary>
        /// <returns></returns>
        private DateTime GetSettingTime()
        {
            // Get time from app.config
            var time = ConfigurationManager.AppSettings["TimeToRequest"];
            int hour;
            int minute;
            if (time.Contains(":"))
            {
                var splitTime = time.Split(':');
                hour = int.Parse(splitTime[0]);
                minute = int.Parse(splitTime[1]);
            }
            else
            {
                hour = 6;
                minute = 0;
            }

            var theTimeWillBeRequest = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, hour, minute, 0);

            return theTimeWillBeRequest;
        }

        /// <summary>
        /// Execute email service
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                var students = _unitOfWork.RepositoryStudent.Search().Where(x => x.ForgotPassword);
                var emails = students.Select(x => x.Email).ToList();

                foreach (var student in students)
                {
                    student.ForgotPassword = false;
                    student.Password = _identityService.HashPassword("pass@word1");
                    _unitOfWork.RepositoryStudent.InsertOrUpdate(student);
                }

                // Update information to db
                _unitOfWork.Commit();

                foreach (var email in emails)
                {
                    var mailTo = new MailAddress(email);

                    // Send email to user who forgot password
                    _emailService.SendMail(new[] { mailTo }, "Updated password", "New password: pass@word1", null, false, false);
                }

                // Set time again
                _timer.Interval = CalculateTimerTime();
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Stop service
        /// </summary>
        protected override void OnStop()
        {
        }

        #endregion

    }
}
