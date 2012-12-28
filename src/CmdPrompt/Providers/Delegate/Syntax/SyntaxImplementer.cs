using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    internal class SyntaxImplementer : CommandSyntax.SyntaxImplementer
    {
        private readonly DelegateCommandDescriptionProvider mProvider;
        private DelegateCommandDescription mCommand;
        private DelegateCommandArgument mArgument;

        public SyntaxImplementer(DelegateCommandDescriptionProvider provider)
        {
            mProvider = provider;
        }

        protected override CommandSyntax.SyntaxImplementer InnerAddCommand(string name, string description = null)
        {
            mCommand = new DelegateCommandDescription(){Name = name, Description = description};
            mProvider.Add(mCommand);
            return this;
        }

        protected override CommandSyntax.SyntaxImplementer InnerWithArgument<T>(string name, string description = null)
        {
            mArgument = new DelegateCommandArgument() { Name = name, Description = description, Type = typeof(T)};
            mCommand.AddArgument(mArgument);
            return this;
        }

        protected override CommandSyntax.SyntaxImplementer InnerOnExecute(Action<CommandPrompt, IDictionary<string, object>> arguments)
        {
            mCommand.Executer = arguments;
            return this;
        }

        protected override CommandSyntax.SyntaxImplementer InnerIsOptional()
        {
            mArgument.IsOptional = true;
            return this;
        }

        protected override CommandSyntax.SyntaxImplementer InnerIsDefault()
        {
            mArgument.IsDefault = true;
            return this;
        }

        protected override CommandSyntax.SyntaxImplementer InnerWithAlias(string alias)
        {
            mArgument.AddAlias(alias);
            return this;
        }
    }
}