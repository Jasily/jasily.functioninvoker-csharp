﻿using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.Internal
{
    internal abstract class MethodInvoker : FunctionInvoker,
        IMethodInvoker
    {
        protected bool _isCompiled;

        protected MethodInvoker(MethodInfo method) : base(method)
        {
            this.Method = method;
        }

        public MethodInfo Method { get; }

        protected object InvokeMethod(object instance, [NotNull] IArgumentsResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            var arguments = this.ResolveArguments(resolver);

            try
            {
                return this.Method.Invoke(instance, arguments);
            }
            catch (TargetInvocationException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        public override bool IsCompiled => this._isCompiled || Volatile.Read(ref this._isCompiled);
    }
}