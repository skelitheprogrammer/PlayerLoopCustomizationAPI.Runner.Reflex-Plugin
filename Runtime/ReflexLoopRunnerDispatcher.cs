using System;
using System.Linq;
using PlayerLoopCustomizationAPI.Addons.Runner.Implementation;
using PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions;
using Reflex.Core;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin
{
    public sealed class ReflexLoopRunnerDispatcher : IDisposable, IStartable
    {
        private readonly Container _container;
        private readonly CompositeDispatcherDisposable _disposable;

        public ReflexLoopRunnerDispatcher(Container container)
        {
            _container = container;
            _disposable = new();
        }

        public void Start()
        {
            Dispatch();
        }

        private void Dispatch()
        {
            if (TryGetResolvers(out IInitialize[] initializes))
            {
                InitializeLoopItem loopItem = new(initializes);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.INITIALIZATION, loopItem);
            }

            if (TryGetResolvers(out IPostInitialize[] postInitializes))
            {
                PostInitializeLoopItem loopItem = new(postInitializes);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.POST_INITIALIZATION, loopItem);
            }

            if (TryGetResolvers(out IStart[] starts))
            {
                StartLoopItem loopItem = new(starts);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.START, loopItem);
            }

            if (TryGetResolvers(out IPostStart[] postStarts))
            {
                PostStartLoopItem loopItem = new(postStarts);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.POST_START, loopItem);
            }

            if (TryGetResolvers(out IFixedTick[] fixedTicks))
            {
                FixedTickLoopItem loopItem = new(fixedTicks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.FIXED_TICK, loopItem);
            }

            if (TryGetResolvers(out IPostFixedTick[] postFixedTicks))
            {
                PostFixedTickLoopItem loopItem = new(postFixedTicks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.POST_FIXED_TICK, loopItem);
            }

            if (TryGetResolvers(out ITick[] ticks))
            {
                TickLoopItem loopItem = new(ticks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.TICK, loopItem);
            }

            if (TryGetResolvers(out IPostTick[] postTicks))
            {
                PostTickLoopItem loopItem = new(postTicks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.POST_TICK, loopItem);
            }

            if (TryGetResolvers(out ILateTick[] lateTicks))
            {
                LateTickLoopItem loopItem = new(lateTicks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.LATE_TICK, loopItem);
            }

            if (TryGetResolvers(out IPostLateTick[] postLateTicks))
            {
                PostLateTickLoopItem loopItem = new(postLateTicks);
                _disposable.Add(loopItem);
                Registrar.Dispatch(PlayerLoopTiming.POST_LATE_TICK, loopItem);
            }

            bool TryGetResolvers<T>(out T[] resolvers)
            {
                resolvers = default;
                bool hasBinding = _container.HasBinding<T>();

                if (!hasBinding)
                {
                    return false;
                }

                resolvers = _container.Get<T>().ToArray();

                return resolvers != null;
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}