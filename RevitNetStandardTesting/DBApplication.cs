using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Newtonsoft.Json;
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
                Debug.Print($"This DBApp says the document {e.Document.Title} has been opened! There are {GetElements.GetCountOFAllElementsInDocument(e.Document)} elements inside. \nPrinting the available Disciplines.");

                // Testing access to the RevitDBAPI
                foreach (var discipline in UnitUtils.GetAllDisciplines())
                {
                    Debug.Print(LabelUtils.GetLabelForDiscipline(discipline));
                }
            }
            catch (Exception ex)
            {
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
               
                return new SpecData(doc, spec);
                
            }
            catch (Exception ex)
            {
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
                Debug.Print("This DBApp is shutting down.");
            }
            catch (Exception ex)
            {
                Debug.Print($"Oh no, an error! Exception:\n{ex.Message}\n{ex.StackTrace}.");
            }
            return ExternalDBApplicationResult.Succeeded;
        }
    }
}
