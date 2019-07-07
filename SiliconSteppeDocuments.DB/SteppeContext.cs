using Microsoft.EntityFrameworkCore;
using SiliconSteppeDocuments.Model;

namespace SiliconSteppeDocuments.DB
{
    public class SteppeContext : DbContext
    {
        public SteppeContext(DbContextOptions<SteppeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SteppeModelBuilder.OnModelCreating(builder);
            base.OnModelCreating(builder);
        }

        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InspectionType> InspectionTypes { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionnaireResult> QuestionnaireResults { get; set; }
        public DbSet<DetailResult> DetailResults { get; set; }
        public DbSet<Invite> Invites { get; set; }
    }
}
