using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NanoSoftExam.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Father Name")]
        public string FatherName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        public byte[] Image { get; set; }
        public string ClassName { get; set; }
        public Class Class { get; set; }
    }
}