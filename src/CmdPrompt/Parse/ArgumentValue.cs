using System;
using System.Collections.Generic;

namespace CmdPrompt
{
    public class ArgumentValue
    {
        private readonly Type mType;
        private readonly ICollection<string> mAliases;
        private object mValue;
        private readonly bool mIsOptional;
        private readonly bool mIsDefault;
        private readonly string mName;

        public ArgumentValue(string name, Type type, IEnumerable<string> aliases, bool isOptional, bool isDefault)
        {
            mName = name;
            mType = type;
            mIsOptional = isOptional;
            mIsDefault = isDefault;
            mAliases = new HashSet<string>(aliases, StringComparer.InvariantCultureIgnoreCase);
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }

        public Type Type
        {
            get
            {
                return mType;
            }
        }

        public IEnumerable<string> Aliases
        {
            get
            {
                return mAliases;
            }
        }

        public bool HasValue
        {
            get; 
            private set; 
        }

        public bool IsFlag
        {
            get
            {
                return (mType == typeof (bool));
            }
        }

        public bool IsOptional
        {
            get
            {
                return mIsOptional;
            }
        }

        public object Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                this.HasValue = true;
            }
        }

        public bool IsDefault
        {
            get
            {
                return mIsDefault;
            }
        }
    }
}