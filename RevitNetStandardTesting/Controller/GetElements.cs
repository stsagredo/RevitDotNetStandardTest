using Autodesk.Revit.DB;
using System;
using System.Linq;

namespace RevitNetStandardTesting.Controller
{
    internal static class GetElements
    {
        internal static double GetCountOFAllElementsInDocument(Document doc) => new FilteredElementCollector(doc).WherePasses(new LogicalOrFilter(new ElementIsElementTypeFilter(), new ElementIsElementTypeFilter(inverted: true))).Count();
    }
}
