using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public class ArrayArgumentsResolver : BaseArgumentsResolver
    {
        private readonly object[] _arguments;

        public ArrayArgumentsResolver([NotNull] params object[] arguments)
        {
            this._arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public override object Resolve(ParameterInfo parameter)
        {
            return this._arguments[parameter.Position];
        }
    }
}