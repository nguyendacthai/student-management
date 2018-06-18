using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Database.Models.Entities;

namespace Database.Models.Contexts
{
    public class RelationalDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initiate database context with connection obtained from App.config.
        /// </summary>
        public RelationalDbContext() : base("name=StudentDbContext")
        {
            System.Data.Entity.Database.SetInitializer<RelationalDbContext>(null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// List of attachment.
        /// </summary>
        public DbSet<Attachment> Attachments { get; set; }

        /// <summary>
        /// List of class.
        /// </summary>
        public DbSet<Class> Classes { get; set; }

        /// <summary>
        /// List of class participations.
        /// </summary>
        public DbSet<ClassParticipation> ClassParticipations { get; set; }

        /// <summary>
        /// List of specialized.
        /// </summary>
        public DbSet<Specialized> Specializeds { get; set; }

        /// <summary>
        /// List of student.
        /// </summary>
        public DbSet<Student> Students { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// Function which is fired when model is being created.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Remove pluralizing naming convention.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion

    }
}
