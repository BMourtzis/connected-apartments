using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnApsEmailService;

namespace ConnApsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var mailSerivce = new EmailService();
            mailSerivce.SendTestEmail("bill.mourtzis@gmail.com");
        }
    }
}
