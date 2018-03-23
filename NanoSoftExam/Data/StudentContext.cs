using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NanoSoftExam.Models;
using System.Data.Entity;
namespace NanoSoftExam.Data
{
    public class StudentContext:DbContext
    {
        public StudentContext(): base("DefaultConnection")
        {

        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
    }
}