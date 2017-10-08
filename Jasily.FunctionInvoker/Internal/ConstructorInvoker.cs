using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.Internal
{
    internal sealed class ConstructorInvoker<TObject> : FunctionInvoker,
        IConstructorInvoker, 
        IConstructorInvoker<TObject>
    {
        private Func<IArgumentsResolver, TObject> _func;
        private bool _isCompiled;

        public ConstructorInvoker(ConstructorInfo constructor) : base(constructor)
        {
            this.Constructor = constructor;
            this._func = this.ImplFunc();
        }

        public ConstructorInfo Constructor { get; }

        private object InvokeMethod([NotNull] IArgumentsResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            var a = this.Parameters.Count == 0 ? null : this.ResolveArguments(resolver);

            try
            {
                return this.Constructor.Invoke(a);
            }
            catch (TargetInvocationException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        public TObject Invoke(IArgumentsResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            return this._func(resolver);
        }

        object IConstructorInvoker.Invoke(IArgumentsResolver resolver) => this.Invoke(resolver);

        private Func<IArgumentsResolver, TObject> ImplFunc()
        {
            if (CompileThreshold <= 0) return this.CompileFunc();
            var count = 0;
            return resolver =>
            {
                if (Interlocked.Increment(ref count) == CompileThreshold)
                {
                    Task.Run(() =>
                    {
                        Volatile.Write(ref this._func, this.CompileFunc());
                        Volatile.Write(ref this._isCompiled, true);
                    });
                }

                return (TObject) this.InvokeMethod(resolver);
            };
        }

        private Func<IArgumentsResolver, TObject> CompileFunc()
        {
            Expression body = this.Parameters.Count == 0
                ? Expression.New(this.Constructor)
                : Expression.New(this.Constructor, this.ResolveArgumentsExpressions());

            return Expression.Lambda<Func<IArgumentsResolver, TObject>>(body,
                ParameterArgumentsResolver
            ).Compile();
        }

        public override bool IsCompiled => this._isCompiled || Volatile.Read(ref this._isCompiled);
    }
}