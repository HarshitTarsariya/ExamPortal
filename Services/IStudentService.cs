using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using ExamPortal.Repositories;
using ExamPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPortal.Services
{
    //Student based services
    public interface IStudentService
    {
        public MCQPaperDTO GetMcqPaper(string code);
        public List<MCQAnswerSheetDTO> GetMCQAnswerSheets(string email);
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string studentEmailId);
        public AnswerSheetDTO GetAnswerSheet(string paperCode, string studentEmailId);
        public DescriptiveAnswerSheetDTO GetDescriptiveAnswerSheetForExam(string papercode);
        public Task SetDescriptiveAnswerSheet(DescriptiveAnswerSheetDTO desanswersheetdto, string studentEmailId);
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
            var answerSheet = AnswerSheetRepo.GetByStudentEmail(email);

            var ans = Mapper.Map<IEnumerable<MCQAnswerSheet>, List<MCQAnswerSheetDTO>>(answerSheet);
            int i = 0;
            foreach (var ele in answerSheet)
            {
                foreach (var que in ele.MCQPaper.Questions)
                {
                    ans[i].TotalMarks += que.Marks;
                }
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
            paperdto=Mapper.Map(paper, paperdto);
            foreach (var que in paper.Questions)
            {
                que.MCQOptions.Shuffle();
                paperdto.Questions.Add(que.EntityToDto());
            }
            paperdto.Questions.ForEach(que => que.TrueAnswer = -1);

            return paperdto;
        }
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string studentEmailId)
        {
            var answersheet = new MCQAnswerSheet();
            var paper1 = PaperRepo.GetByPaperCode(mcqpaperdto.PaperCode);
            var paper = Mapper.Map<MCQPaper, MCQPaperDTO>(paper1);
            foreach (var que in paper1.Questions)
                paper.Questions.Add(que.EntityToDto());
            int TotalMarks = 0, ObtainedMarks = 0;
            for (int i = 0; i < paper.Questions.Count; i++)
            {
                TotalMarks += paper.Questions[i].Marks;
                System.Diagnostics.Debug.Print(mcqpaperdto.Questions[i].TrueAnswer.ToString());
                if (mcqpaperdto.Questions[i].TrueAnswer == paper.Questions[i].TrueAnswer)
                    ObtainedMarks += mcqpaperdto.Questions[i].Marks;
            }
            answersheet.MarksObtained = ObtainedMarks;
            answersheet.StudentEmailId = studentEmailId;
            answersheet.SubmittedTime = DateTime.Now;
            answersheet.MCQPaperId = paper1.PaperId;

            AnswerSheetRepo.SetMCQAnswerSheet(answersheet);

            KeyValuePair<int, int> ret = new KeyValuePair<int, int>(TotalMarks, ObtainedMarks);
            return ret;
        }

        public AnswerSheetDTO GetAnswerSheet(string paperCode, string studentEmailId)
        {
            switch (CodeGenerator.GetPaperType(paperCode))
            {
                case EPaperType.MCQ:
                    var ans = AnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, studentEmailId);
                    var ret = Mapper.Map<MCQAnswerSheet, MCQAnswerSheetDTO>(ans);
                    if (ret != null)
                        ret.Paper = Mapper.Map<MCQPaper, MCQPaperDTO>(ans.MCQPaper);
                    return ans == null ? null : ret;
                case EPaperType.Descriptive:
                    var ans1 = DescriptiveAnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, studentEmailId);
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
            //System.Diagnostics.Debug.Print(answersheet.AnswerLink);
            DescriptiveAnswerSheetRepo.SetDescriptiveAnswerSheet(answersheet);
        }
    }
}
