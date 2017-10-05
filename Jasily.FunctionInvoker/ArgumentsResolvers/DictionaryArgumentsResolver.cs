using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public class DictionaryArgumentsResolver : BaseArgumentsResolver
    {
        private readonly IDictionary<string, object> _arguments;

        public DictionaryArgumentsResolver([NotNull] IDictionary<string, object> arguments)
        {
            this._arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public override object Resolve(ParameterInfo parameter)
        {
            if (this._arguments.TryGetValue(parameter.Name, out var val))
            {
                return val;
            }
            throw new ParameterResolveException(parameter);
        }
    }
}