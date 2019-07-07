namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Перечень вопросов на ответы
    /// </summary>
    public class Answer
    {
        public long ID { get; set; }

        public Question Question { get; set; }

        public long QuestionID { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}