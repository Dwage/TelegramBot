using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot.Source.Logger
{
    class MessageStorage
    {
        public async Task Save(string message)
        {
            try
            {
                using (StreamWriter logFile = new StreamWriter(GetLogFilePath(), true))
                {
                    await logFile.WriteLineAsync(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string GetLogFilePath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string relativePath = @"..\..\..\Log.txt";

            return Path.Combine(appDir, relativePath);
        }
    }
}
