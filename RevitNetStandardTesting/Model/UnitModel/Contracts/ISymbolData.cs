namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    internal interface ISymbolData
    {
        string SymbolTypeId { get; }
        string SymbolName { get; }
        bool IsValidSymbol { get; }
    }
}
