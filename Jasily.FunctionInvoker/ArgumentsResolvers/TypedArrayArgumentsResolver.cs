using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    /// <summary>
    /// A reuseable arguments resolver for avoid boxing.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "()}")]
    public class TypedArrayArgumentsResolver : IArgumentsResolver
    {
        private abstract class Box
        {
            public abstract object GetValue();
        }

        [DebuggerDisplay("{" + nameof(Value) + "}")]
        private class Box<T> : Box
        {
            public T Value;

            public override object GetValue()
            {
                return this.Value;
            }
        }

        private readonly Box[] _boxes;

        public TypedArrayArgumentsResolver(int capacity)
        {
            this._boxes = new Box[capacity];
        }

        public void SetValue<T>(int index, T value)
        {
            if (this._boxes[index] is Box<T> box)
            {
                box.Value = value;
            }
            else
            {
                this._boxes[index] = new Box<T> { Value = value };
            }
        }

        public void SetDefault<T>(int index)
        {
            if (this._boxes[index] is Box<T> box)
            {
                box.Value = default(T);
            }
            else
            {
                this._boxes[index] = null;
            }
        }

        public object Resolve(ParameterInfo parameter)
        {
            return this._boxes[parameter.Position]?.GetValue();
        }

        public T Resolve<T>(ParameterInfo parameter)
        {
            var box = this._boxes[parameter.Position];
            return box == null ? default(T) : ((Box<T>)box).Value; // for unset value, use default.
        }

        private object DebuggerDisplay()
        {
            return ArrayArgumentsResolver.GetDebuggerDisplay(this._boxes.Select(z => z?.GetValue()).ToArray());
        }

        public static TypedArrayArgumentsResolver Create<T>(T value)
        {
            var resolver = new TypedArrayArgumentsResolver(1);
            resolver.SetValue(0, value);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T0, T1>(T0 value0, T1 value1)
        {
            var resolver = new TypedArrayArgumentsResolver(2);
            resolver.SetValue(0, value0);
            resolver.SetValue(1, value1);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T0, T1, T2>(T0 value0, T1 value1, T2 value2)
        {
            var resolver = new TypedArrayArgumentsResolver(3);
            resolver.SetValue(0, value0);
            resolver.SetValue(1, value1);
            resolver.SetValue(2, value2);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T0, T1, T2, T3>(T0 value0, T1 value1, T2 value2, T3 value3)
        {
            var resolver = new TypedArrayArgumentsResolver(4);
            resolver.SetValue(0, value0);
            resolver.SetValue(1, value1);
            resolver.SetValue(2, value2);
            resolver.SetValue(3, value3);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T0, T1, T2, T3, T4>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var resolver = new TypedArrayArgumentsResolver(5);
            resolver.SetValue(0, value0);
            resolver.SetValue(1, value1);
            resolver.SetValue(2, value2);
            resolver.SetValue(3, value3);
            resolver.SetValue(4, value4);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T0, T1, T2, T3, T4, T5>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var resolver = new TypedArrayArgumentsResolver(6);
            resolver.SetValue(0, value0);
            resolver.SetValue(1, value1);
            resolver.SetValue(2, value2);
            resolver.SetValue(3, value3);
            resolver.SetValue(4, value4);
            resolver.SetValue(5, value5);
            return resolver;
        }

        public static TypedArrayArgumentsResolver Create<T>([NotNull] T[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            var resolver = new TypedArrayArgumentsResolver(arguments.Length);
            for (var i = 0; i < arguments.Length; i++)
            {
                resolver.SetValue(i, arguments[i]);
            }
            return resolver;
        }
    }
}