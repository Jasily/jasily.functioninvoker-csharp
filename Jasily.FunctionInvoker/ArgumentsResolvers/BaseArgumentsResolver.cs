using System;
using System.Reflection;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public abstract class BaseArgumentsResolver : IArgumentsResolver
    {
        public abstract object Resolve(ParameterInfo parameter);

        public T Resolve<T>(ParameterInfo parameter)
        {
            return (T)this.Resolve(parameter);
        }
    }
}