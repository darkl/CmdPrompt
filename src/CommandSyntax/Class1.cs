using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdPrompt;
using Flunet.Attributes;

namespace CommandSyntax
{
    [Inherited]
    [Scope("CommandContainer")]
    public interface ICommandContainer
    {
        ICommand AddCommand(string name, string description = null);
    }

    [Inherited]
    [Scope("Command")]
    public interface ICommand
    {
        IArgument WithArgument<T>(string name, string description = null);
        
        [UniqueInScope("Command")]
        ICommand OnExecute(Action<CommandPrompt, IDictionary<string, object>> arguments);
    }

    [Scope("Argument")]
    public interface IArgument
    {
        [UniqueInScope("Argument")]
        IArgument IsOptional();
        [UniqueInScope("Command")]
        IArgument IsDefault();
        IArgument WithAlias(string alias);
    }
}
