using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class SpecData : ISpecData
    {
        public SpecData(Document doc, ForgeTypeId spec)
        {
            
            ForgeTypeId unit = doc.GetUnits().GetFormatOptions(spec).GetUnitTypeId();
            Spec = spec;
            Discipline = LabelUtils.GetLabelForDiscipline(UnitUtils.GetDiscipline(spec));
            SpecTypeId = LabelUtils.GetLabelForSpec(spec);
            Unit = new UnitData(unit);
            DefaultOptions = new DefaultOptionsData(unit);
            SetOptions = new SetOptionsData(doc, spec, unit);
        }

        public string Discipline { get; }
        public string SpecTypeId { get; }
        public ForgeTypeId Spec { get; }
        public IUnitData Unit { get; }
        public IDefaultOptionsData DefaultOptions { get; }
        public ISetOptionsData SetOptions { get; }
    }
}
