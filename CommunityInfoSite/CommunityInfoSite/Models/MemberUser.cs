using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.Models
{
    public class MemberUser : IdentityUser
    {
        //username, basically
        public string ForumNick { get; set; }
    }
}