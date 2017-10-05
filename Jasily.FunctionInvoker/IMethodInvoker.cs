using System.Reflection;

namespace Jasily.FunctionInvoker
{
    public interface IMethodInvoker : IFunctionInvoker
    {
        MethodInfo Method { get; }
    }
}