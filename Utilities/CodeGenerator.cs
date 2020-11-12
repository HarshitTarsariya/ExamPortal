using ExamPortal.DTOS;
using System;
using System.Collections.Generic;

namespace ExamPortal.Utilities
{
    //Generates the unique code based on the ticks of time prefixed with type of paper
    public class CodeGenerator
    {
        static readonly DateTime StartDate = new DateTime(2000, 1, 1);
        //EPaperType is Enum
        private static readonly Dictionary<EPaperType, string> PrefixDict = new Dictionary<EPaperType, string>
        {
            {EPaperType.Descriptive,"D_" },
            {EPaperType.MCQ,"M_" },
            {EPaperType.Invalid,"" }
        };

        public static string GetSharableCode(EPaperType type)
        {
            Func<DateTime, string> GetSharableCode = (start) =>
                Convert
               .ToBase64String(BitConverter.GetBytes((start < DateTime.Now) ? DateTime.Now.Ticks - start.Ticks : DateTime.Now.Ticks))
               .TrimEnd('=')
               .Replace('+', 'A')
               .Replace('/', 'B')
               .Insert(4, "-")
               .Insert(9, "-");
            return PrefixDict[type] + GetSharableCode(StartDate);
        }

        public static EPaperType GetPaperType(string code)
        {
            if (code.StartsWith(PrefixDict[EPaperType.MCQ])) return EPaperType.MCQ;
            if (code.StartsWith(PrefixDict[EPaperType.Descriptive])) return EPaperType.Descriptive;
            return EPaperType.Invalid;
        }
    }
}
