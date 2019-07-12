using System;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Оценки выполненной работы
    /// </summary>
    public class Rate
    {
        public long ID { get; set; }

        public DateTime EventDate { get; set; }

        public ApplicationUser RateUser { get; set; }

        public long RateUserID { get; set; }

        public ApplicationUser FromUser { get; set; }

        public long FromUserID { get; set; }

        public decimal Score { get; set; }
    }
}