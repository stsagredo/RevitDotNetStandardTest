using System.Collections.Generic;

namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    internal interface ISetOptionsData
    {
        string RoundingMethod { get; }
        bool UseDigitGrouping { get; }
        bool UseDefaultFormatting { get; }
        bool AreFormatOptionsValidForSpec { get; }
        bool HasSymbol { get; }
        bool SuppressSpaces { get; }
        bool SuppressLeadingZeros { get; }
        bool SuppressTrailingZeros { get; }
        bool UsePlusPrefix { get; }
        IAccuracyData Accuracy { get; }
        IEnumerable<ISymbolData> ValidSymbols { get; }
        ISymbolData Symbol { get; }
    }
}
