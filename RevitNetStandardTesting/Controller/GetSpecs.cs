using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace RevitNetStandardTesting.Controller
{
    internal class GetSpecs
    {
        public static IEnumerable<ForgeTypeId> GetSpecsByDiscipline(ForgeTypeId discipline)
        {
            return (from spec in UnitUtils.GetAllMeasurableSpecs()
                    let specDiscipline = UnitUtils.GetDiscipline(spec)
                    where specDiscipline.Equals(discipline)
                    select spec).ToList();
        }

        public static IEnumerable<ForgeTypeId> GetDisciplines() => UnitUtils.GetAllDisciplines();
    }
}
