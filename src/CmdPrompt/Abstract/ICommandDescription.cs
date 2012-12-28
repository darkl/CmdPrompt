using System.Collections.Generic;

namespace CmdPrompt
{
    public interface ICommandDescription
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<IArgumentDescription> Arguments { get; }
        ICommand BuildCommand(CommandDetails commandDetails);
    }
}