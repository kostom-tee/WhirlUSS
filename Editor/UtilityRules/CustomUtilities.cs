using System;
using System.Collections.Generic;

namespace Kostom.Style
{
    internal class CustomUtilities : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => throw new NotImplementedException();

        public override bool CanParse(string className)
        {
            return ProcessFile.StyleUtilities.ContainsKey(className) || ProcessFile.UtilityCombo.ContainsKey(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            if (ProcessFile.StyleUtilities.ContainsKey(className))
            {
                return ProcessFile.StyleUtilities[className].Count == 0 ? null : ProcessFile.StyleUtilities[className];
            }
            else if (ProcessFile.UtilityCombo.ContainsKey(className))
            {
                List<(string property, UssValue value)> values = new List<(string, UssValue)>();

                foreach (var item in ProcessFile.UtilityCombo[className].utilities)
                {
                    var val = ClassParser.ParseAndGetPropertyAndValue(item);
                    if (val == null) continue;
                    values.AddRange(val);
                }

                if (values.Count > 0)
                {
                    return values;
                }
            }
            return null;
        }
    }
}