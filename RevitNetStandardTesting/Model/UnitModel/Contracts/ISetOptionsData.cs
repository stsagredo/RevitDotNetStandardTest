﻿/*
 * Solution: RevitNetStandardTesting
 * File: ISetOptionsData.cs
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

using System.Collections.Generic;

namespace RevitNetStandardTesting.Model.UnitModel.Contracts
{
    /// <summary>
    /// Contract for any options set by the user on each specific Document. These often override the <see cref="IDefaultOptionsData"/> definitions.
    /// </summary>
    internal interface ISetOptionsData
    {
        string RoundingMethod { get; }
        bool UseDigitGrouping { get; }
        bool UseDefaultFormatting { get; }
        bool AreFormatOptionsValidForSpec { get; }
        bool HasSymbol { get; }
        bool SuppressSpaces { get; }
        bool SuppressLeadingZeros { get; }
        bool SuppressTrailingZeros { get; }
        bool UsePlusPrefix { get; }
        IAccuracyData Accuracy { get; }
        IEnumerable<ISymbolData> ValidSymbols { get; }
        ISymbolData Symbol { get; }
    }
}
