using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Cortera_Project_2_StringDLL
{
    public class CLeaning_Operation_DLL
    {
        public static string Clean_Operation(string Incomming_String)
        {   Incomming_String = Incomming_String.ToUpper();
            Incomming_String = Regex.Replace(Incomming_String, @"[\s\t]+", " ");
            Incomming_String = Regex.Replace(Incomming_String, @"[^A-Za-z0-9,#.]", " "); 
            Incomming_String.Trim(); 
            return Incomming_String;
        }

        public static int Clean_Operation_Marks(string Marks)
        {   int result;
            bool res = int.TryParse(Marks,out result);
            if(res)
            {
                return result;
            }
            else
            {
                return result = 0;
            }
              
        }


    }
}
