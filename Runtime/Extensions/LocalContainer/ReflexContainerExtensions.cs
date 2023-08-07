using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Reflex.Core;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions
{
    public static class ReflexContainerExtensions
    {
        private static readonly ConditionalWeakTable<Container, Dictionary<Type, object>> _cache = new();

        public static IEnumerable<T> Get<T>(this Container container)
        {
            if (!_cache.TryGetValue(container, out Dictionary<Type, object> cacheMap))
            {
                cacheMap = new();
                _cache.Add(container, cacheMap);
            }

            if (!cacheMap.TryGetValue(typeof(IEnumerable<T>), out object result))
            {
                result = container.ProduceAllLocal<T>();
                cacheMap.Add(typeof(IEnumerable<T>), result);
            }

            return (IEnumerable<T>) cacheMap[typeof(IEnumerable<T>)];
        }

        private static IEnumerable<T> ProduceAllLocal<T>(this Container container)
        {
            return container.Parent == null
                ? container.All<T>()
                : container.All<T>().Except(container.Parent.All<T>());
        }
    }
}