using NUnit.Framework;
using PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions;
using Reflex.Core;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Tests.Runtime
{
    [TestFixture]
    internal sealed class LoopRunnerIntegrationTest
    {
        [Test]
        public void LoopRunner_Integration_Single()
        {
            ContainerDescriptor descriptor = new("TestContainer");
            descriptor.AddLoopRunner();
            Container container = descriptor.Build();
            ReflexLoopRunnerDispatcher loopRunner = container.Resolve<ReflexLoopRunnerDispatcher>();

            Assert.IsNotNull(loopRunner);
        }

        [Test]
        public void LoopRunner_Integration_Nested()
        {
            ContainerDescriptor parentDescriptor = new("ParentTestContainer");
            parentDescriptor.AddLoopRunner();
            Container parentContainer = parentDescriptor.Build();
            
            ContainerDescriptor childDescriptor = new("ChildTestContainer", parentContainer);
            childDescriptor.AddLoopRunner();
            Container childContainer = childDescriptor.Build();
           
            ReflexLoopRunnerDispatcher parentLoopRunner = parentContainer.Resolve<ReflexLoopRunnerDispatcher>();
            ReflexLoopRunnerDispatcher childLoopRunner = childContainer.Resolve<ReflexLoopRunnerDispatcher>();

            Assert.IsNotNull(parentLoopRunner);
            Assert.IsNotNull(childContainer);
            Assert.IsNotNull(parentLoopRunner);
            Assert.IsNotNull(childLoopRunner);
            Assert.AreNotSame(parentLoopRunner, childLoopRunner);
        }
    }
}