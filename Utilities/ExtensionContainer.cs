using ExamPortal.DTOS;
using ExamPortal.Repositories;
using ExamPortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ExamPortal.Models;

namespace ExamPortal.Utilities
{
    public static class ExtensionContainer
    {
        public static IServiceCollection configureDI(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherServiceImpl>();
            services.AddScoped<IStudentService, StudentServiceImpl>();
            services.AddScoped<IMCQPaperRepo, MCQPaperRepoImpl>();
            services.AddScoped<IMCQAnswerSheetRepo, MCQAnswerSheetRepoImpl>();
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddScoped<IFirebaseUpload, FirebaseUpload>();
            services.AddScoped<IDescriptivePaperRepo, DescriptivePaperRepoImpl>();
            return services;
        }

        public static void configureAuth(this IServiceCollection services)
        {
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "566313563077-ejb0mq4u9bnku8itadmc8jgov17u1e1p.apps.googleusercontent.com";
                options.ClientSecret = "QKT74XsgR5NC1oLxVjswn4M8";
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();
        }

        public static MCQQuestion DtoTOEntity(this MCQQuestionDTO questionDTO)
        {
            MCQQuestion question = new MCQQuestion();
            question.QuestionText = questionDTO.QuestionText;
            question.Marks = questionDTO.Marks;
            for (var i = 0; i < questionDTO.Opetions.Count(); i++)
            {
                var opetion = new MCQOption() { OptionText = questionDTO.Opetions[i] };
                question.MCQOptions.Add(opetion);
                if (i == questionDTO.TrueAnswer)
                    question.TrueAnswer = opetion;
            }
            return question;
        }

        public static MCQQuestionDTO EntityToDto(this MCQQuestion question)
        {
            MCQQuestionDTO question1 = new MCQQuestionDTO();
            question1.QuestionText = question.QuestionText;
            question1.Marks = question.Marks;
            var i = 0;
            foreach (var opt in question.MCQOptions)
            {
                question1.Opetions.Add(opt.OptionText);
                if (opt.Id == question.TrueAnswer.Id)
                    question1.TrueAnswer = i;
                i++;
            }
            return question1;
        }
    }
}
