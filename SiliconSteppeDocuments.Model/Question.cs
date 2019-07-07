using System.Collections.Generic;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Вопрос анкеты
    /// </summary>
    public class Question
    {
        public long ID { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public long QuestionnaireID { get; set; }

        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}