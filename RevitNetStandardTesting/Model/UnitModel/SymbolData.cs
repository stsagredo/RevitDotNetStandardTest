using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class SymbolData : ISymbolData
    {
        public SymbolData(ForgeTypeId unit, ForgeTypeId symbol)
        {
            SymbolTypeId = symbol.TypeId;
            SymbolName = TryGetSymbolLabel(unit);
            IsValidSymbol = FormatOptions.IsValidSymbol(unit, symbol);
        }

        private string TryGetSymbolLabel(ForgeTypeId symbol)
        {
            try
            {
                return LabelUtils.GetLabelForSymbol(symbol);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string SymbolTypeId { get; }
        public string SymbolName { get; }
        public bool IsValidSymbol { get; }

    }
}
