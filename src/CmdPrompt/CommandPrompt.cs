using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdPrompt
{
    public class CommandPrompt
    {
        private readonly CommandContainer mContainer;

        public CommandPrompt(IEnumerable<ICommandDescriptionProvider> providers)
        {
            mContainer = new CommandContainer(providers);
        }

        public void Execute(string cmd)
        {
            string commandName = ParseCommandName(cmd);

            ICommandDescription description = mContainer.GetCommandDescription(commandName);

            if (description == null)
            {
                WriteError(string.Format("A command named {0} isn't available", commandName));
            }
            else
            {
                try
                {
                    IDictionary<string, object> arguments = ParseArguments(cmd, description);
                    ICommand command = description.BuildCommand(new CommandDetails(this, arguments));
                    command.Execute();
                }
                catch (Exception ex)
                {
                    WriteError(string.Format("Failed executing command {0}. Error: {1}",
                                             commandName,
                                             ex));
                }                
            }
        }

        private IDictionary<string, object> ParseArguments(string cmd, ICommandDescription description)
        {
            string[] tokens =
                ArgumentParser.SplitCommandLine(cmd).Skip(1).ToArray();

            ArgumentValue[] arguments =
                description.Arguments.Select(x => new ArgumentValue(x.Name, x.Type, x.Aliases, x.IsOptional, x.IsDefault))
                           .ToArray();

            ArgumentParser.Parse(tokens, arguments);

            return arguments.ToDictionary(x => x.Name, x => x.Value);
        }

        private string ParseCommandName(string cmd)
        {
            string trimmed = cmd.Trim();

            int spaceIndex = trimmed.IndexOf(' ');

            if (spaceIndex <= 0)
            {
                return cmd;
            }
            else
            {
                return cmd.Substring(0, spaceIndex);
            }
        }

        public CommandContainer Commands
        {
            get
            {
                return mContainer;
            }
        }

        public void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Write(string line)
        {
            Console.WriteLine(line);
        }
    }
}