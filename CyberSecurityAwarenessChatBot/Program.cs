using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
This method is the entry point of the application. It creates an instance of the CyberSecurityBot class and starts it. 
The bot will then run and provide cybersecurity awareness information to users.
*/
namespace CyberSecurityAwarenessChatBot
{
    internal class Program // The Program class contains the Main method, which is the entry point of the application.
    {
        static void Main(string[] args)
        {
            CyberSecurityBot bot = new CyberSecurityBot();// Create an instance of the CyberSecurityBot class
            bot.Start();// Start the bot, which will run and provide cybersecurity awareness information to users.
        }
    }
}
