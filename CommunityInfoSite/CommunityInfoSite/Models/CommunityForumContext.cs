namespace CommunityInfoSite.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CommunityForumContext : IdentityDbContext<MemberUser>// it knows it is managing this model
    {
        // Your context has been configured to use a 'CommunityForumContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CommunityInfoSite.Models.CommunityForumContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CommunityForumContext' 
        // connection string in the application configuration file.
        public CommunityForumContext()
            : base("name=CommunityForumContext")
        {
            //Database.SetInitializer<CommunityForumContext>(null);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public System.Data.Entity.DbSet<CommunityInfoSite.Models.Member> Members { get; set; }
        public System.Data.Entity.DbSet<CommunityInfoSite.Models.Message> Messages { get; set; }
        public System.Data.Entity.DbSet<CommunityInfoSite.Models.Topic> Topics { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}