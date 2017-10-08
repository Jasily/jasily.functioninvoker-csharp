using System;
using System.Linq;
using System.Reflection;
using Jasily.FunctionInvoker.ArgumentsResolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jasily.FunctionInvoker.Test
{
    [TestClass]
    public class StructUnitTest : BaseUnitTest
    {
        [TestMethod]
        public override void TestNonArgsHasValueInstanceMethod()
        {
            var method = typeof(int)
                .GetRuntimeMethods()
                .Single(z => z.Name == nameof(int.GetHashCode) && z.GetParameters().Length == 0);
            var invoker = FunctionInvoker.CreateInvoker(method);

            InternalAssert(invoker, 27745.GetHashCode(), () => invoker.AsObjectMethodInvoker().Invoke(27745));

            InternalAssert(invoker, 27745.GetHashCode(), () => invoker.AsObjectMethodInvoker<int, int>().Invoke(27745));

            Assert.ThrowsException<InvalidCastException>(() => invoker.AsObjectMethodInvoker<object, int>());
            Assert.ThrowsException<InvalidCastException>(() => invoker.AsObjectMethodInvoker<int, object>());
        }

        [TestMethod]
        public override void TestHasArgsHasValueInstanceMethod()
        {
            var method = typeof(int)
                .GetRuntimeMethods()
                .Single(z => z.Name == nameof(int.CompareTo) && z.GetParameters().Length == 1 && z.GetParameters()[0].ParameterType == typeof(int));
            var invoker = FunctionInvoker.CreateInvoker(method);

            InternalAssert(invoker, 27745.CompareTo(1), () => invoker.AsObjectMethodInvoker().Invoke(27745, new object[] { 1 }));
            // ReSharper disable once RedundantExplicitArrayCreation
            InternalAssert(invoker, 27745.CompareTo(1), () => invoker.AsObjectMethodInvoker().Invoke(27745, new int[] { 1 }));

            InternalAssert(invoker, 27745.CompareTo(1), () => invoker.AsObjectMethodInvoker<int, int>().Invoke(27745, new object[] { 1 }));
            // ReSharper disable once RedundantExplicitArrayCreation
            InternalAssert(invoker, 27745.CompareTo(1), () => invoker.AsObjectMethodInvoker<int, int>().Invoke(27745, new int[] { 1 }));

            Assert.ThrowsException<InvalidCastException>(() => invoker.AsObjectMethodInvoker<object, int>());
            Assert.ThrowsException<InvalidCastException>(() => invoker.AsObjectMethodInvoker<int, object>());
        }

        [TestMethod]
        public override void TestNonArgsNonValueInstanceMethod()
        {
            // TODO
        }

        [TestMethod]
        public override void TestHasArgsNonValueInstanceMethod()
        {
            // TODO
        }

        [TestMethod]
        public override void TestNonArgsHasValueStaticMethod()
        {
            // TODO
        }

        [TestMethod]
        public override void TestHasArgsHasValueStaticMethod()
        {
            var method = typeof(int)
                .GetRuntimeMethods()
                .Single(z => z.Name == nameof(int.Parse) && z.GetParameters().Length == 1 && z.GetParameters()[0].ParameterType == typeof(string));
            var invoker = FunctionInvoker.CreateInvoker(method);

            InternalAssert(invoker, int.Parse("1"), () => invoker.AsStaticMethodInvoker().Invoke(new object[] { "1" }));
            // ReSharper disable once RedundantExplicitArrayCreation
            InternalAssert(invoker, int.Parse("1"), () => invoker.AsStaticMethodInvoker().Invoke(new string[] { "1" }));

            InternalAssert(invoker, int.Parse("1"), () => invoker.AsStaticMethodInvoker<int>().Invoke(new object[] { "1" }));
            // ReSharper disable once RedundantExplicitArrayCreation
            InternalAssert(invoker, int.Parse("1"), () => invoker.AsStaticMethodInvoker<int>().Invoke(new string[] { "1" }));
            
            Assert.ThrowsException<InvalidCastException>(() => invoker.AsStaticMethodInvoker<object>());
        }

        [TestMethod]
        public override void TestNonArgsNonValueStaticMethod()
        {
            // TODO
        }

        [TestMethod]
        public override void TestHasArgsNonValueStaticMethod()
        {
            // TODO
        }

        public override void TestNonArgsConstructor()
        {
            // ignore, see `TestDefault`.
        }

        struct MyStruct
        {
            public int Value { get; }

            public MyStruct(int val) => this.Value = val;
        }

        [TestMethod]
        public override void TestHasArgsConstructor()
        {
            var invoker = FunctionInvoker.CreateInvoker(typeof(MyStruct).GetConstructors().Single());
            InternalAssert(invoker, new MyStruct(3).Value, () => ((MyStruct) invoker.Invoke(new object[] { 3 })).Value);
            InternalAssert(invoker, new MyStruct(4).Value, () => ((MyStruct) invoker.Invoke(new int[] { 4 })).Value);
            InternalAssert(invoker, new MyStruct(5).Value, () => invoker.AsConstructorInvoker<MyStruct>().Invoke(new object[] { 5 }).Value);
            InternalAssert(invoker, new MyStruct(6).Value, () => invoker.AsConstructorInvoker<MyStruct>().Invoke(new int[] { 6 }).Value);
        }

        [TestMethod]
        public override void TestDefault()
        {
            var invoker = FunctionInvoker.CreateDefaultInvoker(typeof(int));
            InternalAssert(invoker, default(int), () => invoker.Invoke());
            InternalAssert(invoker, default(int), () => invoker.AsConstructorInvoker<int>().Invoke());
        }
    }
}
