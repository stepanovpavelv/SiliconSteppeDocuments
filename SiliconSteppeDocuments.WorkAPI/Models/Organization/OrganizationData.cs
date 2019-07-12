namespace SiliconSteppeDocuments.API.Models.Organization
{
    public class OrganizationData
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string INN { get; set; }
        public OrganizationTypeData OrganizationType { get; set; }

        public OrganizationData() { }
        public OrganizationData(Model.Organization organization)
        {
            if (organization == null)
                return;

            ID = organization.ID;
            Name = organization.Name;
            FullName = organization.FullName;
            INN = organization.INN;
            OrganizationType = new OrganizationTypeData(organization.OrganizationType);
        }
    }
}