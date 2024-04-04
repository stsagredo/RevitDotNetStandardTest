namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    internal interface IAccuracyData
    {
        double AccuracyValue { get; }
        bool IsValidAccuracy { get; }
    }
}
