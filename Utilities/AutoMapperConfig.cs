using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            /*PaperDTO <---> Paper auto mapping*/
            /*QuestionDTO <---> Question auto mapping*/

            CreateMap<PaperDTO, Paper>()
                .ForMember(RDest => RDest.Id, LSrc => LSrc.Ignore());
            CreateMap<MCQPaperDTO, MCQPaper>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore());
            CreateMap<MCQPaper, MCQPaperDTO>()
                .ForMember(RDest => RDest.Questions, LSrc => LSrc.Ignore());
            CreateMap<QuestionDTO, Question>()
                .ForMember(RDest => RDest.Id, LSrc => LSrc.Ignore());
            CreateMap<MCQQuestionDTO, MCQQuestion>()
                .ForMember(RDest => RDest.MCQPaperId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQPaper, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptionId, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.TrueAnswer, LSrc => LSrc.Ignore())
                .ForMember(RDest => RDest.MCQOptions, LSrc => LSrc.Ignore());
            CreateMap<string, MCQOption>()
                .ForMember(RDest => RDest.OptionText, LSrc => LSrc.MapFrom(src => src))
                .ForMember(RDest => RDest.Id, LSrc => LSrc.Ignore());
            CreateMap<MCQAnswerSheet, MCQAnswerSheetDTO>()
                .ForMember(RDest => RDest.MCQPaper, LSrc => LSrc.MapFrom(src => src.MCQPaper))
                .ForMember(RDest => RDest.TotalMarks, LSrc => LSrc.Ignore());
            CreateMap<DescriptivePaperDTO, DescriptivePaper>()
              .ForMember(RDest => RDest.Link, LSrc => LSrc.Ignore());
        }

    }
}
