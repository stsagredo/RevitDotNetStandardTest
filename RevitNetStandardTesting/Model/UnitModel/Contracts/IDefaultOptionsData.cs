namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    internal interface IDefaultOptionsData
    {
        bool CanHaveSymbol { get; }
        bool CanSuppressSpaces { get; }
        bool CanSuppressLeadingZeros { get; }
        bool CanSuppressTrailingZeros { get; }
        bool CanUsePlusPrefix { get; }
    }
}
