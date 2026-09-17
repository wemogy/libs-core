using System;
using Castle.DynamicProxy;

namespace Wemogy.Core.DynamicProxies.Extensions
{
    public static class AsyncInterceptorExtensions
    {
        /// <summary>
        /// Shared on purpose. Every ProxyGenerator owns its own module scope, so a new instance per call
        /// emits a new dynamic assembly with a new proxy type each time — types that can never be
        /// unloaded. Wrapping per request (a scoped repository, for example) then leaks memory until
        /// the process is killed. One generator caches the proxy type per interface and is thread-safe.
        /// </summary>
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static TInterface Wrap<TInterface>(
            this IAsyncInterceptor proxy,
            object target)
            where TInterface : class
        {
            var wrappedImplementation = (TInterface)ProxyGenerator.CreateInterfaceProxyWithTarget(
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
            var wrappedImplementation = ProxyGenerator.CreateInterfaceProxyWithTarget(
                interfaceType,
                target,
                proxy.ToInterceptor());

            return wrappedImplementation;
        }
    }
}
