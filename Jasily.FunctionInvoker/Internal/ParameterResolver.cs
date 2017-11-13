using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.Internal
{
    internal abstract class ParameterResolver
    {
        protected internal ParameterResolver(ParameterInfo parameter)
        {
            this.Parameter = parameter;
        }

        public ParameterInfo Parameter { get; }

        public object ResolveArgumentObject<TArgumentsResolver>([NotNull] TArgumentsResolver resolver)
            where TArgumentsResolver : IArgumentsResolver
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            return resolver.Resolve(this.Parameter);
        }

        public static ParameterResolver Create(ParameterInfo parameter)
        {
            var type = typeof(ParameterResolver<>).MakeGenericType(parameter.ParameterType); // ref/out param will crash here.
            return (ParameterResolver)Activator.CreateInstance(type, parameter);
        }
    }

    internal sealed class ParameterResolver<T> : ParameterResolver
    {
        public ParameterResolver(ParameterInfo parameter) : base(parameter)
        {
        }

        public T ResolveArgument<TArgumentsResolver>([NotNull] TArgumentsResolver resolver)
            where TArgumentsResolver : IArgumentsResolver
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            return resolver.Resolve<T>(this.Parameter);
        }
    }
}