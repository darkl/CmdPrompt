using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandPrompt prompt =
                new CommandPrompt(new List<ICommandDescriptionProvider>()
                                      {
                                          new TypeCommandDescriptionProvider()
                                              {
                                              }
                                      });

            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                prompt.Execute(line);
            }
        }
    }
}