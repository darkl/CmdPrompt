using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    internal class DelegateCommandDescription : ICommandDescription
    {
        private readonly List<IArgumentDescription> mArguments; 

        public DelegateCommandDescription()
        {
            mArguments = new List<IArgumentDescription>();
        }

        public Action<CommandPrompt, IDictionary<string, object>> Executer { get; set; } 
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public IEnumerable<IArgumentDescription> Arguments
        {
            get
            {
                return mArguments;
            }
        }
        
        public void AddArgument(DelegateCommandArgument argument)
        {
            mArguments.Add(argument);
        }

        public ICommand BuildCommand(CommandDetails commandDetails)
        {
            return new DelegateCommand(commandDetails.Prompt, commandDetails.Arguments, Executer);
        }
    }
}