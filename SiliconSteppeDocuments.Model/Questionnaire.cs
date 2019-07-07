using System.Collections.Generic;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Анкета
    /// </summary>
    public class Questionnaire
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public InspectionType InspectionType { get; set; }

        public long InspectionTypeID { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
