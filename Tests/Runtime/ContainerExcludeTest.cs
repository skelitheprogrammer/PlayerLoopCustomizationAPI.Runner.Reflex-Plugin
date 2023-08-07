using System.Collections;
using NUnit.Framework;
using PlayerLoopCustomizationAPI.Addons.Runner.Implementation;
using PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions;
using Reflex.Core;
using UnityEngine.TestTools;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Tests.Runtime
{
    [TestFixture]
    internal sealed class ContainerExcludeTest
    {
        [UnityTest]
        public IEnumerator Parent_Child_Container_Child_Resolve_Test()
        {
            ContainerDescriptor parentDescriptor = new("ParentTestContainer");
            parentDescriptor.AddLoopRunner();
            parentDescriptor.AddSingleton(typeof(SomeTestClass), typeof(SomeTestClass), typeof(IStart));
            Container parentContainer = parentDescriptor.Build();

            ContainerDescriptor childDescriptor = new("ChildTestContainer", parentContainer);
            childDescriptor.AddLoopRunner();
            Container childContainer = childDescriptor.Build();

            yield return null;

            Assert.AreEqual(childContainer.Resolve<SomeTestClass>().ContainerName, parentContainer.Name);
        }

        [UnityTest]
        public IEnumerator Separate_Container_Exclude_Test()
        {
            ContainerDescriptor firstDescriptor = new("FirstContainerTest");
            firstDescriptor.AddLoopRunner();
            firstDescriptor.AddSingleton(typeof(SomeTestClass), typeof(SomeTestClass), typeof(IStart));
            Container firstContainer = firstDescriptor.Build();

            ContainerDescriptor childDescriptor = new("ChildContainer", firstContainer);
            childDescriptor.AddLoopRunner();
            Container childContainer = childDescriptor.Build();

            ContainerDescriptor secondDescriptor = new("SecondContainerTest");
            secondDescriptor.AddLoopRunner();
            Container secondContainer = secondDescriptor.Build();

            yield return null;

            Assert.Catch(() => { secondContainer.Resolve<SomeTestClass>(); });
            Assert.AreEqual(firstContainer.Name, childContainer.Resolve<SomeTestClass>().ContainerName);
        }

        private class SomeTestClass : IStart
        {
            private readonly Container _container;
            public string ContainerName => _container.Name;

            public SomeTestClass(Container container)
            {
                _container = container;
            }

            public void Start()
            {
            }
        }
    }
}