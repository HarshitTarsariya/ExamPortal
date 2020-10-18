using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using ExamPortal.Repositories;
using ExamPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Services
{
    public interface IStudentService
    {
        public MCQPaperDTO GetMcqPaper(string code);
        public List<MCQAnswerSheetDTO> GetAnswerSheets(string email);
    }

    public class StudentServiceImpl : IStudentService
    {
        public StudentServiceImpl(IMapper mapper, IMCQPaperRepo paperRepo, IMCQAnswerSheetRepo answerSheetRepo)
        {
            Mapper = mapper;
            PaperRepo = paperRepo;
            AnswerSheetRepo = answerSheetRepo;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo PaperRepo { get; }
        public IMCQAnswerSheetRepo AnswerSheetRepo { get; }

        public List<MCQAnswerSheetDTO> GetAnswerSheets(string email)
        {
            var answerSheet = AnswerSheetRepo.GetByStudentEmail(email);

            var ans = Mapper.Map<IEnumerable<MCQAnswerSheet>, List<MCQAnswerSheetDTO>>(answerSheet);
            int i = 0;
            foreach (var ele in answerSheet)
            {
                foreach (var que in ele.MCQPaper.Questions)
                    ans[i].TotalMarks += que.Marks;
                i++;
            }
            return ans;
        }

        public MCQPaperDTO GetMcqPaper(string code)
        {
            MCQPaper paper = PaperRepo.GetByPaperCode(code);
            if (paper == null)
                return null;
            MCQPaperDTO paperdto = new MCQPaperDTO();
            Mapper.Map(paper, paperdto);
            foreach (var que in paper.Questions)
                paperdto.Questions.Add(que.EntityToDto());
            paperdto.Questions.ForEach(que => que.TrueAnswer = 0);

            return paperdto;
        }

    }
}
