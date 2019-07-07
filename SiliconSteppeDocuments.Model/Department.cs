namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Отделы (офисы) организаций
    /// </summary>
    public class Department
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public Organization Organization { get; set; }

        public long OrganizationID { get; set; }
    }
}
