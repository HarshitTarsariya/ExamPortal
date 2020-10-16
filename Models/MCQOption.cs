using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class MCQOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
    }
}
