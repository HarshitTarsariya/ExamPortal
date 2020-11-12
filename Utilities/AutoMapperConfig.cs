using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using System;

//Automapper automatically maps the models which reduces the code in controller
namespace ExamPortal.Utilities
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            /*PaperDTO <---> Paper auto mapping*/
            /*QuestionDTO <---> Question auto mapping*/

            CreateMap<PaperDTO, Paper>()
                .ForMember(RDest => RDest.PaperId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => Convert.ToDateTime(src.CreatedDate)))
                .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => Convert.ToDateTime(src.DeadLine)));
            CreateMap<Paper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => src.CreatedDate.ToString()))
                .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => src.DeadLine.ToString()));
            CreateMap<MCQPaper, PaperDTO>()
               .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.MCQ));
            CreateMap<DescriptivePaper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.Descriptive));


            CreateMap<Paper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.Ignore());
            CreateMap<MCQPaperDTO, MCQPaper>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore());
            CreateMap<MCQPaper, MCQPaperDTO>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.MCQ))
                .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.Ignore());


            CreateMap<QuestionDTO, Question>()
                .ForMember(RDest => RDest.QuestionId, LSrc => LSrc.Ignore());
            CreateMap<MCQQuestionDTO, MCQQuestion>()
                .ForMember(RDest => RDest.MCQPaperId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQPaper, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptionId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.TrueAnswer, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptions, LSrc => LSrc.Ignore());
            CreateMap<MCQQuestion,MCQQuestionDTO>();
            CreateMap<string, MCQOption>()
                .ForMember(RDest => RDest.OptionText, LSrc => LSrc.MapFrom(src => src))
                .ForMember(RDest => RDest.MCQOptionId, LSrc => LSrc.Ignore());
            CreateMap<MCQAnswerSheet, MCQAnswerSheetDTO>()
                .ForMember(RDest => RDest.Paper, LSrc => LSrc.MapFrom(src => src.MCQPaper))
                .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.Ignore());
            CreateMap<DescriptiveAnswerSheet, DescriptiveAnswerSheetDTO>()
                .ForMember(RDest => RDest.Paper, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.AnswerSheet, LSrc => LSrc.Ignore());
            CreateMap<DescriptivePaperDTO, DescriptivePaper>()
              .ForMember(RDest => RDest.Link, LSrc => LSrc.MapFrom(src => src.PaperPdfUrl));
            CreateMap<DescriptivePaper, DescriptivePaperDTO>()
              .ForMember(RDest => RDest.PaperPdfUrl, LSrc => LSrc.MapFrom(src => src.Link))
              .ForMember(RDest => RDest.paper, LSrc => LSrc.Ignore());

        }

    }
}
