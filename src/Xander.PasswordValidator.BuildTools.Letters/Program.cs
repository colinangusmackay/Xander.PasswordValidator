using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xander.PasswordValidator.BuildTools.Letters
{
    class Program
    {
        public const int startIndex = 128;
        public const int lastIndex = 255;
        static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("[a-zA-Z");
            for (int i = startIndex; i <= lastIndex; i++)
            {
                char c = (char) i;
                if (char.IsLetter(c))
                {
                    sb.Append("\\u");
                    sb.Append(i.ToString("X4"));
                }
            }
            sb.Append("]");
            Console.WriteLine("Done!");
            string result = sb.ToString();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
