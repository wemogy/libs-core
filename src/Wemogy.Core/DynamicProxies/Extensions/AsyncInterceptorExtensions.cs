using System;
using Castle.DynamicProxy;

namespace Wemogy.Core.DynamicProxies.Extensions
{
    public static class AsyncInterceptorExtensions
    {
        public static TInterface Wrap<TInterface>(
            this IAsyncInterceptor proxy,
            object target)
            where TInterface : class
        {
            var proxyGenerator = new ProxyGenerator();

            var wrappedImplementation = (TInterface)proxyGenerator.CreateInterfaceProxyWithTarget(
                typeof(TInterface),
                target,
                proxy.ToInterceptor());

            return wrappedImplementation;
        }

        public static object Wrap(
            this IAsyncInterceptor proxy,
            Type interfaceType,
            object target)
        {
            var proxyGenerator = new ProxyGenerator();

            var wrappedImplementation = proxyGenerator.CreateInterfaceProxyWithTarget(
                interfaceType,
                target,
                proxy.ToInterceptor());

            return wrappedImplementation;
        }
    }
}
