using System;
using System.Collections.Generic;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Прохождение опросника
    /// </summary>
    public class QuestionnaireResult
    {
        public long ID { get; set; }

        public ApplicationUser User { get; set; }

        public long UserID { get; set; }

        public DateTime EventDate { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public long QuestionnaireID { get; set; }

        public virtual ICollection<DetailResult> DetailResults { get; set; }
    }
}