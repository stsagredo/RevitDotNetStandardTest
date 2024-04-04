/*
 * Solution: RevitNetStandardTesting
 * File: DBApplication.cs
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

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using RevitNetStandardTesting.Controller;
using RevitNetStandardTesting.Model.UnitModel;
using RevitNetStandardTesting.Model.UnitModel.Contracts;
using System;
using System.Diagnostics;

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
            try
            {
                // Test if we can access the DB-level objects, like the Document's properties, and perform a FilteredElementCollector query.
                // This tests one of the most common operations done in Revit: getting objects from the model.
                Debug.Print($"This DBApp says the document {e.Document.Title} has been opened! There are {GetElements.GetCountOFAllElementsInDocument(e.Document)} elements inside. \nPrinting the available Disciplines.");

                // Tests if we can access other elements of the API, like some static classes.
                foreach (var discipline in UnitUtils.GetAllDisciplines())
                {
                    Debug.Print(LabelUtils.GetLabelForDiscipline(discipline));
                }
            }
            catch (Exception ex)
            {
                // Show on debug window what went wrong.
                Debug.Print($"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}.");
            }
        }

        /// <summary>
        /// Gets the SpecData from the Revit Database. Callable from the outside.
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        internal ISpecData GetSpecData(Document doc, ForgeTypeId spec)
        {
            try
            {
                // Get the spec data and return.
                return new SpecData(doc, spec);

            }
            catch (Exception ex)
            {
                // Show on the debug window what went wrong.
                Debug.Print($"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}.");
                return null;
            }
        }

        /// <summary>
        /// Method executed when the <see cref="Autodesk.Revit.ApplicationServices.ControlledApplication"/> is closing. This is the last method executed by Revit.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            try
            {
                // Test if we can do things when Revit shuts down.
                Debug.Print("This DBApp is shutting down.");
            }
            catch (Exception ex)
            {
                // Show on the debug window what went wrong.
                Debug.Print($"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}.");
            }
            return ExternalDBApplicationResult.Succeeded;
        }
    }
}
