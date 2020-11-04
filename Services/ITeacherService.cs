using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using ExamPortal.Repositories;
using ExamPortal.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Services
{
    public interface ITeacherService
    {
        /// <summary>
        /// save paper to database and returns sharable code
        /// </summary>
        public string CreatePaper(MCQPaperDTO paper);
        /// <summary>
        /// return paper associated with given code.
        /// </summary>
        public MCQPaperDTO getPaperByCode(string code);
        public List<MCQPaperDTO> getPapersByEmailId(string emailId);
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string Name);
        public MCQAnswerSheet GetMCQAnswerSheetByCodeAndEmail(string paperCode, string Name);
        public Task<string> CreateDescriptivePaper(DescriptivePaperDTO DesPaper);
    }

    public class TeacherServiceImpl : ITeacherService
    {
        public TeacherServiceImpl(IMapper mapper, IMCQPaperRepo paperRepo
            , IMCQAnswerSheetRepo answerSheetRepo, IFirebaseUpload fire,IDescriptivePaperRepo descriptivePaperRepo)
        {
            Mapper = mapper;
            PaperRepo = paperRepo;
            AnswerSheetRepo = answerSheetRepo;
            Fire = fire;
            DescriptivePaperRepo = descriptivePaperRepo;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo PaperRepo { get; }
        public IMCQAnswerSheetRepo AnswerSheetRepo { get; }
        public IFirebaseUpload Fire { get; }
        public IDescriptivePaperRepo DescriptivePaperRepo { get; }

        public string CreatePaper(MCQPaperDTO paper)
        {
            string code = CodeGenerator.GetSharableCode();
            MCQPaper mcqPaper = Mapper.Map<MCQPaperDTO, MCQPaper>(paper);
            mcqPaper.PaperCode = code;
            foreach (var que in paper.Questions)
                mcqPaper.Questions.Add(que.DtoTOEntity());
            PaperRepo.Create(mcqPaper);
            return code;
        }

        public MCQAnswerSheet GetMCQAnswerSheetByCodeAndEmail(string paperCode, string Name)
        {
            return AnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, Name);
        }

        public MCQPaperDTO getPaperByCode(string code)
        {
            var paper = PaperRepo.GetByPaperCode(code);
            var paperdto = Mapper.Map<MCQPaper, MCQPaperDTO>(paper);
            foreach (var que in paper.Questions)
                paperdto.Questions.Add(que.EntityToDto());
            return paperdto;
        }

        public List<MCQPaperDTO> getPapersByEmailId(string emailId)
        {
            var ans = Mapper.Map<IEnumerable<MCQPaper>, List<MCQPaperDTO>>(PaperRepo.GetByTeacherEmail(emailId));
            return ans;
        }

        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto, string Name)
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
                if (mcqpaperdto.Questions[i].TrueAnswer == paper.Questions[i].TrueAnswer)
                    ObtainedMarks += mcqpaperdto.Questions[i].Marks;
            }
            answersheet.MarksObtained = ObtainedMarks;
            answersheet.StudentEmailId = Name;
            answersheet.SubmittedTime = DateTime.Now;
            answersheet.MCQPaperId = paper1.Id;

            AnswerSheetRepo.SetMCQAnswerSheet(answersheet);

            KeyValuePair<int, int> ret = new KeyValuePair<int, int>(TotalMarks, ObtainedMarks);
            return ret;
        }

        public async Task<string> CreateDescriptivePaper(DescriptivePaperDTO DesPaper)
        {
            string code = CodeGenerator.GetSharableCode(); ;
            DesPaper.PaperCode = code;
            string linkwith = await Fire.Upload(DesPaper), link = "";

            for (int i = 0; i < linkwith.Length; i++)   //Sql Database Cannot Store Special Character like '&' , So  storing '&' == "EPF"
            {
                link += (linkwith[i] == '&') ? Fire.Ampersand : $"{linkwith[i]}";
            }
            DescriptivePaper paper = Mapper.Map<DescriptivePaperDTO, DescriptivePaper>(DesPaper);
            paper.Link = link;
            DescriptivePaperRepo.Create(paper);

            return code;
        }
    }
}
