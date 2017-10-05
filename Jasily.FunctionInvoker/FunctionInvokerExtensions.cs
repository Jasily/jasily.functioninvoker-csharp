using System;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public static class FunctionInvokerExtensions
    {
        [NotNull]
        public static IObjectMethodInvoker AsObjectMethodInvoker([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker) invoker;
        }

        [NotNull]
        public static IObjectMethodInvoker<TObject> AsObjectMethodInvoker<TObject>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker<TObject>) invoker;
        }

        [NotNull]
        public static IObjectMethodInvoker<TObject, TResult> AsObjectMethodInvoker<TObject, TResult>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IObjectMethodInvoker<TObject, TResult>) invoker;
        }

        [NotNull]
        public static IStaticMethodInvoker AsStaticMethodInvoker([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IStaticMethodInvoker) invoker;
        }

        [NotNull]
        public static IStaticMethodInvoker<TResult> AsStaticMethodInvoker<TResult>([NotNull] this IMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IStaticMethodInvoker<TResult>) invoker;
        }

        [NotNull]
        public static IConstructorInvoker<TInstance> AsConstructorInvoker<TInstance>([NotNull] this IConstructorInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return (IConstructorInvoker<TInstance>) invoker;
        }

        public static bool IsStaticMethodInvoker([CanBeNull] this IMethodInvoker invoker)
        {
            return invoker is IStaticMethodInvoker;
        }

        public static bool IsObjectMethodInvoker([CanBeNull] this IMethodInvoker invoker)
        {
            return invoker is IObjectMethodInvoker;
        }
    }
}