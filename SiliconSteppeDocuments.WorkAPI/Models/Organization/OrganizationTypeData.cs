namespace SiliconSteppeDocuments.API.Models.Organization
{
    public class OrganizationTypeData
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public OrganizationTypeData() { }
        public OrganizationTypeData(Model.OrganizationType organizationType)
        {
            if (organizationType == null)
                return;

            ID = organizationType.ID;
            Name = organizationType.Name;
        }
    }
}