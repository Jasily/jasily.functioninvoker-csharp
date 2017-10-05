using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public interface IArgumentsResolver
    {
        object Resolve([NotNull] ParameterInfo parameter);

        /// <summary>
        /// Provide a way to allow customized <see cref="IArgumentsResolver"/> to avoid boxing if possible.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        T Resolve<T>([NotNull] ParameterInfo parameter);
    }
}