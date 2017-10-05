using System;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public interface IStaticMethodInvoker : IMethodInvoker
    {
        /// <summary>
        /// Invoke the static method.
        /// </summary>
        /// <param name="resolver"></param>
        /// <exception cref="ArgumentNullException">throw if <paramref name="resolver"/> is null.</exception>
        /// <returns></returns>
        object Invoke([NotNull] IArgumentsResolver resolver);
    }

    public interface IStaticMethodInvoker<out TResult> : IMethodInvoker
    {
        /// <summary>
        /// Invoke the static method.
        /// </summary>
        /// <param name="resolver"></param>
        /// <exception cref="ArgumentNullException">throw if <paramref name="resolver"/> is null.</exception>
        /// <returns></returns>
        TResult Invoke([NotNull] IArgumentsResolver resolver);
    }
}