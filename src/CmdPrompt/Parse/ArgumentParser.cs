using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdPrompt
{
    public static class ArgumentParser
    {
        public static void Parse(string[] tokens, ArgumentValue[] arguments)
        {
            ThrowHelper.CheckAtMostOneDefault(arguments);
            ThrowHelper.CheckConvertible(arguments);

            Dictionary<ArgumentValue, int> argumentsToIndexes = 
                GetArgumentsToIndexes(tokens, arguments);

            foreach (var argumentsToIndex in argumentsToIndexes)
            {
                if (argumentsToIndex.Key.IsFlag)
                {
                    argumentsToIndex.Key.Value = true;
                }
                else
                {
                    string value =
                        ParseArgumentValue(tokens, argumentsToIndex, argumentsToIndexes);

                    argumentsToIndex.Key.Value =
                        Convert.ChangeType(value, argumentsToIndex.Key.Type);
                }
            }

            ThrowHelper.CheckNonOptional(arguments);
        }

        private static string ParseArgumentValue(string[] tokens, KeyValuePair<ArgumentValue, int> argumentsToIndex,
                                                 Dictionary<ArgumentValue, int> argumentsToIndexes)
        {
            string aliasAndValue = tokens[argumentsToIndex.Value];

            string value = null;

            int seperatorIndex = aliasAndValue.IndexOf(':');

            if ((argumentsToIndex.Value == 0) &&
                argumentsToIndex.Key.IsDefault &&
                !argumentsToIndex.Key.Aliases.Any
                     (x => aliasAndValue.StartsWith
                               (x,
                                StringComparison.InvariantCultureIgnoreCase)))
            {
                value = aliasAndValue;
            }
            else if (seperatorIndex > 0 && seperatorIndex < aliasAndValue.Length)
            {
                value = aliasAndValue.Substring(seperatorIndex + 1);

                value = NormalizeString(value);
            }
            else
            {
                int valueIndex = argumentsToIndex.Value + 1;

                if (valueIndex < tokens.Length)
                {
                    if (!argumentsToIndexes.Any(x => x.Value == valueIndex))
                    {
                        value = tokens[valueIndex];
                    }
                }
            }

            return value;
        }

        private static Dictionary<ArgumentValue, int> GetArgumentsToIndexes(string[] tokens, ArgumentValue[] arguments)
        {
            Dictionary<ArgumentValue, int> withoutDefault = 
                GetWithoutDefault(tokens, arguments);

            ArgumentValue defaultParameter =
                arguments.FirstOrDefault(x => x.IsDefault);

            if (tokens.Any() &&
                (defaultParameter != null) &&
                !withoutDefault.ContainsKey(defaultParameter) &&
                !withoutDefault.ContainsValue(0))
            {
                withoutDefault[defaultParameter] = 0;
            }

            return withoutDefault;
        }

        private static Dictionary<ArgumentValue, int> GetWithoutDefault(string[] tokens, ArgumentValue[] arguments)
        {
            var argumentsToIndexes =
                tokens.Select
                    ((token, index) =>
                     new
                         {
                             Index = index,
                             Argument = arguments.FirstOrDefault(
                                 argument => argument.Aliases.Any(
                                     alias => token.StartsWith(alias, StringComparison.InvariantCultureIgnoreCase)))
                         })
                      .Where(x => x.Argument != null)
                      .ToDictionary(x => x.Argument,
                                    x => x.Index);

            return argumentsToIndexes;
        }

        private static string NormalizeString(string value)
        {
            if (value.Length >= 2 &&
                (value.First() == '\"') &&
                (value.Last() == '\"'))
            {
                value = value.Trim('\"');
            }

            return value;
        }

        #region Utilities

        /// <remarks>
        /// From http://stackoverflow.com/a/467313
        /// </remarks>
        public static string[] SplitCommandLine(String argumentString)
        {
            StringBuilder translatedArguments = new StringBuilder(argumentString).Replace("\\\"", "\r");
            bool insideQuote = false;
            for (int i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    insideQuote = !insideQuote;
                }
                if (translatedArguments[i] == ' ' && !insideQuote)
                {
                    translatedArguments[i] = '\n';
                }
            }

            string[] toReturn = translatedArguments.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = RemoveMatchingQuotes(toReturn[i]);
                toReturn[i] = toReturn[i].Replace("\r", "\"");
            }
            return toReturn;
        }

        private static string RemoveMatchingQuotes(string stringToTrim)
        {
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }
            return stringToTrim;
        }

        #endregion

        private static class ThrowHelper
        {
            public static void CheckConvertible(ArgumentValue[] arguments)
            {
                ArgumentValue notConvertible =
                    arguments.FirstOrDefault(x => !x.IsOptional && !typeof (IConvertible).IsAssignableFrom(x.Type));

                if (notConvertible != null)
                {
                    throw new ArgumentException(
                        string.Format("The argument {0} is of type {1} which isn't convertible",
                                      notConvertible.Name,
                                      notConvertible.Type));
                }
            }

            public static void CheckNonOptional(ArgumentValue[] arguments)
            {
                ArgumentValue missingValue =
                    arguments.FirstOrDefault(x => !x.IsOptional && !x.HasValue);

                if (missingValue != null)
                {
                    throw new ArgumentException(
                        string.Format("The argument {0} isn't optional and has no value",
                                      missingValue.Name));
                }
            }

            public static void CheckAtMostOneDefault(ArgumentValue[] arguments)
            {
                if (arguments.Where(x => x.IsDefault).Skip(1).Any())
                {
                    throw new ArgumentException("There exists more than one default parameter");
                }
            }
        }
    }
}