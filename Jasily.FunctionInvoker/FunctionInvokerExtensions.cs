using System;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public static class FunctionInvokerExtensions
    {
        public static IObjectMethodInvoker AsObjectMethodInvoker([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker) invoker;
        }

        public static IObjectMethodInvoker<TObject> AsObjectMethodInvoker<TObject>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker<TObject>) invoker;
        }

        public static IObjectMethodInvoker<TObject, TResult> AsObjectMethodInvoker<TObject, TResult>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker<TObject, TResult>) invoker;
        }

        public static IStaticMethodInvoker AsStaticMethodInvoker([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IStaticMethodInvoker) invoker;
        }

        public static IStaticMethodInvoker<TResult> AsStaticMethodInvoker<TResult>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IStaticMethodInvoker<TResult>) invoker;
        }

        public static IConstructorInvoker<TInstance> AsConstructorInvoker<TInstance>([NotNull] this IConstructorInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IConstructorInvoker<TInstance>) invoker;
        }
    }
}