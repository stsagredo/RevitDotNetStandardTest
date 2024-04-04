namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    internal interface ISpecData
    {
        string SpecTypeId { get; }
        IUnitData Unit { get; }
        IDefaultOptionsData DefaultOptions { get; }
        ISetOptionsData SetOptions { get; }
    }
}
