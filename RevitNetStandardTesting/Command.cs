using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
        /// Executes code inside the <see cref="IExternalCommand.Execute(ExternalCommandData, ref string, ElementSet)"/> method.
        /// </summary>
        /// <param name="commandData">Dependencies passed by Revit to initialise the <see cref="Command"/>. Includes access to a <see cref="IDictionary{TKey, TValue}"/> for JournalData, the <see cref="UIApplication"/> and the current <see cref="View"/> shown on the UI.</param>
        /// <param name="message">Anything written here will be passed to the UI to display as a warning.</param>
        /// <param name="elements">Access to the currenly selected items by the user.</param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string docName = commandData.Application.ActiveUIDocument.Document.Title;
            Autodesk.Revit.UI.TaskDialog t = new TaskDialog("This is running in .NET Standard 2.0!")
            {
                MainContent = $"This command is running over a document named {docName} and it's working!"
            };
            t.Show();
            return Result.Succeeded;
        }
    }
}
