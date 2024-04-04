using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class DefaultOptionsData : IDefaultOptionsData
    {
        public DefaultOptionsData(ForgeTypeId unit)
        {
            CanHaveSymbol = FormatOptions.CanHaveSymbol(unit);
            CanSuppressSpaces = FormatOptions.CanSuppressSpaces(unit);
            CanSuppressLeadingZeros = FormatOptions.CanSuppressLeadingZeros(unit);
            CanSuppressTrailingZeros = FormatOptions.CanSuppressTrailingZeros(unit);
            CanUsePlusPrefix = FormatOptions.CanUsePlusPrefix(unit);
        }

        public bool CanHaveSymbol { get; }
        public bool CanSuppressSpaces { get; }
        public bool CanSuppressLeadingZeros { get; }
        public bool CanSuppressTrailingZeros { get; }
        public bool CanUsePlusPrefix { get; }
    }
}
