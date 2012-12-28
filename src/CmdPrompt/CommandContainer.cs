using System;
using System.Collections;
using System.Collections.Generic;

namespace CmdPrompt
{
    public class CommandContainer : IEnumerable<ICommandDescription>
    {
        private readonly IDictionary<string, ICommandDescription> mCommandNameToDescription; 

        public CommandContainer(IEnumerable<ICommandDescriptionProvider> providers)
        {
            mCommandNameToDescription = new Dictionary<string, ICommandDescription>(StringComparer.InvariantCultureIgnoreCase);

            foreach (ICommandDescriptionProvider provider in providers)
            {
                ICommandDescription[] currentCommands = provider.GetCommands();

                foreach (ICommandDescription command in currentCommands)
                {
                    if (mCommandNameToDescription.ContainsKey(command.Name))
                    {
                        throw new CommandNameNotUniqueException(command.Name);
                    }

                    mCommandNameToDescription[command.Name] = command;
                }
            }
        }

        public ICommandDescription GetCommandDescription(string name)
        {
            ICommandDescription result;

            if (!mCommandNameToDescription.TryGetValue(name, out result))
            {
                return null;
            }

            return result;
        }

        public IEnumerator<ICommandDescription> GetEnumerator()
        {
            return mCommandNameToDescription.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}