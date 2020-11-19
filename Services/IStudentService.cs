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
    //Student based services
    public interface IStudentService
    {
        public (List<List<int>> answers, MCQPaperDTO paper) GetMcqPaper(string code);
        public List<MCQAnswerSheetDTO> GetMCQAnswerSheets(string email);
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string studentEmailId);
        public AnswerSheetDTO GetAnswerSheet(string paperCode, string studentEmailId);
        public DescriptiveAnswerSheetDTO GetDescriptiveAnswerSheetForExam(string papercode);
        public Task SetDescriptiveAnswerSheet(DescriptiveAnswerSheetDTO desanswersheetdto, string studentEmailId);
        public (int total, IEnumerable<AnswerSheetDTO> answersheet) GetAllAnswerSheets(string studentemailid, int pages);
    }

    public class StudentServiceImpl : IStudentService
    {
        #region Constructor and Properties

        public StudentServiceImpl(IMapper mapper, IMCQPaperRepo paperRepo
            , IMCQAnswerSheetRepo answerSheetRepo, IDescriptiveAnswerSheetRepo descriptiveAnswerSheetRepo
            , IDescriptivePaperRepo descriptivePaperRepo, IFirebaseUpload fire)
        {
            Mapper = mapper;
            PaperRepo = paperRepo;
            AnswerSheetRepo = answerSheetRepo;
            DescriptiveAnswerSheetRepo = descriptiveAnswerSheetRepo;
            DescriptivePaperRepo = descriptivePaperRepo;
            Fire = fire;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo PaperRepo { get; }
        public IMCQAnswerSheetRepo AnswerSheetRepo { get; }
        public IDescriptiveAnswerSheetRepo DescriptiveAnswerSheetRepo { get; }
        public IDescriptivePaperRepo DescriptivePaperRepo { get; }
        public IFirebaseUpload Fire { get; }

        #endregion
        public List<MCQAnswerSheetDTO> GetMCQAnswerSheets(string email)
        {
            var answerSheet = AnswerSheetRepo.GetByStudentEmail(email).ToList();
            return null;
        }

        public (List<List<int>> answers, MCQPaperDTO paper) GetMcqPaper(string code)
        {
            MCQPaper paper = PaperRepo.GetByPaperCode(code);
            if (paper == null)
                return (null, null);
            List<List<int>> answerstobenoted = new List<List<int>>();
            MCQPaperDTO paperdto = new MCQPaperDTO();
            paperdto = Mapper.Map(paper, paperdto);
            foreach (var que in paper.Questions)
            {
                que.MCQOptions.Shuffle();
                List<int> add = new List<int>();
                foreach (var opt in que.MCQOptions)
                {
                    add.Add(opt.MCQOptionId);
                }
                answerstobenoted.Add(add);
                paperdto.Questions.Add(Mapper.Map<MCQQuestion, MCQQuestionDTO>(que));
            }
            paperdto.Questions.ForEach(que => que.TrueAnswer = -1);

            return (answerstobenoted, paperdto);
        }
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string studentEmailId)
        {
            var answersheet = new MCQAnswerSheet();
            var paper1 = PaperRepo.GetByPaperCode(mcqpaperdto.PaperCode);
            var paper = Mapper.Map<MCQPaper, MCQPaperDTO>(paper1);
            foreach (var que in paper1.Questions)
                paper.Questions.Add(Mapper.Map<MCQQuestion, MCQQuestionDTO>(que));
            int ObtainedMarks = 0;
            for (int i = 0; i < paper.Questions.Count; i++)
            {
                if (mcqpaperdto.Questions[i].TrueAnswer == paper1.Questions[i].MCQOptionId)
                    ObtainedMarks += mcqpaperdto.Questions[i].Marks;
            }

            answersheet.MarksObtained = ObtainedMarks;
            answersheet.StudentEmailId = studentEmailId;
            answersheet.SubmittedTime = DateTime.Now;
            answersheet.MCQPaperId = paper1.PaperId;

            AnswerSheetRepo.SetMCQAnswerSheet(answersheet);

            KeyValuePair<int, int> ret = new KeyValuePair<int, int>(paper1.TotalMarks, ObtainedMarks);
            return ret;
        }

        public AnswerSheetDTO GetAnswerSheet(string paperCode, string studentEmailId)
        {
            switch (CodeGenerator.GetPaperType(paperCode))
            {
                case EPaperType.MCQ:
                    var ans = AnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, studentEmailId);
                    if (ans == null)
                        return null;
                    var ret = Mapper.Map<MCQAnswerSheet, MCQAnswerSheetDTO>(ans);
                    if (ret != null)
                        ret.Paper = Mapper.Map<MCQPaper, MCQPaperDTO>(ans.MCQPaper);
                    return ans == null ? null : ret;
                case EPaperType.Descriptive:
                    var ans1 = DescriptiveAnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, studentEmailId);
                    if (ans1 == null)
                        return null;
                    var ret1 = Mapper.Map<DescriptiveAnswerSheet, DescriptiveAnswerSheetDTO>(ans1);
                    if (ret1 != null)
                        ret1.Paper = Mapper.Map<DescriptivePaper, DescriptivePaperDTO>(ans1.DescriptivePaper);
                    return ans1 == null ? null : ret1;
            }
            return null;
        }

        public DescriptiveAnswerSheetDTO GetDescriptiveAnswerSheetForExam(string papercode)
        {
            var paper = Mapper.Map<DescriptivePaper, DescriptivePaperDTO>(DescriptivePaperRepo.GetByPaperCode(papercode));
            paper.PaperPdfUrl = paper.PaperPdfUrl.Replace(Fire.Ampersand, "&");
            var ansSheet = new DescriptiveAnswerSheetDTO()
            {
                Paper = paper
            };
            return ansSheet;
        }
        public async Task SetDescriptiveAnswerSheet(DescriptiveAnswerSheetDTO desanswersheetdto, string studentEmailId)
        {
            var answersheet = new DescriptiveAnswerSheet();
            answersheet.StudentEmailId = studentEmailId;
            answersheet.SubmittedTime = DateTime.Now;
            answersheet.DescriptivePaperId = DescriptivePaperRepo.GetByPaperCode(desanswersheetdto.Paper.PaperCode).PaperId;
            string linkwith = await Fire.Upload(desanswersheetdto.AnswerSheet.OpenReadStream(), studentEmailId, desanswersheetdto.Paper.PaperCode);
            answersheet.AnswerLink = linkwith.Replace("&", Fire.Ampersand);

            DescriptiveAnswerSheetRepo.SetDescriptiveAnswerSheet(answersheet);
        }
        public (int total, IEnumerable<AnswerSheetDTO> answersheet) GetAllAnswerSheets(string studentemailid, int pages)
        {
            var pair = AnswerSheetRepo.GetAllAnswerSheets(studentemailid, pages);
            var sheet = pair.answersheet.ToList();
            var dto = Mapper.Map<IEnumerable<AnswerSheet>, List<AnswerSheetDTO>>(sheet);
            for (int i = 0; i < (sheet.Count()); i++)
            {
                var pp = AnswerSheetRepo.GetPaperByAnswerSheetId(sheet[i].AnswerSheetId);
                dto[i].paperdto = Mapper.Map<Paper, PaperDTO>(pp);
            }
            return (pair.total, dto);
        }
    }
}
