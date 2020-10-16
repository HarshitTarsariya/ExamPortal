using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{

    public class MCQPaperDTO : PaperDTO
    {
        public MCQPaperDTO()
        {
            Questions = new List<MCQQuestionDTO>();
        }

        public List<MCQQuestionDTO> Questions { get; set; }

    }
    //public class ListItemDTO
    //{
    //    public string Text { get; set; }
    //    public int Value { get; set; }
    //    public bool Selected { get; set; }
    //}
    //public enum AnswerType
    //{
    //    Radio = 0, Text = 1, CheckBox = 2
    //}
    //public enum PaperType
    //{
    //    MCQS = 0, Discriptive = 1
    //}
    //public class PaperDTO
    //{
    //    public PaperDTO()
    //    {
    //        PaperType = PaperType.MCQS;
    //        Questions = new List<QuestionDTO>();
    //        ExpTime = DateTime.Today.AddDays(2);
    //    }
    //    public PaperType PaperType { get; set; }
    //    public DateTime ExpTime { get; set; }
    //    public List<QuestionDTO> Questions { get; set; }

    //}



    //public class QuestionDTO
    //{
    //    public QuestionDTO()
    //    {
    //        AnswerType = AnswerType.Text;
    //        Opetions = new List<ListItemDTO>();
    //        QuestionText = "";
    //    }
    //    public string QuestionText { get; set; }
    //    public AnswerType AnswerType { get; set; }
    //    public List<ListItemDTO> Opetions { get; set; }
    //}


}
