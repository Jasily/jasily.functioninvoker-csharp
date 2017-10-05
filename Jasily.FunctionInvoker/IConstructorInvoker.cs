using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker
{
    public interface IConstructorInvoker : IFunctionInvoker
    {
        ConstructorInfo Constructor { get; }

        object Invoke([NotNull] IArgumentsResolver resolver);
    }

    public interface IConstructorInvoker<out TInstance> : IConstructorInvoker
    {
        new TInstance Invoke([NotNull] IArgumentsResolver resolver);
    }
}