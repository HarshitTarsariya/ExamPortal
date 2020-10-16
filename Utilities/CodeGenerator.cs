using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
    public class CodeGenerator
    {
        public static string GetSharableCode()
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
