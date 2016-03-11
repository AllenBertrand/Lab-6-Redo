using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class Topic
    {
        List<Message> messages = new List<Message>();

        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public List<Message> Messages { get { return messages; } }

        //.... do i really need this? for now.
        public void AddMessage(Message m)
        {
            Messages.Add(m);
        }
    }
}