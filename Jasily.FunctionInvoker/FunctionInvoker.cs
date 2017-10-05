using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Jasily.FunctionInvoker.Internal;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public abstract class FunctionInvoker : IFunctionInvoker
    {
        protected static readonly ParameterExpression ParameterArgumentsResolver;
        private static readonly MethodInfo MethodResolveArguments;

        internal const int CompileThreshold = 4;

        static FunctionInvoker()
        {
            ParameterArgumentsResolver = Expression.Parameter(typeof(IArgumentsResolver));

            MethodResolveArguments = typeof(FunctionInvoker)
                .GetRuntimeMethods()
                .Where(z => z.Name == nameof(ResolveArguments))
                .Single(z => z.GetParameters().Length == 2);
        }

        protected internal FunctionInvoker(MethodBase method)
        {
            this.Parameters = new ReadOnlyCollection<ParameterResolver> (method.GetParameters()
                .Select(ParameterResolver.Create)
                .ToArray());
        }

        [NotNull]
        internal IReadOnlyList<ParameterResolver> Parameters { get; }

        public abstract bool IsCompiled { get; }

        internal object[] ResolveArguments([NotNull] IArgumentsResolver resolver)
        {
            if (this.Parameters.Count == 0) return Array.Empty<object>();

            var length = this.Parameters.Count;
            var args = new object[length];
            for (var i = 0; i < length; i++)
            {
                args[i] = this.Parameters[i].ResolveArgumentObject(resolver);
            }
            return args;
        }

        private TArgument ResolveArguments<TArgument>([NotNull] IArgumentsResolver resolver, int index)
        {
            return ((ParameterResolver<TArgument>)this.Parameters[index]).ResolveArgument(resolver);
        }

        internal Expression[] ResolveArgumentsExpressions()
        {
            var exps = new Expression[this.Parameters.Count];
            for (var i = 0; i < this.Parameters.Count; i++)
            {
                var p = this.Parameters[i];
                var m = MethodResolveArguments.MakeGenericMethod(p.Parameter.ParameterType);
                exps[i] = Expression.Call(Expression.Constant(this), m, ParameterArgumentsResolver, Expression.Constant(i));
            }
            return exps;
        }

        [PublicAPI]
        public static IMethodInvoker CreateInvoker([NotNull] MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (method.ContainsGenericParameters) throw new InvalidOperationException("Method cannot contains generic parameters.");

            var reflectedType = method.ReflectedType;

            Type ResolveType()
            {
                if (method.IsStatic)
                {
                    return method.ReturnType == typeof(void)
                        ? typeof(StaticMethodInvoker)
                        : typeof(StaticMethodInvoker<>).MakeGenericType(method.ReturnType);
                }
                else
                {
                    return method.ReturnType == typeof(void)
                        ? typeof(ObjectMethodInvoker<>).MakeGenericType(reflectedType)
                        : typeof(ObjectMethodInvoker<,>).MakeGenericType(reflectedType, method.ReturnType);
                }
            }

            return (IMethodInvoker) Activator.CreateInstance(ResolveType(), method);
        }

        [PublicAPI]
        public static IConstructorInvoker CreateInvoker([NotNull] ConstructorInfo constructor)
        {
            if (constructor == null) throw new ArgumentNullException(nameof(constructor));
            if (constructor.ContainsGenericParameters) throw new InvalidOperationException("Method cannot contains generic parameters.");

            var reflectedType = constructor.ReflectedType;
            var type = typeof(ConstructorInvoker<>).MakeGenericType(reflectedType);

            return (IConstructorInvoker) Activator.CreateInstance(type, constructor);
        }
    }
}