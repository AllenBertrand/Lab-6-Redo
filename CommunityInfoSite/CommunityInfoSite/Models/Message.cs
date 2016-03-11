using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class Message
    {
        public int MessageId {get; set;}
        public int TopicID { get; set; }//FK from topic
        public string From { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MemberUser Member { get; set; }
    }
}