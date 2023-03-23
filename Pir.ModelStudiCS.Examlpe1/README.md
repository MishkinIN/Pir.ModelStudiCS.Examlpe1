# Pir.ModelStudiCS.Examlpe
## References
```
<Project>
<PropertyGroup>
<NC_SDK>..\..\NanoCAD\NC_SDK_22.0\</NC_SDK> <!--add Path to nanoCAD SDK-->
...
<!--add reference to COM Model Studio Objects 1.0 Type Library-->
...
```
## How use example command.
- Run nanoCAd application
- Load Pir.ModelStudiCS.Examlpe1.dll (command NETLOAD)
- Open dwg file width Model Studio CS primitives
- run command PIRELPROP, select Model Studio CS element and press Enter.

**Result**: If you select  Model Studio CS element, in command line print selected object class and
IElement.Name property of element.