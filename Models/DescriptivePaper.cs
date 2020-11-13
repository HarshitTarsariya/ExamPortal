using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("DescriptivePaper")]
    public class DescriptivePaper : Paper
    {
        public string Link { get; set; }
    }
}
