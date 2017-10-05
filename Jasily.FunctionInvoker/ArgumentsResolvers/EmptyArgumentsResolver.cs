using System;
using System.Reflection;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public class EmptyArgumentsResolver : BaseArgumentsResolver
    {
        public static readonly EmptyArgumentsResolver Instance = new EmptyArgumentsResolver();

        private EmptyArgumentsResolver() { }

        public override object Resolve(ParameterInfo parameter)
        {
            throw new InvalidOperationException($"Cannot resolve any argument from <{nameof(EmptyArgumentsResolver)}>");
        }
    }
}