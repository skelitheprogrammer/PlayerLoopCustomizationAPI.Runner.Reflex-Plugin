using Reflex.Core;

namespace PlayerLoopCustomizationAPI.Addons.Runner.ReflexPlugin.Extensions
{
    public static class ContainerDescriptorExtensions
    {
        public static ContainerDescriptor AddLoopRunner(this ContainerDescriptor descriptor)
        {
            descriptor.AddSingleton(typeof(ReflexLoopRunnerDispatcher), typeof(ReflexLoopRunnerDispatcher),typeof(IStartable));
            return descriptor;
        }
    }
}