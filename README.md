<div align="center">   

<h1>PlayerLoopAPI Runner Reflex plugin</h1>
Plugin for <a href="https://github.com/gustavopsantos/Reflex#blazing-fast-minimal-but-complete-dependency-injection-library-for-unity">Reflex</a> to automate custom loop system maanagement by extending the <a href="https://github.com/skelitheprogrammer/PlayerLoop-customization-API-Runner-Addon">PlayerLoopAPI.Runner</a> possibilities.
</div>

# Features
- Fully automated lifecycle span of PlayerLoopAPI.Runner custom LoopSystems.
- Easy to setup.

# Installation

### Add via package manager

```
https://github.com/skelitheprogrammer/Reflex-PlayerLoop-Runner-Plugin.git
```

### Add dependency in manifest.json
```
"com.skillitronic.playerloopcustomizationapi.addons.runner.reflexplugin": "https://github.com/skelitheprogrammer/Reflex-PlayerLoop-Runner-Plugin.git"
```

# Getting Started
# Choose interested interface

| IInitialize 	| IPostInitialize 	|
|-------------	|-----------------	|
| IStart      	| IPostStart      	|
| IFixedTick  	| IPostFixedTick  	|
| ITick       	| IPostTick       	|
| ILateTick   	| IPostLateTick   	|

## Create class and add interested interface
```c#
public class MyTicker : ITick
{
    public void Tick()
    {
        Debug.Log("Ticky!");
    }
}
```
## Insert into the installer
- Add `descriptor.AddLoopRunner()`
- Bind `MyTicker` and explicitly add interface contracts to make it work
```c#
public class MyTicker : ITick
{
    public void Tick()
    {
        Debug.Log("Ticky!");
    }
}

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    public override void AddBindings(ContainerDescriptor descriptor)
    {
        descriptor.AddLoopRunner();
        descriptor.AddSingleton(typeof(Myticker), typeof(MyTicker), typeof(ITick))
    }

}
```
