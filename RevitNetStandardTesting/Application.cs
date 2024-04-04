using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitNetStandardTesting.Controller;
using System;

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

        public Document ActiveDocument { get; set; }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                // Small test to see if we can access the UIApp object:
                string revitVersion = $"{application.ControlledApplication.VersionName} {application.ControlledApplication.SubVersionNumber}";

                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0!")
                {
                    MainContent = $"This application just started on {revitVersion} and is working!"
                };
                t.Show();

                // Always have the active document accesible through the Application.
                application.ControlledApplication.DocumentOpened += ControlledApplication_DocumentOpened;

                // Add the Length Check Button
                AddLengthCheckButton(application);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                {
                    MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                };
                t.Show();
                return Result.Failed;
            }
        }

        private void AddLengthCheckButton(UIControlledApplication application)
        {
            // Load command onto the Ribbon using our classes:
            GetUIElements.AddNewTab(application, ".NET Standard Test");
            RibbonPanel commonUnits = GetUIElements.AddNewPanel(application, ".NET Standard Test", "Common Units");

            // This button references our Command, where the check will be invoked.
            PushButtonData getLengthData = GetUIElements.CreateNewButton("GetLength", "Get Set Length", typeof(Command));
            GetUIElements.AddButton(commonUnits, getLengthData);
        }

        private void ControlledApplication_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
            this.ActiveDocument = e.Document;
        }

        /// <summary>
        /// Method executed when the <see cref="Autodesk.Revit.ApplicationServices.Application"/> is closing. This is the last method executed by Revit.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            try
            {
                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0!")
                {
                    MainContent = $"This application is shutting down."
                };
                t.Show();
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                {
                    MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                };
                t.Show();
                return Result.Failed;
            }

        }
    }
}
