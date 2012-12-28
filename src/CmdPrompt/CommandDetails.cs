using System.Collections.Generic;

namespace CmdPrompt
{
    public class CommandDetails
    {
        private readonly CommandPrompt mPrompt;
        private readonly IDictionary<string, object> mArguments;

        public CommandDetails(CommandPrompt prompt, IDictionary<string, object> arguments)
        {
            mPrompt = prompt;
            mArguments = arguments;
        }

        public CommandPrompt Prompt
        {
            get
            {
                return mPrompt;
            }
        }

        public IDictionary<string, object> Arguments
        {
            get
            {
                return mArguments;
            }
        }
    }
}