# Jasily.FunctionInvoker

Provide a way to dynamic invoke compiled method.

## Usage

Namespaces:

``` cs
using Jasily.FunctionInvoker;
using Jasily.FunctionInvoker.ArgumentsResolvers;
```

### For Methods

For example, the static method `public static String Join(String separator, IEnumerable<String> values);`.

``` cs
var method = var method = typeof(string).GetMethods()
    .Where(z =>
        z.Name == "Join" &&
        z.GetParameters().Length == 2 &&
        z.GetParameters()[1].ParameterType == typeof(IEnumerable<String>)
    ).Single();
var invoker = FunctionInvoker.CreateInvoker(method).AsStaticMethodInvoker();
invoker.Invoke(new object[] { "s", new string[] { "3", "4" } }); // output: "3s4"
```

#### Avoid boxing

Try to avoid return value boxing ?

For example, if the return value is `int`,
you can just replace

`var invoker = FunctionInvoker.CreateInvoker(method).AsStaticMethodInvoker();`

to

`var invoker = FunctionInvoker.CreateInvoker(method).AsStaticMethodInvoker<int>();`

#### All kind of invoker

`invoker` should be:

* `IStaticMethodInvoker` - For any static methods.
* `IStaticMethodInvoker<T>` - For known return type.
* `IObjectMethodInvoker` - For any instance methods.
* `IObjectMethodInvoker<TObject>` - For known instance type.
* `IObjectMethodInvoker<TObject, TResult>` - For known instance type and return type.

### For Constructor

``` cs
ConstructorInfo constructor = /* I don't care where do you get this. */;
var invoker = FunctionInvoker.CreateInvoker(constructor);
invoker.Invoke(); // a new instance.
```

### For `default(T)`

Still use `Activator.CreateInstance(typeof(int));` ?

Try this for avoid boxing:

``` cs
var invoker = FunctionInvoker.CreateDefaultInvoker(typeof(int)).AsConstructorInvoker<int>();
invoker.Invoke(); // 0
```
