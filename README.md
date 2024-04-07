![WriteupCover](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/6814d873-c95c-4234-a012-326a44448c4a)
# RevitNetStandardTesting
**Disclaimer:**

*Views and opinions expressed are of my own, and in no way reflect the official policy or position of my employer. Research conducted using publicly available and/or official sources, like the [Revit API Documentation](https://www.revitapidocs.com/), Autodesk forums, and/or sources cited whenever applicable.*

## Why?
Revit 2025 brings the biggest change for us developers down below: the transition to .NET 8, the latest LTS version of .NET. But what if we **have** to support 2025 *and* older versions of Revit? Do we split the codebase? I don’t want to be left developing two different solutions for what’s in the end the same target because the underlying common technologies are different. 

Now this is where .NET Standard comes in.

This proof-of-concept:
- Create a test add-in that implements all three add-in types of Revit: `IExternalApplication`, `IExternalDBApplication` and `IExternalCommand`.
- Compile them targeted as pure, native .NET Standard assemblies from the same codebase.
- Test them inside Revit.
- Note any differences, incompatibilities or errors found.

### Considerations:

- The Revit version used will be **Revit 2024.2**
    - Revit 2025 was not available for use on my work machine to test at time of writing. Will update when I can test it myself.
- The sample `Snowdon Towers Sample Architectural.rvt` Document will be used.
- The .NET Target will be **.NET Standard 2.0**
    - .NET Standard 2.1 cannot be used to target .NET Framework 4.8, [according to this article](https://learn.microsoft.com/en-us/dotnet/standard/whats-new/whats-new-in-dotnet-standard?tabs=csharp).
- The API assemblies will be obtained from [this NuGet package](https://www.nuget.org/packages/Revit_All_Main_Versions_API_x64/2024.0.0), as to make it universally available. You can replace them with your own found on your Revit program folder.

### The brief:

- Develop an application that implements three different classes: a **UI-based application, a DB-level application, and a Command.**
- This test application will be based on my Units command, [available on GitHub here](https://github.com/stsagredo/RevitUnitsDemystified). It will perform the following tasks:
    - Getting **dependencies** to perform operations, like the Revit Document, Application and certain Events.
    - Create a new **Ribbon tab, button and hook a Command to it.**
    - Perform a **Unit Check** on a Revit Document.
    - Show a **window** on the screen with a result status.
    - **Log** certain events to the **Debug console**.
- Every task will be separated onto different parts. Think about it like **MVC**, where the **Model** is handled by an `IExternalDBApplication`, the **View** is handled by an `IExternalApplication` and the **Controller** is handled by an `IExternalCommand`:
    - `IExternalApplication`: handles the UI elements: ribbon tab, button, visible windows, and user input. Handles the **View** and triggers the **Controller** to perform the task.
    - `IExternalDBApplication`: contains the data model, logic and methods to perform the Unit check. Handles the data queries from the **View** and passes the results to the **Controller**.
    - `IExternalCommand`: handles the execution of the Unit check and passes the result to the **View.**
    - *Note: might not be 100% “kosher” MVC, but it follows a logic of separating concerns by distributing the data, interface and logic layers to separate objects.*
- The result is a **serialised JSON export** of the Units set on the current model, exported to the Desktop as to simplify the code produced.

# Testing
This is an abridged version of the testing I did. The full process can be found on my [LinkedIn writeup!](https://www.linkedin.com/in/stsagredo/)
1. First good sign, the assemblies were found by Revit!
   
  ![Untitled 7](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/a5dd2502-4ccb-4c43-be00-91ba3d740503)
  
  ![Untitled 8](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/a66532f0-a02c-41d4-b024-8e885fa8121d)
  
  ![Untitled 9](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/f8d7292e-7336-4dc2-9785-e3b33a1f5b33)

2. Second good sign, our `DBApp` executed their first payload.
   
  ![Untitled 10](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/15a04234-fe93-4dbc-bb32-38fac70b8d51)

3. The UI loaded our customised methods to create the `RibbonTab`:
   
  ![Untitled 14](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/a0c8c2e7-38f4-46d9-9a72-80f8464ac0bb)

4. And we get a button!
   
  ![Untitled 15](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/d8b40b06-25fc-448d-8511-98b50c11bc6f)


5. And after some debugging *(I was entering the Accuracy data the wrong way around: putting the unit where the spec should be, lol)*, we get a result!
    
  ![Untitled 16](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/371766f5-cb63-430d-8815-17d61b4265f2)

Turns out yes, you *can* run .NET Standard 2.0 add-ins on Revit. Which should be a foregone conclusion given how it is *inside* Framework, yet it changes enough things that it can lead to weird behaviour that is not documented. Here we have a working proof of concept.

# Conclusions

The main limitations to upgrade away from Framework onto the new technologies .NET offer is, like anyone expected, **leaving Windows (specific stuff) behind.**

Some of the incompatibilities I observed and not limited to, are:

- Cannot use `System.Windows.Media.Imaging` to load bitmaps onto the UI. There might be options out there, [like these that were introduced in .NET Core 2.0](https://devblogs.microsoft.com/dotnet/net-core-image-processing/).
- Cannot use **anything** that involves WinForms or Framework-like WPF. This wouldn’t come as a surprise, but many Revit add-ins do in fact use these two libraries to draw UI elements. This means porting any UI elements to [UWP](https://learn.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide).
- Can’t really test if there’s any issues with classes that rely on Office COM Interoperability. It’s beyond my scope and, apart from finding it repackaged in NuGet, I can’t tell if it works the same.

Transitioning to Revit 2025’s .NET 8-based API will be one huge challenge, considering the sheer amount of Windows-dependent code that many add-ins use and took advantage of throughout the years. However, if you have native Revit API code that does not use any external, Windows-only library or API call, then it’s certain that it will work at least down to a DB-level. Many of the basic Revit API functions work flawlessly and I cannot tell them apart from native Framework code.

I do, however, believe that a good approach going forward, especially for environments where several Revit versions are used in parallel), is to decouple the business logic from any code stuck on Framework. This should come as no surprise too, since it’s basic OOP good practice. However, it’s meant for precisely this case: where parts of your complex application need to move away from a single unified codebase, and interdependence means extensive (and expensive) refactoring of working code. 

# Class diagram
I know not many programmers like these, but helps me understand what I'm doing and since I have one, why not share it?
![ClassDiagram](https://github.com/stsagredo/RevitDotNetStandardTest/assets/159783646/a24330f4-4627-4ed4-b533-97691b2386ca)


If I were to take my example code, and apply the same separation of concerns strategy, it’d be easier, if not trivial, to fix and retarget. This should be a lesson for all of us in the future that **temporary solutions are the most permanent ones.**
