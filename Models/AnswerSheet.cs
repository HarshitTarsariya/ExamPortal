using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class AnswerSheet
    {
        public int Id { get; set; }
        public string StudentEmailId { get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}
