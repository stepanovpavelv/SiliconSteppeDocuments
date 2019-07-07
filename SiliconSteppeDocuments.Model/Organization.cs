namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Организации
    /// </summary>
    public class Organization
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string INN { get; set; }

        public OrganizationType OrganizationType { get; set; }

        public long OrganizationTypeID { get; set; }
    }
}
