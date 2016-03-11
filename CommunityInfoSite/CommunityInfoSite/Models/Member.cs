using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class Member
    {
        public List<Message> messages = new List<Message>();

        public List<Message> Messages {get{return messages;} }

        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}