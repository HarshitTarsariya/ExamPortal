using System;
using System.ComponentModel.DataAnnotations;

namespace ExamPortal.DTOS
{

    public class PaperDTO
    {
        public PaperDTO()
        {
            CreatedDate = DateTime.Now;
        }
        public string PaperCode { get; set; }
        public string TeacherEmailId { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [DeadLineValidate(ErrorMessage = "please specify valid date maximum 62 days are valid")]
        public DateTime DeadLine { get; set; }
        [Required]
        public string PaperTitle { get; set; }

        public EPaperType Type { get; set; }
    }

    public class DeadLineValidate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = Convert.ToDateTime(value);

            return DateTime.Today <= date && date <= DateTime.Today.AddDays(62);
        }
    }
}
