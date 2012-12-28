using System;
using System.Runtime.Serialization;

namespace CmdPrompt
{
    [Serializable]
    public class CommandNameNotUniqueException : Exception
    {
        public CommandNameNotUniqueException()
        {
        }

        public CommandNameNotUniqueException(string name) :
            this(name, null)
        {
        }

        public CommandNameNotUniqueException(string name, Exception inner)
            : base(string.Format("A command named {0} was registered by more than one command provider",
                                 name), inner)
        {
        }

        protected CommandNameNotUniqueException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}