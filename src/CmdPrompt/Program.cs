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
            DelegateCommandDescriptionProvider provider =
                new DelegateCommandDescriptionProvider();

            provider.AddCommand("Exit")
                    .OnExecute((cmd, dict) => Environment.Exit(0));

            provider.AddCommand("Write")
                    .WithArgument<string>("text")
                    .IsDefault()
                    .WithAlias("-text")
                    .WithAlias("/text:")
                    .WithArgument<bool>("yellow")
                    .WithAlias("/yellow")
                    .IsOptional()
                    .OnExecute((cmd, dict) =>
                                   {
                                       if (dict.ContainsKey("yellow"))
                                       {
                                           Console.ForegroundColor =
                                               ConsoleColor.Yellow;
                                       }

                                       Console.WriteLine(dict["text"]);

                                       if (dict.ContainsKey("yellow"))
                                       {
                                           Console.ForegroundColor =
                                               ConsoleColor.Gray;
                                       }
                                   });

            CommandPrompt prompt =
                new CommandPrompt(new List<ICommandDescriptionProvider>()
                                      {
                                          provider
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