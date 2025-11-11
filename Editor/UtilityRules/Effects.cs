using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Effects : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable,
        };

        public override bool CanParse(string className)
        {
            return CanParseEffects(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {

            SupportedValueType? detectedType;

            if (className == "text-shadow-2xs")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new UssVariableValue("--text-shadow-2xs"))
            };
            }
            else if (className == "text-shadow-xs")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new UssVariableValue("--text-shadow-xs"))
            };
            }
            else if (className == "text-shadow-sm")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new UssVariableValue("--text-shadow-sm"))
            };
            }
            else if (className == "text-shadow-md")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new UssVariableValue("--text-shadow-md"))
            };
            }
            else if (className == "text-shadow-lg")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new UssVariableValue("--text-shadow-lg"))
            };
            }
            else if (className == "text-shadow-none")
            {
                return new List<(string property, UssValue value)> {
                ("text-shadow", new StaticValue("0px 0px 0px rgba(0 0 0 0)"))
            };
            }
            else if (className.StartsWith("text-shadow-"))
            {
                string suffix = className["text-shadow-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("text-shadow", cssValue),
                };
                }
            }

            if (className.StartsWith("opacity-"))
            {
                string suffix = className["opacity-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("opacity", cssValue),
                };
                }
                else
                {
                    if (!suffix.Contains('.'))
                    {
                        if (int.TryParse(suffix, out var result))
                        {
                            return new List<(string property, UssValue value)> {
                            ("opacity", new StaticValue($"{(float)result/100}")),
                        };
                        }
                    }
                    return new List<(string property, UssValue value)> {
                    ("opacity", new StaticValue($"{suffix}")),
                };
                }
            }

            return null;
        }

        private bool CanParseEffects(string className)
        {
            return className == "text-shadow-xs"
                || className == "text-shadow-2xs"
                || className == "text-shadow-sm"
                || className == "text-shadow-md"
                || className == "text-shadow-lg"
                || className == "text-shadow-none"
                || className.StartsWith("text-shadow-")
                || className.StartsWith("opacity-");
        }
    }
}