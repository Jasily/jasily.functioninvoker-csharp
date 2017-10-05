using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Jasily.FunctionInvoker.ArgumentsResolvers
{
    public class ServiceProviderArgumentsResolver : BaseArgumentsResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderArgumentsResolver([NotNull] IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public override object Resolve(ParameterInfo parameter)
        {
            return this._serviceProvider.GetService(parameter.ParameterType);
        }
    }
}