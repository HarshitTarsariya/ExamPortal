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

            #region PaperDTO <--> Paper
            CreateMap<PaperDTO, Paper>()
               .ForMember(RDest => RDest.PaperId, LSrc => LSrc.Ignore())
               .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => Convert.ToDateTime(src.CreatedDate)))
               .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => Convert.ToDateTime(src.DeadLine)));
            CreateMap<Paper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => src.CreatedDate.ToString()))
                .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => src.DeadLine.ToString()));
            #endregion

            #region MCQPaperDTO <--> MCQPaper
            CreateMap<MCQPaperDTO, MCQPaper>()
                 .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore())
                 .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.Ignore());
            CreateMap<MCQPaper, MCQPaperDTO>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.MCQ));
            #endregion

            #region DescriptivePaperDTO <--> DescriptivePaper
            CreateMap<DescriptivePaperDTO, DescriptivePaper>()
               .ForMember(RDest => RDest.Link, LSrc => LSrc.MapFrom(src => src.PaperPdfUrl));
            CreateMap<DescriptivePaper, DescriptivePaperDTO>()
              .ForMember(RDest => RDest.PaperPdfUrl, LSrc => LSrc.MapFrom(src => src.Link))
              .ForMember(RDest => RDest.paper, LSrc => LSrc.Ignore());
            #endregion

            #region MCQQuestionDTO <--> MCQQuestion
            CreateMap<MCQQuestionDTO, MCQQuestion>()
                .ForMember(RDest => RDest.MCQPaperId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQPaper, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptionId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.TrueAnswer, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptions, LSrc => LSrc.Ignore());
            CreateMap<MCQQuestion, MCQQuestionDTO>();
            #endregion

            #region MCQPaper , DescriptivePaper --> PaperDTO
            CreateMap<MCQPaper, PaperDTO>()
               .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.MCQ));
            CreateMap<DescriptivePaper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.Descriptive));
            #endregion

            #region AnswerSheet , MCQAnswerSheet , DescriptiveAnswerSheet --> DTO
            CreateMap<MCQAnswerSheet, MCQAnswerSheetDTO>()
                .ForMember(RDest => RDest.Paper, LSrc => LSrc.MapFrom(src => src.MCQPaper));
            CreateMap<DescriptiveAnswerSheet, DescriptiveAnswerSheetDTO>()
                .ForMember(RDest => RDest.Paper, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.AnswerSheet, LSrc => LSrc.Ignore());
            CreateMap<AnswerSheet, AnswerSheetDTO>()
               .ForMember(RDest => RDest.paperdto, LSrc => LSrc.Ignore());
            #endregion


            CreateMap<QuestionDTO, Question>()
                .ForMember(RDest => RDest.QuestionId, LSrc => LSrc.Ignore());

            CreateMap<string, MCQOption>()
                .ForMember(RDest => RDest.OptionText, LSrc => LSrc.MapFrom(src => src))
                .ForMember(RDest => RDest.MCQOptionId, LSrc => LSrc.Ignore());

            #region MCQQuestionDTO <--> MCQQuestion
            CreateMap<MCQQuestion, MCQQuestionDTO>()
                .ConvertUsing((src, dest, context) =>
                {
                    dest = new MCQQuestionDTO();
                    dest.QuestionText = src.QuestionText;
                    dest.Marks = src.Marks;
                    var i = 0;
                    foreach (var opt in src.MCQOptions)
                    {
                        dest.Opetions.Add(opt.OptionText);
                        if (opt.MCQOptionId == src.TrueAnswer.MCQOptionId)
                            dest.TrueAnswer = i;
                        i++;
                    }
                    return dest;
                });
            CreateMap<MCQQuestionDTO, MCQQuestion>()
                .ConvertUsing((src, dest, context) =>
                {
                    dest = new MCQQuestion();
                    dest.QuestionText = src.QuestionText;
                    dest.Marks = src.Marks;
                    for (var i = 0; i < src.Opetions.Count; i++)
                    {
                        var opetion = context.Mapper.Map<string, MCQOption>(src.Opetions[i]);
                        dest.MCQOptions.Add(opetion);
                        if (i == src.TrueAnswer)
                            dest.TrueAnswer = opetion;
                    }
                    return dest;
                });
            #endregion

        }

    }
}
