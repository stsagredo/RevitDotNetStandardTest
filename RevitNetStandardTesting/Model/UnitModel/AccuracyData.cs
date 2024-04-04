using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class AccuracyData : IAccuracyData
    {
        public AccuracyData(Document doc, ForgeTypeId unit, ForgeTypeId spec)
        {
            AccuracyValue = doc.GetUnits().GetFormatOptions(spec).Accuracy;
            IsValidAccuracy = FormatOptions.IsValidAccuracy(unit, AccuracyValue);
        }
        public double AccuracyValue { get; }
        public bool IsValidAccuracy { get; }
    }
}
