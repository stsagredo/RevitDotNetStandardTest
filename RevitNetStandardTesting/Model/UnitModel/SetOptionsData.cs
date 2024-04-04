/*
 * Solution: RevitNetStandardTesting
 * File: SetOptionsData.cs
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
using System.Collections.Generic;
using System.Linq;

namespace RevitNetStandardTesting.Model.UnitModel
{
    internal class SetOptionsData : ISetOptionsData
    {
        /// <summary>
        /// Gets the overriden, user specific data of a <see cref="UnitTypeId"/> set for a given <see cref="SpecTypeId"/> inside a Revit <see cref="Document"/>.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="spec"></param>
        /// <param name="unit"></param>
        public SetOptionsData(Document doc, ForgeTypeId spec, ForgeTypeId unit)
        {
            RoundingMethod = doc.GetUnits().GetFormatOptions(spec).RoundingMethod.ToString();
            UseDigitGrouping = doc.GetUnits().GetFormatOptions(spec).UseDigitGrouping;
            UseDefaultFormatting = doc.GetUnits().GetFormatOptions(spec).UseDefault;
            AreFormatOptionsValidForSpec = doc.GetUnits().GetFormatOptions(spec).IsValidForSpec(spec);
            HasSymbol = doc.GetUnits().GetFormatOptions(spec).CanHaveSymbol();
            SuppressSpaces = doc.GetUnits().GetFormatOptions(spec).SuppressSpaces;
            SuppressLeadingZeros = doc.GetUnits().GetFormatOptions(spec).SuppressLeadingZeros;
            SuppressTrailingZeros = doc.GetUnits().GetFormatOptions(spec).SuppressTrailingZeros;
            UsePlusPrefix = doc.GetUnits().GetFormatOptions(spec).UsePlusPrefix;
            Accuracy = new AccuracyData(doc, unit, spec);
            ValidSymbols = GetValidSymbols(unit);
            Symbol = new SymbolData(unit, doc.GetUnits().GetFormatOptions(spec).GetSymbolTypeId());
        }

        /// <summary>
        /// Get all valid symbols for any given <see cref="UnitTypeId"/>.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private IEnumerable<ISymbolData> GetValidSymbols(ForgeTypeId unit)
        {
            return from item in FormatOptions.GetValidSymbols(unit)
                   select new SymbolData(unit, item);
        }

        public string RoundingMethod { get; }
        public bool UseDigitGrouping { get; }
        public bool UseDefaultFormatting { get; }
        public bool AreFormatOptionsValidForSpec { get; }
        public bool HasSymbol { get; }
        public bool SuppressSpaces { get; }
        public bool SuppressLeadingZeros { get; }
        public bool SuppressTrailingZeros { get; }
        public bool UsePlusPrefix { get; }
        public IAccuracyData Accuracy { get; }
        public IEnumerable<ISymbolData> ValidSymbols { get; }
        public ISymbolData Symbol { get; }
    }
}
