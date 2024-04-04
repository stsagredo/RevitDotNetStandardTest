/*
 * Solution: RevitNetStandardTesting
 * File: SpecData.cs
 * Date: 2024-04-04
 * Version: 2024.2.1
 * Revit version tested: Revit 2024.2
 * 
 * Written by Sebastian Torres Sagredo. 
 * GH: https://github.com/stsagredo 
 * 
 * Under MIT Licence:
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using Autodesk.Revit.DB;
using RevitNetStandardTesting.Model.UnitModel.Contracts;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class SpecData : ISpecData
    {
        /// <summary>
        /// Gets the information of a given <see cref="Autodesk.Revit.DB.SpecTypeId"/> inside a Revit <see cref="Document"/>.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="spec"></param>
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
