using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class CommunityForumDBInitializer : DropCreateDatabaseAlways<CommunityForumContext>
    {
        protected override void Seed(CommunityForumContext context)
        {                                                   //context is passed in
            Member memberAllen = new Member { Name = "AllenB", Email = "Bertrandallen@gmail.com" };
            Member memberLee = new Member { Name = "LeeB", Email = "BertrandLee@gmail.com" };
            Message message1 = new Message
            {
                Subject = "Hey",
                From = memberAllen.Name,
                Date = new DateTime(2016, 08, 07),
                Body = "Welcome to the new forum!"
                //TopicID = 1
            };
            Message message2 = new Message
            {
                Subject = "Hey back.",
                From = memberLee.Name,
                Date = new DateTime(2016, 08, 07),
                Body = "Glad to be here."
                //TopicID = 1
            };
            Message message3 = new Message
            {
                Subject = "Quid pro quo",
                From = memberLee.Name,
                Date = new DateTime(2016, 08, 08),
                Body = "This forum looks like ass"
                //TopicID = 2
            };
            Message message4 = new Message
            {
                Subject = "re: Quid pro quo",
                From = memberAllen.Name,
                Date = new DateTime(2016, 08, 08),
                Body = "Yea"
                //TopicID = 2
            };

            Topic topicGeneral = new Topic
            {
                TopicName = "General Discussion",
                //TopicId = 1
            };

            Topic topicOffTopic = new Topic
            {
                TopicName = "Off-Topic",
                //TopicId = 2
            };

            topicGeneral.Messages.Add(message1);
            topicGeneral.Messages.Add(message2);
            topicOffTopic.Messages.Add(message3);
            topicOffTopic.Messages.Add(message4);

            context.Members.Add(memberAllen);
            context.Members.Add(memberLee);

            context.Topics.Add(topicGeneral);
            context.Topics.Add(topicOffTopic);

            base.Seed(context);
        }
    }
}