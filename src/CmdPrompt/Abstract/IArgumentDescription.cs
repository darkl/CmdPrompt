using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    public interface IArgumentDescription
    {
        string Name { get; }
        IEnumerable<string> Aliases { get; }
        string Description { get; }
        bool IsOptional { get; }
        bool IsDefault { get; }
        Type Type { get; }
    }
}