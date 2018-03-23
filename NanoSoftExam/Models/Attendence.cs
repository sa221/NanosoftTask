using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NanoSoftExam.Models
{
    public class Attendence
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public virtual Student Student { get; set; }
    }
}