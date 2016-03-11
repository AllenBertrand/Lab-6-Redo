using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommunityInfoSite.Models;

namespace CommunityInfoSite.Controllers
{
    public class MessagesController : Controller
    {
        private CommunityForumContext db = new CommunityForumContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = new List<MessageViewModel>();

            var topics = from topic in db.Topics.Include("Messages")
                         select topic;

            foreach (Topic t in topics)// THIS AINT WORKING, NOT ADDING NEW MESSAGES
            {                           //NEED TO ADD MESSAGES TO TOPICS.MESSAGES
                foreach (Message m in t.Messages)
                {
                    var messageVM = new MessageViewModel();
                    messageVM.Body = m.Body;
                    messageVM.Date = m.Date;
                    messageVM.From = m.From;//Maybe make this m.From.Name? change the whole class to correspond?
                    messageVM.MessageId = m.MessageId;
                    messageVM.Subject = m.Subject;
                    messageVM.TopicItem = t;
                    messages.Add(messageVM);
                }
            }

            return View(messages);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = (from m in db.Messages
                                        join t in db.Topics on m.TopicID equals t.TopicId
                                        where m.MessageId == id
                                        select new MessageViewModel
                                        {
                                            Body = m.Body,
                                            Date = m.Date,
                                            From = m.From,
                                            MessageId = m.MessageId,
                                            Subject = m.Subject,
                                            TopicItem = t
                                        }).FirstOrDefault();
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TopicNames = new SelectList(db.Topics.OrderBy(t => t.TopicName), "TopicId", "TopicName");
            ViewBag.From = new SelectList(db.Members.OrderBy(m => m.Name), "MemberId", "Name");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "MessageId,From,Date,Subject,Body, TopicNames")] MessageViewModel messageVM, int? TopicNames, int? From)
        {
            if (ModelState.IsValid)
            {
                Topic topic = (from t in db.Topics
                               where t.TopicId == TopicNames
                               select t).FirstOrDefault();

                Member from = (from f in db.Members
                               where f.MemberId == From
                               select f).FirstOrDefault();


                if (topic == null)
                {
                    topic = new Topic() { TopicName = messageVM.TopicItem.TopicName };
                    db.Topics.Add(topic);
                }

                if (from == null)//make a new member if nothing is returned from searching the DB
                {
                    from = new Member() { Name = messageVM.From };
                    db.Members.Add(from);
                }

                Message m = new Message()
                {
                    MessageId = messageVM.MessageId,
                    From = from.Name,
                    Date = DateTime.Now,//not sure if this should be done here or not
                    Subject = messageVM.Subject,
                    Body = messageVM.Body
                };

                //I HAD TO REALLY HACK THIS IN CUZ IT WASN'T COOPERATING
                foreach (Topic t in db.Topics)
                {
                    if (t.TopicId == topic.TopicId)
                    {
                        t.AddMessage(m);
                    }
                }

                db.Messages.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicNames = new SelectList(db.Topics.OrderBy(t => t.TopicName), "TopicId", "TopicName");
            ViewBag.From = new SelectList(db.Members.OrderBy(m => m.Name), "MemberId", "Name");
            return View(messageVM);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Edit([Bind(Include = "MessageId,From,Date,Subject,Body")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET SEARCH
        public ActionResult Search()
        {
            return View();
        }

        //POST SEARCH
        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            List<MessageViewModel> messageVMs = new List<MessageViewModel>();
            var messages = (from m in db.Messages
                            where m.Subject.Contains(searchTerm)
                            select m).ToList<Message>();

            foreach (Message m in messages)
            {
                var topic = (from t in db.Topics
                             where t.TopicId == m.TopicID// THIS PROB WONT WORK RIGHT NOW
                             select t).FirstOrDefault();

                messageVMs.Add(new MessageViewModel()
                {
                    MessageId = m.MessageId,
                    From = m.From,
                    Date = m.Date,//not sure if this should be done here or not
                    Subject = m.Subject,
                    Body = m.Body
                });
            }

            //details if just one, index view if more
            if (messageVMs.Count == 1)
                return View("Details", messageVMs[0]);
            else
                return View("Index", messageVMs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private List<Message> GetTopicsAndMessages(int? messageId)
        //{
        //    var messages = new List<Message>();

        //    var topics = from topic in db.Topics.Include("Messages")
        //                 select topic;
            
        //    //Message message = (from m in db.Messages
        //    //                     join t in db.Topics on m.TopicId equals t.TopicId
        //    //                     where m.MessageId == messageId
        //    //                     select new Message
        //    //                     {
        //    //                         MessageId = m.MessageId,
        //    //                         Subject = m.Subject,
        //    //                         From = m.From,
        //    //                         Date = m.Date,
        //    //                         Body = m.Body
        //    //                     }).FirstOrDefault();
        //    //foreach (Topic t in topics)
        //    //{
        //    //    foreach (Message m in t.Messages)
        //    //    {
        //    //        if (m.MessageId == messageId || messageId == 0)
        //    //        {
        //    //            var message = new Message();
        //    //            message.MessageId = m.MessageId;
        //    //            message.Subject = m.Subject;
        //    //            message.To = m.To;
        //    //            message.From = m.From;
        //    //            message.Date = m.Date;
        //    //            message.Body = m.Body;
        //    //            messages.Add(message);
        //    //        }
        //    //    }
        //    //    return messageVM;

        //    foreach (Topic t in topics)
        //    {
        //        foreach (Message m in t.Messages)
        //        {
        //            if (m.MessageId == messageId || 0 == messageId)
        //            {
        //                var message = new Message();
        //                message.Body = m.Body;
        //                message.Date = m.Date;
        //                message.From = m.From;
        //                message.MessageId = m.MessageId;
        //                message.Subject = m.Subject;
        //                message.TopicItem = t;
        //                messages.Add(message);
        //            }
        //        }
        //    }
        //    return messages;
        //}
    }
}
