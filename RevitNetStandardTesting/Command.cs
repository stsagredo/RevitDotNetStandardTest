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
                FetchAndShowSpecAsJson(commandData.Application.ActiveUIDocument.Document, SpecTypeId.Length);
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                {
                    MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                };
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
            using (Transaction tr = new Transaction(doc, "Get Length Format"))
            {
                tr.Start();
                try
                {
                    // We create a DBApp object to test if we can execute functions from here:
                    DBApplication app = new DBApplication();

                    // Create an object with the spec data 
                    ISpecData specData = app.GetSpecData(spec);

                    // Serialise the data:
                    string serialisedSpec = JsonConvert.SerializeObject(specData, Formatting.Indented);

                    // Show a TaskDialog with the results.
                    Autodesk.Revit.UI.TaskDialog t = new TaskDialog($"Specs for {specData.SpecTypeId}")
                    {
                        MainContent = serialisedSpec
                    };
                    t.Show();
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0! But it failed :(")
                    {
                        MainContent = $"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}."
                    };
                    t.Show();
                    tr.RollBack();
                }
                tr.Dispose();
            }
        }
    }
}
