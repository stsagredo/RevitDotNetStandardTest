/*
 * Solution: RevitNetStandardTesting
 * File: Command.cs
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

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RevitNetStandardTesting.Model.UnitModel.Contracts;
using System;

namespace RevitNetStandardTesting
{
    /// <summary>
    /// Executable external commands that can be called through the <see cref="UIApplication"/>. Note that they're not accessible without access to the UI, as they derive from <see cref="Autodesk.Revit.UI"/>.
    /// </summary>
    [TransactionAttribute(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        /// <summary>
        /// Executes code inside the <see cref="IExternalCommand.Execute(ExternalCommandData, ref bool, ElementSet)"/> method.
        /// </summary>
        /// <param name="commandData">Dependencies passed by Revit to initialise the <see cref="Command"/>. Includes access to a <see cref="IDictionary{TKey, TValue}"/> for JournalData, the <see cref="UIApplication"/> and the current <see cref="View"/> shown on the UI.</param>
        /// <param name="message">Anything written here will be passed to the UI to display as a warning.</param>
        /// <param name="elements">Access to the currenly selected items by the user.</param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Get the unit data through the DBApp.
                // No need to do it this way, but we can test if the DB-level apps work under .NET Standard
                FetchAndShowSpecAsJson(commandData.Application.ActiveUIDocument.Document, SpecTypeId.Length);
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                // Show on screen what went wrong.
                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                {
                    MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                };
                t.Show();
                return Result.Failed;
            }

        }

        /// <summary>
        /// Tests a lot of different functions, like calling a DBApplication, external libraries, API calls.
        /// 
        /// Fetches the data from the spec, and displays it on a dialog box.
        /// </summary>
        /// <param name="spec"></param>
        private void FetchAndShowSpecAsJson(Document doc, ForgeTypeId spec)
        {
            // Start a new disposable transaction.
            using (Transaction tr = new Transaction(doc, "Get Length Format"))
            {
                tr.Start();
                try
                {
                    // We create a DBApp object to test if we can execute functions from here:
                    DBApplication app = new DBApplication();

                    // Create an object with the spec data 
                    ISpecData specData = app.GetSpecData(doc, spec);

                    // Serialise the data:
                    string serialisedSpec = JsonConvert.SerializeObject(specData, Formatting.Indented);

                    // Show a TaskDialog with the results.
                    Autodesk.Revit.UI.TaskDialog t = new TaskDialog($"Specs for {specData.SpecTypeId}")
                    {
                        MainContent = serialisedSpec
                    };
                    t.Show();

                    // Commit the transaction and finish.
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    // Show on screen what went wrong.
                    Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                    {
                        MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                    };
                    t.Show();

                    // Roll back the transaction.
                    tr.RollBack();
                }

                // Clear out the transaction from memory.
                tr.Dispose();
            }
        }
    }
}
