using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace RevitNetStandardTesting.Controller
{
    internal class GetUIElements
    {
        public static void AddNewTab(UIControlledApplication a, string tabName) => a.CreateRibbonTab(tabName);

        public static RibbonPanel AddNewPanel(UIControlledApplication a, string tabName, string panelName) => a.CreateRibbonPanel(tabName, panelName);

        public static PushButtonData CreateNewButton(string buttonName, string buttonUIName, Type command) => new PushButtonData(buttonName, buttonUIName, System.Reflection.Assembly.GetExecutingAssembly().Location, command.FullName);

        public static void AddButton(RibbonPanel panel, PushButtonData buttonData) => panel.AddItem(buttonData);

        public static void AddButtons(RibbonPanel panel, IEnumerable<PushButtonData> buttons)
        {
            foreach (PushButtonData button in buttons)
            {
                panel.AddItem(button);
            }
        }

        public static void CreateSyncedSplitButton(string name, string text, IEnumerable<PushButtonData> buttons, RibbonPanel panel)
        {
            SplitButtonData data = new SplitButtonData(name, text);
            SplitButton sb = panel.AddItem(data) as SplitButton;
            foreach (PushButtonData button in buttons)
            {
                sb.AddPushButton(button);
            }
            sb.IsSynchronizedWithCurrentItem = true;
        }

        public static void CreateButtonGroup(RibbonPanel panel, IEnumerable<PushButtonData> buttons, string name)
        {
            RadioButtonGroupData d = new RadioButtonGroupData(name);
            RadioButtonGroup rbg = panel.AddItem(d) as RadioButtonGroup;
            foreach (PushButtonData button in buttons)
            {
                ToggleButtonData data = button as ToggleButtonData;
                rbg.AddItem(data);
            }
        }
    }
}
