using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Jasily.FunctionInvoker.Internal
{
    internal sealed class ObjectMethodInvoker<TObject> : MethodInvoker,
        IObjectMethodInvoker,
        IObjectMethodInvoker<TObject>,
        IObjectMethodInvoker<TObject, object>
    {
        private readonly bool _isValueType;
        private Action<TObject, IArgumentsResolver> _func;

        public ObjectMethodInvoker(MethodInfo method) : base(method)
        {
            this._isValueType = typeof(TObject).IsValueType;
            this._func = this.ImplFunc();
        }

        object IObjectMethodInvoker.Invoke(object instance, IArgumentsResolver resolver)
        {
            this.Invoke((TObject) instance, resolver);
            return null;
        }

        public void Invoke(TObject instance, IArgumentsResolver resolver)
        {
            if (!this._isValueType && Equals(instance, null)) throw new ArgumentNullException(nameof(instance));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            this._func(instance, resolver);
        }

        object IObjectMethodInvoker<TObject, object>.Invoke(TObject instance, IArgumentsResolver resolver)
        {
            this.Invoke(instance, resolver);
            return null;
        }

        private Action<TObject, IArgumentsResolver> ImplFunc()
        {
            if (CompileThreshold <= 0) return this.CompileFunc();
            var count = 0;
            return (instance, resolver) =>
            {
                if (Interlocked.Increment(ref count) == CompileThreshold)
                {
                    Task.Run(() =>
                    {
                        Volatile.Write(ref this._func, this.CompileFunc());
                        Volatile.Write(ref this._isCompiled, true);
                    });
                }

                this.InvokeMethod(instance, resolver);
            };
        }

        private Action<TObject, IArgumentsResolver> CompileFunc()
        {
            var instance = Expression.Parameter(typeof(TObject));

            Expression body = this.Parameters.Count == 0
                ? Expression.Call(instance, this.Method)
                : Expression.Call(instance, this.Method, this.ResolveArgumentsExpressions());

            return Expression.Lambda<Action<TObject, IArgumentsResolver>>(body,
                instance, ParameterArgumentsResolver
            ).Compile();
        }
    }

    internal sealed class ObjectMethodInvoker<TObject, TResult> : MethodInvoker,
        IObjectMethodInvoker,
        IObjectMethodInvoker<TObject>,
        IObjectMethodInvoker<TObject, TResult>
    {
        private readonly bool _isValueType;
        private Func<TObject, IArgumentsResolver, TResult> _func;

        public ObjectMethodInvoker(MethodInfo method) : base(method)
        {
            this._isValueType = typeof(TObject).IsValueType;
            this._func = this.ImplFunc();
        }

        object IObjectMethodInvoker.Invoke(object instance, IArgumentsResolver resolver) => this.Invoke((TObject)instance, resolver);

        void IObjectMethodInvoker<TObject>.Invoke(TObject instance, IArgumentsResolver resolver) => this.Invoke(instance, resolver);

        public TResult Invoke(TObject instance, IArgumentsResolver resolver)
        {
            if (!this._isValueType && Equals(instance, null)) throw new ArgumentNullException(nameof(instance));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            return this._func(instance, resolver);
        }

        private Func<TObject, IArgumentsResolver, TResult> ImplFunc()
        {
            if (CompileThreshold <= 0) return this.CompileFunc();
            var count = 0;
            return (instance, resolver) =>
            {
                if (Interlocked.Increment(ref count) == CompileThreshold)
                {
                    Task.Run(() =>
                    {
                        Volatile.Write(ref this._func, this.CompileFunc());
                        Volatile.Write(ref this._isCompiled, true);
                    });
                }

                return (TResult)this.InvokeMethod(instance, resolver);
            };
        }

        private Func<TObject, IArgumentsResolver, TResult> CompileFunc()
        {
            var instance = Expression.Parameter(typeof(TObject));

            Expression body = this.Parameters.Count == 0
                ? Expression.Call(instance, this.Method)
                : Expression.Call(instance, this.Method, this.ResolveArgumentsExpressions());

            return Expression.Lambda<Func<TObject, IArgumentsResolver, TResult>>(body,
                instance, ParameterArgumentsResolver
            ).Compile();
        }
    }
}