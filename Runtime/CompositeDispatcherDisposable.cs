using System;
using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin
{
    internal sealed class CompositeDispatcherDisposable : IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();

        public void Add(IDisposable disposable)
        {
            lock (_disposables)
            {
                _disposables.Push(disposable);
            }
        }

        public void Dispose()
        {
            IDisposable disposable;

            do
            {
                lock (_disposables)
                {
                    disposable = _disposables.Count > 0 ? _disposables.Pop() : null;
                }

                disposable?.Dispose();
            } while (disposable != null);
        }
    }
}
