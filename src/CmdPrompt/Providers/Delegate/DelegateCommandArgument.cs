using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    internal class DelegateCommandArgument : IArgumentDescription
    {
        private readonly ICollection<string> mAliases;

        public DelegateCommandArgument()
        {
            mAliases = new List<string>();
        }
        
        public string Name { get; set; }

        public IEnumerable<string> Aliases
        {
            get
            {
                return mAliases;
            }
        }

        public void AddAlias(string alias)
        {
            mAliases.Add(alias);
        }

        public string Description { get; set; }
        public bool IsOptional { get; set; }
        public bool IsDefault { get; set; }
        public Type Type { get; set; }
    }
}