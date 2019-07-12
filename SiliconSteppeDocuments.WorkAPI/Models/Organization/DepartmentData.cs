namespace SiliconSteppeDocuments.API.Models.Organization
{
    public class DepartmentData
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public OrganizationData Organization { get; set; }

        public DepartmentData() { }
        public DepartmentData(Model.Department department)
        {
            if (department == null)
                return;

            ID = department.ID;
            Name = department.Name;
            Organization = new OrganizationData(department.Organization);
        }
    }
}