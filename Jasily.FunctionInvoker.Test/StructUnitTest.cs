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

        struct HasArgsStruct
        {
            public int Value { get; set; }

            public HasArgsStruct(int val)
            {
                this.Value = val;
            }

            public static int ConstVal => 1998;
        }

        [TestMethod]
        public override void TestHasArgsConstructor()
        {
            var invoker = FunctionInvoker.CreateInvoker(typeof(HasArgsStruct).GetConstructors().Single());
            InternalAssert(invoker, new HasArgsStruct(3).Value, () => ((HasArgsStruct) invoker.Invoke(new object[] { 3 })).Value);
            InternalAssert(invoker, new HasArgsStruct(4).Value, () => ((HasArgsStruct) invoker.Invoke(new int[] { 4 })).Value);
            InternalAssert(invoker, new HasArgsStruct(5).Value, () => invoker.AsConstructorInvoker<HasArgsStruct>().Invoke(new object[] { 5 }).Value);
            InternalAssert(invoker, new HasArgsStruct(6).Value, () => invoker.AsConstructorInvoker<HasArgsStruct>().Invoke(new int[] { 6 }).Value);
        }

        [TestMethod]
        public override void TestDefault()
        {
            var invoker = FunctionInvoker.CreateDefaultInvoker(typeof(int));
            InternalAssert(invoker, default(int), () => invoker.Invoke());
            InternalAssert(invoker, default(int), () => invoker.AsConstructorInvoker<int>().Invoke());
        }

        [TestMethod]
        public override void TestInstancePropertyAccessors()
        {
            var property = typeof(HasArgsStruct).GetRuntimeProperty(nameof(HasArgsStruct.Value));
            
            var getterInvoker = FunctionInvoker.CreateInvoker(property.GetMethod);
            InternalAssert(getterInvoker, new HasArgsStruct(3).Value, 
                () => getterInvoker.AsObjectMethodInvoker().Invoke(new HasArgsStruct(3)));
            InternalAssert(getterInvoker, new HasArgsStruct(4).Value, 
                () => getterInvoker.AsObjectMethodInvoker<HasArgsStruct, int>().Invoke(new HasArgsStruct(4)));

            // value type setter alawys not work.
        }

        public override void TestStaticPropertyAccessors()
        {
            // this should same as reference type static property accessors.
        }
    }
}
