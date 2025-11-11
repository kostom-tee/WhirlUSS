using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Arbitrary : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[] { SupportedValueType.Arbitrary };

        public override bool CanParse(string className)
        {
            return className.StartsWith("[") && className.EndsWith("]") && className.Length > 1 && className[1] != '&';
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            string suffix = className[1..^1];
            UssValue cssValue = UssValueParser.Parse(suffix);
            SupportedValueType detectedType = SupportedValueType.Arbitrary;
            if (!SupportedTypes.Contains(detectedType))
            {
                return null;
            }
            return new List<(string property, UssValue value)> { ("", cssValue) };
        }
    }
}