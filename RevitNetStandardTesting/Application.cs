/*
 * Solution: RevitNetStandardTesting
 * File: Application.cs
 * Date: 2024-04-04
 * Version: 2024.2.1
 * Revit version tested: Revit 2024.2
 * 
 * Written by Sebastian Torres Sagredo. 
 * GH: https://github.com/stsagredo 
 * 
 * Under MIT Licence:
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

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
        /// Active UI Document
        /// </summary>
        public Document ActiveDocument { get; set; }

        /// <summary>
        /// Entry point for the Revit Application. This is the first method executed by Revit upon startup.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
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
                // Show on the UI what happened.
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

        /// <summary>
        /// Loads the document to the Application instance under <see cref="Application.ActiveDocument"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
