/*
 * Solution: RevitNetStandardTesting
 * File: GetUIElements.cs
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

using Autodesk.Revit.UI;
using System;

namespace RevitNetStandardTesting.Controller
{
    /// <summary>
    /// Operations related to the Revit UI.
    /// </summary>
    internal static class GetUIElements
    {
        /// <summary>
        /// Adds a new tab on the Revit ribbon.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="tabName"></param>
        public static void AddNewTab(UIControlledApplication a, string tabName) => a.CreateRibbonTab(tabName);

        /// <summary>
        /// Adds a new panel inside a Revit tab.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="tabName"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel AddNewPanel(UIControlledApplication a, string tabName, string panelName) => a.CreateRibbonPanel(tabName, panelName);

        /// <summary>
        /// Creates a new button, alongside hooks to the given command's <see cref="IExternalCommand.Execute(ExternalCommandData, ref string, Autodesk.Revit.DB.ElementSet)"/>.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="buttonUIName"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static PushButtonData CreateNewButton(string buttonName, string buttonUIName, Type command) => new PushButtonData(buttonName, buttonUIName, System.Reflection.Assembly.GetExecutingAssembly().Location, command.FullName);

        /// <summary>
        /// Adds a button to the given ribbon pannel.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="buttonData"></param>
        public static void AddButton(RibbonPanel panel, PushButtonData buttonData) => panel.AddItem(buttonData);
    }
}
