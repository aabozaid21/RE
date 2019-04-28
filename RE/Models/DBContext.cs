
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RE.Models
{
    public class DBContext:DbContext
    {

        public DBContext() : base("RE") 
        {

        }

        public System.Data.Entity.DbSet<RE.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<RE.Models.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<RE.Models.ProjectUser> ProjectUser { get; set; }
    } 
}