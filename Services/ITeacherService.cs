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
        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto,string Name);
        public MCQAnswerSheet GetMCQAnswerSheetByCodeAndEmail(string paperCode, string Name);
    }

    public class TeacherServiceImpl : ITeacherService
    {
        public TeacherServiceImpl(IMapper mapper, IMCQPaperRepo paperRepo, SignInManager<IdentityUser> signInManager,IStudentService studentService,IMCQAnswerSheetRepo mCQAnswerSheetRepo)
        {
            Mapper = mapper;
            PaperRepo = paperRepo;
            SignInManager = signInManager;
            StudentService = studentService;
            MCQAnswerSheetRepo = mCQAnswerSheetRepo;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo PaperRepo { get; }
        IStudentService StudentService { get; }
        IMCQAnswerSheetRepo MCQAnswerSheetRepo { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

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

        public KeyValuePair<int, int> SetMCQAnswerSheet(MCQPaperDTO mcqpaperdto,string Name)
        {
            MCQAnswerSheet answersheet = new MCQAnswerSheet();
            MCQPaper paper = StudentService.GetMcqPaper(mcqpaperdto.PaperCode);
            int TotalMarks = 0, ObtainedMarks = 0;
            for(int i=0;i< paper.Questions.Count;i++)
            {
                TotalMarks += paper.Questions[i].Marks;
                foreach(var op in paper.Questions[i].MCQOptions)
                {
                    if(op.Id == paper.Questions[i].MCQOptionId && op.OptionText == mcqpaperdto.Questions[i].Opetions[mcqpaperdto.Questions[i].TrueAnswer])
                    {
                        ObtainedMarks += mcqpaperdto.Questions[i].Marks;
                    }
                }
            }
            answersheet.MarksObtained = ObtainedMarks;
            answersheet.StudentEmailId= Name;
            answersheet.SubmittedTime = DateTime.Now;
            answersheet.MCQPaperId = paper.Id;

            MCQAnswerSheetRepo.SetMCQAnswerSheet(answersheet);
            
            KeyValuePair<int, int> ret = new KeyValuePair<int, int>(TotalMarks, ObtainedMarks);
            return ret;
        }
        public MCQAnswerSheet GetMCQAnswerSheetByCodeAndEmail(string paperCode, string Name)
        {
            return MCQAnswerSheetRepo.GetByPaperCodeAndStudentEmail(paperCode, Name);
        }
    }
}
