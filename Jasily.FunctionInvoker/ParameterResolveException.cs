using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public class ParameterResolveException : Exception
    {
        public ParameterResolveException([NotNull] ParameterInfo parameter)
        {
            this.Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public ParameterInfo Parameter { get; }
    }
}