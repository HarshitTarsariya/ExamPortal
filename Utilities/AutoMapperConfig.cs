using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using System;
using System.Globalization;

//Automapper automatically maps the models which reduces the code in controller
namespace ExamPortal.Utilities
{

    public class AutoMapperConfig : Profile
    {
        public static string DateFormat => "MM/dd/yyyy h:mm tt";

        public AutoMapperConfig()
        {
            /*PaperDTO <---> Paper auto mapping*/
            /*QuestionDTO <---> Question auto mapping*/

            #region PaperDTO <--> Paper
            CreateMap<PaperDTO, Paper>()
               .ForMember(RDest => RDest.PaperId, LSrc => LSrc.Ignore())
               .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.MapFrom(src => src.TotalMarks))
            .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.CreatedDate, DateFormat, CultureInfo.InvariantCulture)))
            .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.DeadLine, DateFormat, CultureInfo.InvariantCulture)));

            CreateMap<Paper, PaperDTO>()
                .ForMember(RDest => RDest.Type, LSrc => LSrc.Ignore())
               .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.MapFrom(src => src.TotalMarks))
            .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => src.CreatedDate.ToString(DateFormat, CultureInfo.InvariantCulture)))
            .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => src.DeadLine.ToString(DateFormat, CultureInfo.InvariantCulture)));
            #endregion

            #region MCQPaperDTO <--> MCQPaper
            CreateMap<MCQPaperDTO, MCQPaper>()
                 .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.CreatedDate, DateFormat, CultureInfo.InvariantCulture)))
                 .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.DeadLine, DateFormat, CultureInfo.InvariantCulture)))
                 .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore())
                 .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.Ignore());
            CreateMap<MCQPaper, MCQPaperDTO>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.Type, LSrc => LSrc.MapFrom(src => EPaperType.MCQ))
                .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => src.CreatedDate.ToString(DateFormat, CultureInfo.InvariantCulture)))
            .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => src.DeadLine.ToString(DateFormat, CultureInfo.InvariantCulture)));
            #endregion

            #region DescriptivePaperDTO <--> DescriptivePaper
            CreateMap<DescriptivePaperDTO, DescriptivePaper>()
               .ForMember(RDest => RDest.Link, LSrc => LSrc.MapFrom(src => src.PaperPdfUrl))
                .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.CreatedDate, DateFormat, CultureInfo.InvariantCulture)))
            .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => DateTime.ParseExact(src.DeadLine, DateFormat, CultureInfo.InvariantCulture)));
            CreateMap<DescriptivePaper, DescriptivePaperDTO>()
              .ForMember(RDest => RDest.PaperPdfUrl, LSrc => LSrc.MapFrom(src => src.Link))
              .ForMember(RDest => RDest.paper, LSrc => LSrc.Ignore())
               .ForMember(RDest => RDest.CreatedDate, LSrc => LSrc.MapFrom(src => src.CreatedDate.ToString(DateFormat, CultureInfo.InvariantCulture)))
            .ForMember(RDest => RDest.DeadLine, LSrc => LSrc.MapFrom(src => src.DeadLine.ToString(DateFormat, CultureInfo.InvariantCulture)));
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
