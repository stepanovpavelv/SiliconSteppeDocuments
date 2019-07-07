namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Дальные результаты прохождения тестирования
    /// </summary>
    public class DetailResult
    {
        public long ID { get; set; }

        public QuestionnaireResult QuestionnaireResult { get; set; }

        public long QuestionnaireResultID { get; set; }

        public Answer Answer { get; set; }

        public long? AnswerID { get; set; }
    }
}