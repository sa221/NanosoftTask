using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NanoSoftExam.Models
{
    public class Class
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

        public List<Student> Students { get; set; }
    }
}