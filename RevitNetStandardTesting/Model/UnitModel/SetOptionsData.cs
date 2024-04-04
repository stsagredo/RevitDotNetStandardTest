using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class SetOptionsData : ISetOptionsData
    {
        public SetOptionsData(Document doc, ForgeTypeId spec, ForgeTypeId unit)
        {
            RoundingMethod = doc.GetUnits().GetFormatOptions(spec).RoundingMethod.ToString();
            UseDigitGrouping = doc.GetUnits().GetFormatOptions(spec).UseDigitGrouping;
            UseDefaultFormatting = doc.GetUnits().GetFormatOptions(spec).UseDefault;
            AreFormatOptionsValidForSpec = doc.GetUnits().GetFormatOptions(spec).IsValidForSpec(spec);
            HasSymbol = doc.GetUnits().GetFormatOptions(spec).CanHaveSymbol();
            SuppressSpaces = doc.GetUnits().GetFormatOptions(spec).SuppressSpaces;
            SuppressLeadingZeros = doc.GetUnits().GetFormatOptions(spec).SuppressLeadingZeros;
            SuppressTrailingZeros = doc.GetUnits().GetFormatOptions(spec).SuppressTrailingZeros;
            UsePlusPrefix = doc.GetUnits().GetFormatOptions(spec).UsePlusPrefix;
            Accuracy = new AccuracyData(doc, unit, spec);
            ValidSymbols = GetValidSymbols(unit);
            Symbol = new SymbolData(unit, doc.GetUnits().GetFormatOptions(spec).GetSymbolTypeId());
        }

        private IEnumerable<ISymbolData> GetValidSymbols(ForgeTypeId unit)
        {
            return from item in FormatOptions.GetValidSymbols(unit)
                   select new SymbolData(unit, item);
        }

        public string RoundingMethod { get; }
        public bool UseDigitGrouping { get; }
        public bool UseDefaultFormatting { get; }
        public bool AreFormatOptionsValidForSpec { get; }
        public bool HasSymbol { get; }
        public bool SuppressSpaces { get; }
        public bool SuppressLeadingZeros { get; }
        public bool SuppressTrailingZeros { get; }
        public bool UsePlusPrefix { get; }
        public IAccuracyData Accuracy { get; }
        public IEnumerable<ISymbolData> ValidSymbols { get; }
        public ISymbolData Symbol { get; }
    }
}
