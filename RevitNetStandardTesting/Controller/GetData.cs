using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel;
using RevitNetStandardTesting.Model.UnitModel.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RevitNetStandardTesting.Controller
{
    internal class GetData
    {
        public static ISpecData GetSpecData(Document doc, ForgeTypeId spec) => new SpecData(doc, spec);
        public static IEnumerable<ForgeTypeId> GetSpecsByDiscipline(ForgeTypeId discipline)
        {
            return (from spec in UnitUtils.GetAllMeasurableSpecs()
                    let specDiscipline = UnitUtils.GetDiscipline(spec)
                    where specDiscipline.Equals(discipline)
                    select spec);
        }
    }
}
