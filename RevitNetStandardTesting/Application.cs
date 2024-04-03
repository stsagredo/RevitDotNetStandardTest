using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevitNetStandardTesting
{
    /// <summary>
    /// Entry point for the Revit Extension.
    /// </summary>
    public class Application : IExternalApplication
    {
        /// <summary>
        /// Entry point for the Revit Application. This is the first method executed by Revit upon startup.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            // Small test to see if we can access the UIApp object:
            string revitVersion = $"{application.ControlledApplication.VersionName} {application.ControlledApplication.VersionNumber}"; 
            Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0!")
            {
                MainContent = $"This application just started on {revitVersion} and is working!"
            };
            t.Show();
            return Result.Succeeded;
        }

        /// <summary>
        /// Method executed when the <see cref="Autodesk.Revit.ApplicationServices.Application"/> is closing. This is the last method executed by Revit.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0!")
            {
                MainContent = $"This application is shutting down."
            };
            t.Show();
            return Result.Succeeded;
        }


    }
}
