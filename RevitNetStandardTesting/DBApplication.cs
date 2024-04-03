using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RevitNetStandardTesting
{
    /// <summary>
    /// Application without access to Revit's User Interface. They can be deployed to Autodesk's Design Automation services, or any other service that requires headless Revit access.
    /// </summary>
    public class DBApplication : IExternalDBApplication
    {
        /// <summary>
        /// Entry point for the Revit <see cref="ControlledApplication"/>. This is the first method executed by Revit upon startup.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            // Subscribe to the DocumentOpened event and execute this method when raised:
            application.DocumentOpened += Application_DocumentOpened;
            return ExternalDBApplicationResult.Succeeded;
        }

        /// <summary>
        /// Method run when a <see cref="Document"/> is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
            Debug.Print($"This DBApp says the document {e.Document.Title} has been opened!");
        }

        /// <summary>
        /// Method executed when the <see cref="Autodesk.Revit.ApplicationServices.ControlledApplication"/> is closing. This is the last method executed by Revit.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            Debug.Print("This DBApp is shutting down.");
            return ExternalDBApplicationResult.Succeeded;
        }


    }
}
