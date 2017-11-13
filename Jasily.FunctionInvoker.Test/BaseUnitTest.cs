using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jasily.FunctionInvoker.Test
{
    public abstract class BaseUnitTest
    {
        public static void InternalAssert<T>(IFunctionInvoker invoker, T expected, Func<T> actual)
        {
            while (!invoker.IsCompiled)
            {
                Assert.AreEqual(expected, actual());
            }
            // compiled
            Assert.AreEqual(expected, actual());
        }
        
        public abstract void TestNonArgsHasValueInstanceMethod();
        
        public abstract void TestHasArgsHasValueInstanceMethod();
        
        public abstract void TestNonArgsNonValueInstanceMethod();
        
        public abstract void TestHasArgsNonValueInstanceMethod();
        
        public abstract void TestNonArgsHasValueStaticMethod();
        
        public abstract void TestHasArgsHasValueStaticMethod();
        
        public abstract void TestNonArgsNonValueStaticMethod();
        
        public abstract void TestHasArgsNonValueStaticMethod();

        public abstract void TestNonArgsConstructor();

        public abstract void TestHasArgsConstructor();

        public abstract void TestDefault();

        public abstract void TestInstancePropertyAccessors();

        public abstract void TestStaticPropertyAccessors();
    }
}