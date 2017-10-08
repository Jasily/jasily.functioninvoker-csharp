using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "()}")]
    public class ArrayArgumentsResolver : BaseArgumentsResolver
    {
        private readonly object[] _arguments;

        public ArrayArgumentsResolver([NotNull] object[] arguments)
        {
            this._arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public override object Resolve(ParameterInfo parameter)
        {
            return this._arguments[parameter.Position];
        }

        private object DebuggerDisplay()
        {
            return GetDebuggerDisplay(this._arguments);
        }

        internal static object GetDebuggerDisplay([NotNull] object[] objs)
        {
            switch (objs.Length)
            {
                case 0: return Array.Empty<object>();
                case 1: return Tuple.Create(objs[0]);
                case 2: return Tuple.Create(objs[0],objs[1]);
                case 3: return Tuple.Create(objs[0],objs[1],objs[2]);
                case 4: return Tuple.Create(objs[0],objs[1],objs[2],objs[3]);
                case 5: return Tuple.Create(objs[0],objs[1],objs[2],objs[3],objs[4]);
                case 6: return Tuple.Create(objs[0],objs[1],objs[2],objs[3],objs[4],objs[5]);
                default: return objs;
            }
        }
    }
}