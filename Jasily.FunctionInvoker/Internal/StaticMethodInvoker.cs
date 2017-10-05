using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Jasily.FunctionInvoker.Internal
{
    internal class StaticMethodInvoker : MethodInvoker,
        IStaticMethodInvoker, 
        IStaticMethodInvoker<object>
    {
        private Action<IArgumentsResolver> _func;

        public StaticMethodInvoker(MethodInfo method) : base(method)
        {
            this._func = this.ImplFunc();
        }

        public object Invoke(IArgumentsResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            this._func(resolver);
            return null;
        }

        private Action<IArgumentsResolver> ImplFunc()
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

                this.InvokeMethod(null, resolver);
            };
        }

        private Action<IArgumentsResolver> CompileFunc()
        {
            Expression body = this.Parameters.Count == 0
                ? Expression.Call(this.Method)
                : Expression.Call(this.Method, this.ResolveArgumentsExpressions());

            return Expression.Lambda<Action<IArgumentsResolver>>(body, ParameterArgumentsResolver).Compile();
        }
    }

    internal class StaticMethodInvoker<TResult> : MethodInvoker,
        IStaticMethodInvoker,
        IStaticMethodInvoker<TResult>
    {
        private Func<IArgumentsResolver, TResult> _func;

        public StaticMethodInvoker(MethodInfo method) : base(method)
        {
            this._func = this.ImplFunc();
        }

        public TResult Invoke(IArgumentsResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            return this._func(resolver);
        }

        object IStaticMethodInvoker.Invoke(IArgumentsResolver resolver) => this.Invoke(resolver);

        private Func<IArgumentsResolver, TResult> ImplFunc()
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

                return (TResult)this.InvokeMethod(null, resolver);
            };
        }

        private Func<IArgumentsResolver, TResult> CompileFunc()
        {
            Expression body = this.Parameters.Count == 0
                ? Expression.Call(this.Method)
                : Expression.Call(this.Method, this.ResolveArgumentsExpressions());

            return Expression.Lambda<Func<IArgumentsResolver, TResult>>(body, ParameterArgumentsResolver).Compile();
        }
    }
}