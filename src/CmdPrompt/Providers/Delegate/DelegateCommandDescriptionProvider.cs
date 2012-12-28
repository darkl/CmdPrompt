using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CmdPrompt
{
    public class DelegateCommandDescriptionProvider : ICommandDescriptionProvider
    {
        private IDictionary<string, DelegateCommandDescription> mCommandNameToDescription = new Dictionary<string, DelegateCommandDescription>();

        public DelegateCommandDescriptionProvider()
        {
        }

        public ICommandDescription[] GetCommands()
        {
            return mCommandNameToDescription.Values.OfType<ICommandDescription>().ToArray();
        }

        internal void Add(DelegateCommandDescription command)
        {
            mCommandNameToDescription.Add(command.Name, command);
        }
    }
}