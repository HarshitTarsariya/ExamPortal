using ExamPortal.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ExamPortal.DTOS
{

    public class PaperDTO
    {
        public PaperDTO()
        {
            CreatedDate = DateTime.Now.ToString(AutoMapperConfig.DateFormat, CultureInfo.InvariantCulture);
        }
        public string PaperCode { get; set; }
        public string TeacherEmailId { get; set; }
        public string CreatedDate { get; set; }
        [Required]
        [DeadLineValidate(maxday: 62, ErrorMessage = "please specify valid date maximum 62 days are valid")]
        public string DeadLine { get; set; }
        [Required]
        public string PaperTitle { get; set; }
        public int TotalMarks { get; set; }

        public EPaperType Type { get; set; }
    }

    public class DeadLineValidate : ValidationAttribute
    {
        public DeadLineValidate(int maxday)
        {
            Maxday = maxday;
        }

        public int Maxday { get; }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParseExact(value as string, AutoMapperConfig.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return DateTime.Today <= date && date <= DateTime.Today.AddDays(Maxday);
            }
            return false;
        }
    }
}
