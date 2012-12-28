using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    internal class DelegateCommand : ICommand
    {
        private readonly CommandPrompt mCmd;
        private readonly IDictionary<string, object> mArguments;
        private readonly Action<CommandPrompt, IDictionary<string, object>> mExecuter;

        public DelegateCommand(CommandPrompt cmd, IDictionary<string, object> arguments, Action<CommandPrompt, IDictionary<string, object>> executer)
        {
            mCmd = cmd;
            mArguments = arguments;
            mExecuter = executer;
        }

        public void Execute()
        {
            mExecuter(mCmd, mArguments);
        }
    }
}