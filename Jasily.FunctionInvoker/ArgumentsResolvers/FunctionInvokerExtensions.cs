using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public static class FunctionInvokerExtensions
    {
        public static object Invoke([NotNull] this IObjectMethodInvoker invoker,
            [NotNull] object instance)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, EmptyArgumentsResolver.Instance);
        }

        public static object Invoke([NotNull] this IObjectMethodInvoker invoker, 
            [NotNull] object instance, [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            return invoker.Invoke(instance, new ArrayArgumentsResolver(arguments));
        }

        public static object Invoke<TArgument>([NotNull] this IObjectMethodInvoker invoker,
            [NotNull] object instance, [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            return invoker.Invoke(instance, TypedArrayArgumentsResolver.Create(arguments));
        }

        public static object Invoke([NotNull] this IObjectMethodInvoker invoker,
            [NotNull] object instance, [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, new DictionaryArgumentsResolver(arguments));
        }

        public static object Invoke([NotNull] this IObjectMethodInvoker invoker,
            [NotNull] object instance, [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, new ServiceProviderArgumentsResolver(arguments));
        }

        public static void Invoke<TObject>([NotNull] this IObjectMethodInvoker<TObject> invoker,
            [NotNull] TObject instance)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            invoker.Invoke(instance, EmptyArgumentsResolver.Instance);
        }

        public static void Invoke<TObject>([NotNull] this IObjectMethodInvoker<TObject> invoker,
            [NotNull] TObject instance, [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            invoker.Invoke(instance, new ArrayArgumentsResolver(arguments));
        }

        public static void Invoke<TObject, TArgument>([NotNull] this IObjectMethodInvoker<TObject> invoker,
            [NotNull] TObject instance, [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            invoker.Invoke(instance, TypedArrayArgumentsResolver.Create(arguments));
        }

        public static void Invoke<TObject>([NotNull] this IObjectMethodInvoker<TObject> invoker,
            [NotNull] TObject instance, [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            invoker.Invoke(instance, new DictionaryArgumentsResolver(arguments));
        }

        public static void Invoke<TObject>([NotNull] this IObjectMethodInvoker<TObject> invoker,
            [NotNull] TObject instance, [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            invoker.Invoke(instance, new ServiceProviderArgumentsResolver(arguments));
        }

        public static TResult Invoke<TObject, TResult>([NotNull] this IObjectMethodInvoker<TObject, TResult> invoker,
            [NotNull] TObject instance)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, EmptyArgumentsResolver.Instance);
        }

        public static TResult Invoke<TObject, TResult>([NotNull] this IObjectMethodInvoker<TObject, TResult> invoker,
            [NotNull] TObject instance, [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, new ArrayArgumentsResolver(arguments));
        }

        public static TResult Invoke<TObject, TArgument, TResult>([NotNull] this IObjectMethodInvoker<TObject, TResult> invoker,
            [NotNull] TObject instance, [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, TypedArrayArgumentsResolver.Create(arguments));
        }

        public static TResult Invoke<TObject, TResult>([NotNull] this IObjectMethodInvoker<TObject, TResult> invoker,
            [NotNull] TObject instance, [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, new DictionaryArgumentsResolver(arguments));
        }

        public static TResult Invoke<TObject, TResult>([NotNull] this IObjectMethodInvoker<TObject, TResult> invoker,
            [NotNull] TObject instance, [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(instance, new ServiceProviderArgumentsResolver(arguments));
        }

        public static object Invoke([NotNull] this IStaticMethodInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(EmptyArgumentsResolver.Instance);
        }

        public static object Invoke([NotNull] this IStaticMethodInvoker invoker,
            [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ArrayArgumentsResolver(arguments));
        }

        public static object Invoke<TArgument>([NotNull] this IStaticMethodInvoker invoker,
            [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(TypedArrayArgumentsResolver.Create(arguments));
        }

        public static object Invoke([NotNull] this IStaticMethodInvoker invoker,
            [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new DictionaryArgumentsResolver(arguments));
        }

        public static object Invoke([NotNull] this IStaticMethodInvoker invoker,
            [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ServiceProviderArgumentsResolver(arguments));
        }

        public static TResult Invoke<TResult>([NotNull] this IStaticMethodInvoker<TResult> invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(EmptyArgumentsResolver.Instance);
        }

        public static TResult Invoke<TResult>([NotNull] this IStaticMethodInvoker<TResult> invoker,
            [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ArrayArgumentsResolver(arguments));
        }

        public static TResult Invoke<TArgument, TResult>([NotNull] this IStaticMethodInvoker<TResult> invoker,
            [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(TypedArrayArgumentsResolver.Create(arguments));
        }

        public static TResult Invoke<TResult>([NotNull] this IStaticMethodInvoker<TResult> invoker,
            [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new DictionaryArgumentsResolver(arguments));
        }

        public static TResult Invoke<TResult>([NotNull] this IStaticMethodInvoker<TResult> invoker,
            [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ServiceProviderArgumentsResolver(arguments));
        }

        public static object Invoke([NotNull] this IConstructorInvoker invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(EmptyArgumentsResolver.Instance);
        }

        public static object Invoke([NotNull] this IConstructorInvoker invoker,
            [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ArrayArgumentsResolver(arguments));
        }

        public static object Invoke<TArgument>([NotNull] this IConstructorInvoker invoker,
            [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(TypedArrayArgumentsResolver.Create(arguments));
        }

        public static object Invoke([NotNull] this IConstructorInvoker invoker,
            [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new DictionaryArgumentsResolver(arguments));
        }

        public static object Invoke([NotNull] this IConstructorInvoker invoker,
            [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ServiceProviderArgumentsResolver(arguments));
        }

        public static TInstance Invoke<TInstance>([NotNull] this IConstructorInvoker<TInstance> invoker)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(EmptyArgumentsResolver.Instance);
        }

        public static TInstance Invoke<TInstance>([NotNull] this IConstructorInvoker<TInstance> invoker,
            [NotNull] object[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ArrayArgumentsResolver(arguments));
        }

        public static TInstance Invoke<TArgument, TInstance>([NotNull] this IConstructorInvoker<TInstance> invoker,
            [NotNull] TArgument[] arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(TypedArrayArgumentsResolver.Create(arguments));
        }

        public static TInstance Invoke<TInstance>([NotNull] this IConstructorInvoker<TInstance> invoker,
            [NotNull] IDictionary<string, object> arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new DictionaryArgumentsResolver(arguments));
        }

        public static TInstance Invoke<TInstance>([NotNull] this IConstructorInvoker<TInstance> invoker,
            [NotNull] IServiceProvider arguments)
        {
            if (invoker == null) throw new ArgumentNullException(nameof(invoker));
            return invoker.Invoke(new ServiceProviderArgumentsResolver(arguments));
        }
    }
}