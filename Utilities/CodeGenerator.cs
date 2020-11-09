using ExamPortal.DTOS;
using System;
using System.Collections.Generic;

namespace ExamPortal.Utilities
{
    //Generates the unique code based on the ticks of time prefixed with type of paper
    public class CodeGenerator
    {
        //EPaperType is Enum
        private static readonly Dictionary<EPaperType, string> PrefixDict = new Dictionary<EPaperType, string>
        {
            {EPaperType.Descriptive,"D_" },
            {EPaperType.MCQ,"M_" },
            {EPaperType.Invalid,"" }
        };

        public static string GetSharableCode(EPaperType type) => PrefixDict[type] + GetSharableCode();
        public static EPaperType GetPaperType(string code)
        {
            if (code.StartsWith(PrefixDict[EPaperType.MCQ])) return EPaperType.MCQ;
            if (code.StartsWith(PrefixDict[EPaperType.Descriptive])) return EPaperType.Descriptive;
            return EPaperType.Invalid;
        }
        private static string GetSharableCode()
        {
            long dt = DateTime.Now.Ticks;
            string codet = Convert.ToBase64String(BitConverter.GetBytes(dt)).TrimEnd('='), code = "";
            for (int i = 0; i < codet.Length; i++)
            {
                if (codet[i] == '+')
                    code += 'A';
                else if (codet[i] == '/')
                    code += 'B';
                else
                    code += codet[i];
                if (i == 3 || i == 7)
                    code += '-';
            }
            return code;
        }
    }
}
