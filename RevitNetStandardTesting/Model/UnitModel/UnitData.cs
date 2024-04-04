using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class UnitData : IUnitData
    {
        public UnitData(ForgeTypeId unit)
        {
            UnitTypeId = unit.TypeId;
            UnitName = LabelUtils.GetLabelForUnit(unit);
        }
        public string UnitTypeId { get; }
        public string UnitName { get; }
    }
}
