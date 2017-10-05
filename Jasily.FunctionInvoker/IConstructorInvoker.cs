using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public interface IConstructorInvoker : IFunctionInvoker
    {
        /// <summary>
        /// Return null if the <see cref="IConstructorInvoker"/> is create from <see cref="FunctionInvoker.CreateDefaultInvoker"/>.
        /// </summary>
        [CanBeNull]
        ConstructorInfo Constructor { get; }

        object Invoke([NotNull] IArgumentsResolver resolver);
    }

    public interface IConstructorInvoker<out TInstance> : IConstructorInvoker
    {
        new TInstance Invoke([NotNull] IArgumentsResolver resolver);
    }
}