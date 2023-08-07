using System.Collections;
using NUnit.Framework;
using PlayerLoopCustomizationAPI.Addons.Runner.Implementation;
using PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions;
using Reflex.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Tests.Runtime
{
    internal sealed class InterfaceResolveTest
    {
        [UnityTest]
        public IEnumerator Interface_Test()
        {
            ContainerDescriptor descriptor = new("TestContainer");
            descriptor.AddLoopRunner();
            descriptor.AddSingleton(typeof(InterfaceDummy), typeof(InterfaceDummy), typeof(ITick));
            Container container = descriptor.Build();
            
            yield return null;
            yield return null;
            yield return null;
            yield return null;

            int tickCount = container.Resolve<InterfaceDummy>().TickCount;
            Debug.Log(tickCount);
            Assert.That(tickCount == 4);
        }

        class InterfaceDummy : ITick
        {
            public int TickCount;
            
            public void Tick()
            {
                TickCount++;
            }
        }
    }
}