using System;
using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Borders : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Color,
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable
        };

        public override bool CanParse(string className)
        {
            return CanParseB(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            SupportedValueType? detectedType;
            //radius
            if (className == "rounded-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-t-xs")
            {
                return new List<(string property, UssValue value)> {
                    ("border-top-left-radius", new UssVariableValue("--radius-xs")),
                    ("border-top-right-radius", new UssVariableValue("--radius-xs"))
                  };
            }
            else if (className == "rounded-t-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-sm")),
                ("border-top-right-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-t-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-md")),
                ("border-top-right-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-t-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-lg")),
                ("border-top-right-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-t-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-xl")),
                ("border-top-right-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-t-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-2xl")),
                 ("border-top-right-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-t-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-3xl")),
                ("border-top-right-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-t-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-4xl")),
                ("border-top-right-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-t-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new StaticValue("0")),
                ("border-top-right-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-t-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-full")),
                ("border-top-right-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-r-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-xs")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-r-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-sm")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-r-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-md")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-r-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-lg")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-r-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-xl")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-r-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-2xl")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-r-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-3xl")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-r-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-4xl")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-r-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new StaticValue("0")),
                ("border-bottom-right-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-r-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-full")),
                ("border-bottom-right-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-b-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-xs")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-b-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-sm")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-b-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-md")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-b-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-lg")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-b-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-b-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-2xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-b-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-b-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-4xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-b-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new StaticValue("0")),
                ("border-bottom-left-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-b-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-full")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-l-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-xs")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-l-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-sm")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-l-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-md")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-l-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-lg")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-l-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-l-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-2xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-l-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-3xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-l-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-4xl")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-l-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new StaticValue("0")),
                ("border-bottom-left-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-l-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-full")),
                ("border-bottom-left-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-tl-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-tl-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-tl-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-tl-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-tl-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-tl-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-tl-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-tl-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-tl-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-tl-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-left-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-tr-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-tr-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-tr-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-tr-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-tr-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-tr-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-tr-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-tr-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-tr-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-tr-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-right-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-br-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-br-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-br-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-br-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-br-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-br-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-br-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-br-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-br-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-br-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-right-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className == "rounded-bl-xs")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-xs"))
              };
            }
            else if (className == "rounded-bl-sm")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-sm"))
              };
            }
            else if (className == "rounded-bl-md")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-md"))
              };
            }
            else if (className == "rounded-bl-lg")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-lg"))
              };
            }
            else if (className == "rounded-bl-xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-xl"))
              };
            }
            else if (className == "rounded-bl-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-2xl"))
              };
            }
            else if (className == "rounded-bl-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-3xl"))
              };
            }
            else if (className == "rounded-bl-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-4xl"))
              };
            }
            else if (className == "rounded-bl-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new StaticValue("0"))
              };
            }
            else if (className == "rounded-bl-full")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-left-radius", new UssVariableValue("--radius-full"))
              };
            }
            else if (className.StartsWith("rounded-t-"))
            {
                string suffix = className["rounded-t-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", cssValue),
                        ("border-top-right-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", ProcessFile.CustomTheme["radius"][suffix]),
                        ("border-top-right-radius", ProcessFile.CustomTheme["radius"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", new StaticValue($"{suffix}px")),
                        ("border-top-right-radius", new StaticValue($"{suffix}px")),
                    };
                }
            }
            else if (className.StartsWith("rounded-r-"))
            {
                string suffix = className["rounded-r-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-right-radius", cssValue),
                        ("border-bottom-right-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-bottom-right-radius", ProcessFile.CustomTheme["radius"][suffix]),
                        ("border-top-right-radius", ProcessFile.CustomTheme["radius"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-bottom-right-radius", new StaticValue($"{suffix}px")),
                    ("border-top-right-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-b-"))
            {
                string suffix = className["rounded-b-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-bottom-left-radius", cssValue),
                        ("border-bottom-right-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-bottom-left-radius", ProcessFile.CustomTheme["radius"][suffix]),
                        ("border-bottom-right-radius", ProcessFile.CustomTheme["radius"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-bottom-left-radius", new StaticValue($"{suffix}px")),
                    ("border-bottom-right-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-l-"))
            {
                string suffix = className["rounded-l-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", cssValue),
                        ("border-bottom-left-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", ProcessFile.CustomTheme["radius"][suffix]),
                        ("border-bottom-left-radius", ProcessFile.CustomTheme["radius"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-top-left-radius", new StaticValue($"{suffix}px")),
                    ("border-bottom-left-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-tl-"))
            {
                string suffix = className["rounded-tl-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-top-left-radius", ProcessFile.CustomTheme["radius"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-top-left-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-tr-"))
            {
                string suffix = className["rounded-tr-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-top-right-radius", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-top-right-radius", ProcessFile.CustomTheme["radius"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-top-right-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-bl-"))
            {
                string suffix = className["rounded-bl-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-bottom-left-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-bottom-left-radius", ProcessFile.CustomTheme["radius"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                        ("border-bottom-left-radius", new StaticValue($"{suffix}px")),
                    };
                }
            }
            else if (className.StartsWith("rounded-br-"))
            {
                string suffix = className["rounded-br-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-bottom-right-radius", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("border-bottom-right-radius", ProcessFile.CustomTheme["radius"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-bottom-right-radius", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("rounded-"))
            {
                string suffix = className["rounded-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-radius", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("radius") && ProcessFile.CustomTheme["radius"].ContainsKey(suffix))
                {

                    return new List<(string property, UssValue value)> {
                        ("border-radius", ProcessFile.CustomTheme["radius"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                        ("border-radius", new StaticValue($"{suffix}px")),
                    };
                }
            }


            //border
            if (className == "border")
            {
                return new List<(string property, UssValue value)> {
                ("border-width", new StaticValue("1px"))
            };
            }
            else if (className == "border-x")
            {
                return new List<(string property, UssValue value)> {
                ("border-right-width", new StaticValue("1px")),
                ("border-left-width", new StaticValue("1px"))
            };
            }
            else if (className == "border-y")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-width", new StaticValue("1px")),
                ("border-bottom-width", new StaticValue("1px"))
              };
            }
            else if (className == "border-t")
            {
                return new List<(string property, UssValue value)> {
                ("border-top-width", new StaticValue("1px"))
              };
            }
            else if (className == "border-r")
            {
                return new List<(string property, UssValue value)> {
                ("border-right-width", new StaticValue("1px"))
              };
            }
            else if (className == "border-b")
            {
                return new List<(string property, UssValue value)> {
                ("border-bottom-width", new StaticValue("1px"))
              };
            }
            else if (className == "border-l")
            {
                return new List<(string property, UssValue value)> {
                ("border-left-width", new StaticValue("1px"))
              };
            }
            else if (className.StartsWith("border-x-"))
            {

                string suffix = className["border-x-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-left-width", new UssVariableValue(newVal)),
                            ("border-right-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-left-color", cssValue),
                    ("border-right-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-left-color", ProcessFile.CustomTheme["color"][suffix]),
                        ("border-right-color", ProcessFile.CustomTheme["color"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                        ("border-left-width", new StaticValue($"{suffix}px")),
                        ("border-right-width", new StaticValue($"{suffix}px"))
                    };
                }
            }
            else if (className.StartsWith("border-y-"))
            {

                string suffix = className["border-y-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-top-width", new UssVariableValue(newVal)),
                            ("border-bottom-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-color", cssValue),
                        ("border-bottom-color", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-color", ProcessFile.CustomTheme["color"][suffix]),
                        ("border-bottom-color", ProcessFile.CustomTheme["color"][suffix])
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-top-width", new StaticValue($"{suffix}px")),
                    ("border-bottom-width", new StaticValue($"{suffix}px"))
                };
                }
            }
            else if (className.StartsWith("border-t-"))
            {

                string suffix = className["border-t-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-top-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-top-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-top-color", ProcessFile.CustomTheme["color"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-top-width", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("border-r-"))
            {

                string suffix = className["border-r-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-right-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-right-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-right-color", ProcessFile.CustomTheme["color"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-right-width", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("border-b-"))
            {

                string suffix = className["border-b-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-bottom-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-bottom-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-bottom-color", ProcessFile.CustomTheme["color"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-bottom-width", new StaticValue($"{suffix}px")),
                };
                }
            }
            else if (className.StartsWith("border-l-"))
            {

                string suffix = className["border-l-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-left-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-left-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-left-color", ProcessFile.CustomTheme["color"][suffix]),
                    };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("border-left-width", new StaticValue($"{suffix}px")),
                };
                }
            }



            //style
            if (className == "border-solid")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("solid"))
              };
            }
            else if (className == "border-dashed")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("dashed"))
              };
            }
            else if (className == "border-dotted")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("dotted"))
              };
            }
            else if (className == "border-double")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("double"))
              };
            }
            else if (className == "border-hidden")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("hidden"))
              };
            }
            else if (className == "border-none")
            {
                return new List<(string property, UssValue value)> {
                ("border-style", new StaticValue("none"))
              };
            }
            else if (className.StartsWith("border-"))
            {
                string suffix = className["border-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "length")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> {
                            ("border-width", new UssVariableValue(newVal)),
                        };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                    ("border-color", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("border-color", ProcessFile.CustomTheme["color"][suffix]),
                    };
                }
                else
                {
                    if (float.TryParse(suffix.Trim(), out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("border-width", new StaticValue($"{val}px")),
                        };
                    }
                }
            }

            return null;
        }

        private bool CanParseB(string className)
        {
            return className == "rounded-xs"
            || className == "rounded-sm"
            || className == "rounded-md"
            || className == "rounded-lg"
            || className == "rounded-xl"
            || className == "rounded-2xl"
            || className == "rounded-3xl"
            || className == "rounded-4xl"
            || className == "rounded-none"
            || className == "rounded-full"
            || className == "rounded-t-xs"
            || className == "rounded-t-sm"
            || className == "rounded-t-md"
            || className == "rounded-t-lg"
            || className == "rounded-t-xl"
            || className == "rounded-t-2xl"
            || className == "rounded-t-3xl"
            || className == "rounded-t-4xl"
            || className == "rounded-t-none"
            || className == "rounded-t-full"
            || className == "rounded-r-xs"
            || className == "rounded-r-sm"
            || className == "rounded-r-md"
            || className == "rounded-r-lg"
            || className == "rounded-r-xl"
            || className == "rounded-r-2xl"
            || className == "rounded-r-3xl"
            || className == "rounded-r-4xl"
            || className == "rounded-r-none"
            || className == "rounded-r-full"
            || className == "rounded-b-xs"
            || className == "rounded-b-sm"
            || className == "rounded-b-md"
            || className == "rounded-b-lg"
            || className == "rounded-b-xl"
            || className == "rounded-b-2xl"
            || className == "rounded-b-3xl"
            || className == "rounded-b-4xl"
            || className == "rounded-b-none"
            || className == "rounded-b-full"
            || className == "rounded-l-xs"
            || className == "rounded-l-sm"
            || className == "rounded-l-md"
            || className == "rounded-l-lg"
            || className == "rounded-l-xl"
            || className == "rounded-l-2xl"
            || className == "rounded-l-3xl"
            || className == "rounded-l-4xl"
            || className == "rounded-l-none"
            || className == "rounded-l-full"
            || className == "rounded-tl-xs"
            || className == "rounded-tl-sm"
            || className == "rounded-tl-md"
            || className == "rounded-tl-lg"
            || className == "rounded-tl-xl"
            || className == "rounded-tl-2xl"
            || className == "rounded-tl-3xl"
            || className == "rounded-tl-4xl"
            || className == "rounded-tl-none"
            || className == "rounded-tl-full"
            || className == "rounded-tr-xs"
            || className == "rounded-tr-sm"
            || className == "rounded-tr-md"
            || className == "rounded-tr-lg"
            || className == "rounded-tr-xl"
            || className == "rounded-tr-2xl"
            || className == "rounded-tr-3xl"
            || className == "rounded-tr-4xl"
            || className == "rounded-tr-none"
            || className == "rounded-tr-full"
            || className == "rounded-br-xs"
            || className == "rounded-br-sm"
            || className == "rounded-br-md"
            || className == "rounded-br-lg"
            || className == "rounded-br-xl"
            || className == "rounded-br-2xl"
            || className == "rounded-br-3xl"
            || className == "rounded-br-4xl"
            || className == "rounded-br-none"
            || className == "rounded-br-full"
            || className == "rounded-bl-xs"
            || className == "rounded-bl-sm"
            || className == "rounded-bl-md"
            || className == "rounded-bl-lg"
            || className == "rounded-bl-xl"
            || className == "rounded-bl-2xl"
            || className == "rounded-bl-3xl"
            || className == "rounded-bl-4xl"
            || className == "rounded-bl-none"
            || className == "rounded-bl-full"


            || className == "border"
            || className == "border-x"
            || className == "border-y"
            || className == "border-t"
            || className == "border-r"
            || className == "border-b"
            || className == "border-l"

            || className == "border-solid"
            || className == "border-dashed"
            || className == "border-dotted"
            || className == "border-double"
            || className == "border-hidden"
            || className == "border-none"


            || className.StartsWith("border-x-")
            || className.StartsWith("border-y-")
            || className.StartsWith("border-t-")
            || className.StartsWith("border-r-")
            || className.StartsWith("border-b-")
            || className.StartsWith("border-l-")
            || className.StartsWith("border-")

            || className.StartsWith("rounded-t-")
            || className.StartsWith("rounded-r-")
            || className.StartsWith("rounded-b-")
            || className.StartsWith("rounded-l-")
            || className.StartsWith("rounded-tl-")
            || className.StartsWith("rounded-tr-")
            || className.StartsWith("rounded-bl-")
            || className.StartsWith("rounded-br-")
            || className.StartsWith("rounded-");
        }
    }
}