using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class MessageViewModel
    {
        private Topic topic = new Topic();
        //private Member member = new Member(); Instead of passing in a whole member class, im only going to pass in the member name

        public int MessageId { get; set; }

        [Required]
        public string From { get; set; }

        [Display(Name = "Date Posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Subject { get; set; }

        [StringLength(140, MinimumLength = 5)]
        [Required]
        public string Body { get; set; }

        //[Required] ASK TEACHER ABOUT THIS. NOT HAPPENING BECAUSE VIEW ASKS FOR TOPICITEM.NAME
        public Topic TopicItem { get { return topic; } set { topic = value; } }
        // trying this without tying a topic to a message, doing topic -> message

        //public TopicsClass Topic { get; set; }// this might not be right. Check topics class
    }
}