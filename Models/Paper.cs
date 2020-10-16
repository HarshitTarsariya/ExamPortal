using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class Paper
    {
        public int Id { get; set; }
        public string PaperCode { get; set; }
        public string TeacherEmailId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string PaperTitle { get; set; }
    }
}
