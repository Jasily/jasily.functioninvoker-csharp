using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public interface IObjectMethodInvoker : IMethodInvoker
    {
        /// <summary>
        /// Invoke the instance method.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="resolver"></param>
        /// <exception cref="ArgumentNullException">throw if <paramref name="instance"/> or <paramref name="resolver"/> is null.</exception>
        /// <returns></returns>
        object Invoke([NotNull] object instance, [NotNull] IArgumentsResolver resolver);
    }

    public interface IObjectMethodInvoker<in TObject> : IMethodInvoker
    {
        /// <summary>
        /// Invoke the instance method.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="resolver"></param>
        void Invoke([NotNull] TObject instance, [NotNull] IArgumentsResolver resolver);
    }

    public interface IObjectMethodInvoker<in TObject, out TResult> : IMethodInvoker
    {
        /// <summary>
        /// Invoke the instance method.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        TResult Invoke([NotNull] TObject instance, [NotNull] IArgumentsResolver resolver);
    }
}