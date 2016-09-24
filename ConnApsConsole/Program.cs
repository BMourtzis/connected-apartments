using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            //for (int i = 0; i < 6 * random.NextDouble() + 8; i++)
            //{
            //    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(93 * random.NextDouble() + 33)));
            //    builder.Append(ch);
            //}

            for(int i = 0; i < 94; i++)
            {
                Console.WriteLine(i +" - "+ Convert.ToChar(Convert.ToInt32(i + 33)));
            }

            Console.ReadKey();
        }
    }
}
