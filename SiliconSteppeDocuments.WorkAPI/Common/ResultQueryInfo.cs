using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliconSteppeDocuments.API.Common
{
    /// <summary>
    /// Контейнер сообщений ответа API
    /// </summary>
    public class ResultQueryInfo
    {
        public void AddMessage(MessageType type, string text)
        {
            if (Messages == null)
                Messages = new List<Message>();

            Messages.Add(new Message
            {
                Text = text,
                Type = type
            });
        }

        public void AddException(Exception ex)
        {
            if (Messages == null)
                Messages = new List<Message>();

            Messages.Add(new Message
            {
                Text = ex?.ToString(),
                Type = MessageType.Error
            });
        }

        public bool HasError
        {
            get
            {
                if (Messages != null && Messages.Any())
                {
                    return Messages.Any(x => x.Type == MessageType.Error);
                }

                return false;
            }
        }

        public List<Message> Messages { get; set; }
    }
}