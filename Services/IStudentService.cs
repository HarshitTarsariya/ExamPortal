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
    public interface IStudentService
    {
        public MCQPaper GetMcqPaper(string code);
        
    }

    public class StudentServiceImpl : IStudentService
    {
        public StudentServiceImpl(IMapper mapper , IMCQPaperRepo paperRepo, SignInManager<IdentityUser> signInManager)
        {
            Mapper = mapper;
            PaperRepo = paperRepo;
            SignInManager = signInManager;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo PaperRepo { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public MCQPaper GetMcqPaper(string code)
        {
            MCQPaper paper = PaperRepo.GetByPaperCode(code);
            if (paper == null)
                return null;

            return paper;
        }
    }
}
