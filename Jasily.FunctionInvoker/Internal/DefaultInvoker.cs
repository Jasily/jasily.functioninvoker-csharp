using System.Reflection;

namespace Jasily.FunctionInvoker.Internal
{
    internal sealed class DefaultInvoker<TObject> : FunctionInvoker,
        IConstructorInvoker,
        IConstructorInvoker<TObject>
    {
        public DefaultInvoker() : base(null)
        {
        }

        public override bool IsCompiled => true;

        public ConstructorInfo Constructor => null;

        public TObject Invoke(IArgumentsResolver resolver)
        {
            return default(TObject);
        }

        object IConstructorInvoker.Invoke(IArgumentsResolver resolver)
        {
            return default(TObject);
        }
    }
}