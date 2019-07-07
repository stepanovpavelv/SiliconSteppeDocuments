using Microsoft.EntityFrameworkCore;
using SiliconSteppeDocuments.Model;

namespace SiliconSteppeDocuments.DB
{
    public static class SteppeModelBuilder
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Organization type
            builder.Entity<OrganizationType>().ToTable("OrganizationTypes");
            builder.Entity<OrganizationType>().HasKey(x => x.ID);

            // Organization
            builder.Entity<Organization>().ToTable("Organizations");
            builder.Entity<Organization>().HasKey(x => x.ID);
            builder.Entity<Organization>().Property(x => x.Name).HasMaxLength(75);
            builder.Entity<Organization>().HasOne(x => x.OrganizationType).WithMany().HasForeignKey(x => x.OrganizationTypeID);

            // Department
            builder.Entity<Department>().ToTable("Departments");
            builder.Entity<Department>().HasKey(x => x.ID);
            builder.Entity<Department>().HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationID);

            // User
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.ID);
            builder.Entity<User>().Property(x => x.FirstName).HasMaxLength(150);
            builder.Entity<User>().Property(x => x.SecondName).HasMaxLength(150);
            builder.Entity<User>().Property(x => x.MiddleName).HasMaxLength(150);
            builder.Entity<User>().HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentID);
            builder.Entity<User>().HasMany(x => x.Rates).WithOne(x => x.RateUser).HasForeignKey(x => x.RateUserID);

            // Inspection type
            builder.Entity<InspectionType>().ToTable("InspectionTypes");
            builder.Entity<InspectionType>().HasKey(x => x.ID);

            // Questionnaire 
            builder.Entity<Questionnaire>().ToTable("Questionnaire");
            builder.Entity<Questionnaire>().HasKey(x => x.ID);
            builder.Entity<Questionnaire>().HasOne(x => x.InspectionType).WithMany().HasForeignKey(x => x.InspectionTypeID);
            builder.Entity<Questionnaire>().HasMany(x => x.Questions).WithOne(x => x.Questionnaire).HasForeignKey(x => x.QuestionnaireID);

            // Questions
            builder.Entity<Question>().ToTable("Questions");
            builder.Entity<Question>().HasKey(x => x.ID);
            builder.Entity<Question>().HasOne(x => x.Questionnaire).WithMany().HasForeignKey(x => x.QuestionnaireID);
            builder.Entity<Question>().HasMany(x => x.Answers).WithOne(x => x.Question).HasForeignKey(x => x.QuestionID);

            // Answers
            builder.Entity<Answer>().ToTable("Answers");
            builder.Entity<Answer>().HasKey(x => x.ID);
            builder.Entity<Answer>().HasOne(x => x.Question).WithMany().HasForeignKey(x => x.QuestionID);

            // QuestionnaireResult
            builder.Entity<QuestionnaireResult>().ToTable("QuestionnaireResults");
            builder.Entity<QuestionnaireResult>().HasKey(x => x.ID);
            builder.Entity<QuestionnaireResult>().HasOne(x => x.Questionnaire).WithMany().HasForeignKey(x => x.QuestionnaireID);
            builder.Entity<QuestionnaireResult>().HasMany(x => x.DetailResults).WithOne(x => x.QuestionnaireResult).HasForeignKey(x => x.QuestionnaireResultID);

            // Detail result
            builder.Entity<DetailResult>().ToTable("DetailResults");
            builder.Entity<DetailResult>().HasKey(x => x.ID);
            builder.Entity<DetailResult>().HasOne(x => x.QuestionnaireResult).WithMany().HasForeignKey(x => x.QuestionnaireResultID);

            // Invites
            builder.Entity<Invite>().ToTable("Invites");
            builder.Entity<Invite>().HasKey(x => x.ID);
            builder.Entity<Invite>().HasOne(x => x.InvitedUser).WithMany().HasForeignKey(x => x.InvitedUserID);
            builder.Entity<Invite>().HasOne(x => x.ResponsibleUser).WithMany().HasForeignKey(x => x.ResponsibleUserID);

            // Rates
            builder.Entity<Rate>().ToTable("Rates");
            builder.Entity<Rate>().HasKey(x => x.ID);
            builder.Entity<Rate>().HasOne(x => x.RateUser).WithMany().HasForeignKey(x => x.RateUserID);
            builder.Entity<Rate>().HasOne(x => x.FromUser).WithMany().HasForeignKey(x => x.FromUserID);
        }
    }
}