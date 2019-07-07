using System;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Приглашение фрилансера
    /// </summary>
    public class Invite
    {
        public long ID { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public User InvitedUser { get; set; }

        public long InvitedUserID { get; set; }

        public User ResponsibleUser { get; set; }

        public long ResponsibleUserID { get; set; }

        public bool Active { get; set; }
    }
}