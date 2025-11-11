using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kostom.Style
{
    internal class Sizing : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
        SupportedValueType.Arbitrary,
        SupportedValueType.CssVariable
    };

        public override bool CanParse(string className)
        {
            return CanParseSizing(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {

            SupportedValueType? detectedType;

            //width
            if (className == "w-3xs")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("256px"))
              };
            }
            else if (className == "w-2xs")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("288px"))
              };
            }
            else if (className == "w-xs")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("320px"))
              };
            }
            else if (className == "w-sm")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("384px"))
              };
            }
            else if (className == "w-md")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("448px"))
              };
            }
            else if (className == "w-lg")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("512px"))
              };
            }
            else if (className == "w-xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("576px"))
              };
            }
            else if (className == "w-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("672px"))
              };
            }
            else if (className == "w-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("768px"))
              };
            }
            else if (className == "w-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("896px"))
              };
            }
            else if (className == "w-5xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("1024px"))
              };
            }
            else if (className == "w-6xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("1152px"))
              };
            }
            else if (className == "w-7xl")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("1280px"))
              };
            }
            else if (className == "w-auto")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue(" auto"))
              };
            }
            else if (className == "w-px")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("1px"))
              };
            }
            else if (className == "w-full")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100%"))
              };
            }
            else if (className == "w-screen")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100vw"))
              };
            }
            else if (className == "w-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100dvw"))
              };
            }
            else if (className == "w-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100dvh"))
              };
            }
            else if (className == "w-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100lvw"))
              };
            }
            else if (className == "w-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100lvh"))
              };
            }
            else if (className == "w-svw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100svw"))
              };
            }
            else if (className == "w-svh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100svh"))
              };
            }
            else if (className == "w-min")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("min-content"))
              };
            }
            else if (className == "w-max")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("max-content"))
              };
            }
            else if (className == "w-fit")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("fit-content"))
              };
            }
            else if (className == "size-auto")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("auto")),
                ("height", new StaticValue("auto"))
              };
            }
            else if (className == "size-px")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("1px")),
                ("height", new StaticValue("1px"))
              };
            }
            else if (className == "size-full")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue(" 100%")),
                ("height", new StaticValue(" 100%"))
              };
            }
            else if (className == "size-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue(" 100dvw")),
                ("height", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "size-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue(" 100dvh")),
                ("height", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "size-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue(" 100lvw")),
                ("height", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "size-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100lvh")),
                ("height", new StaticValue("100lvh"))
              };
            }
            else if (className == "size-svw")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100svw")),
                ("height", new StaticValue("100svw"))
              };
            }
            else if (className == "size-svh")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("100svh")),
                ("height", new StaticValue("100svh"))
              };
            }
            else if (className == "size-min")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("min-content")),
                ("height", new StaticValue("min-content"))
              };
            }
            else if (className == "size-max")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("max-content")),
                ("height", new StaticValue("max-content"))
              };
            }
            else if (className == "size-fit")
            {
                return new List<(string property, UssValue value)> {
                ("width", new StaticValue("fit-content")),
                ("height", new StaticValue("fit-content"))
              };
            }
            else if (className.StartsWith("w-"))
            {
                string suffix = className[2..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("width", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>w-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("width", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }
            else if (className.StartsWith("size-"))
            {
                string suffix = className[5..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("width", cssValue),
                    ("height", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>size-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("width", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                        ("height", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }


            //min width
            if (className == "min-w-3xs")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("256px"))
              };
            }
            else if (className == "min-w-2xs")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("288px"))
              };
            }
            else if (className == "min-w-xs")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("320px"))
              };
            }
            else if (className == "min-w-sm")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("384px"))
              };
            }
            else if (className == "min-w-md")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("448px"))
              };
            }
            else if (className == "min-w-lg")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("512px"))
              };
            }
            else if (className == "min-w-xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("576px"))
              };
            }
            else if (className == "min-w-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("672px"))
              };
            }
            else if (className == "min-w-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("768px"))
              };
            }
            else if (className == "min-w-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("896px"))
              };
            }
            else if (className == "min-w-5xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("1024px"))
              };
            }
            else if (className == "min-w-6xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("1152px"))
              };
            }
            else if (className == "min-w-7xl")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue("1280px"))
              };
            }
            else if (className == "min-w-auto")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" auto"))
              };
            }
            else if (className == "min-w-px")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 1px"))
              };
            }
            else if (className == "min-w-full")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100%"))
              };
            }
            else if (className == "min-w-screen")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100vw"))
              };
            }
            else if (className == "min-w-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "min-w-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "min-w-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "min-w-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100lvh"))
              };
            }
            else if (className == "min-w-svw")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100svw"))
              };
            }
            else if (className == "min-w-svh")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" 100svh"))
              };
            }
            else if (className == "min-w-min")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" min-content"))
              };
            }
            else if (className == "min-w-max")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" max-content"))
              };
            }
            else if (className == "min-w-fit")
            {
                return new List<(string property, UssValue value)> {
                ("min-width", new StaticValue(" fit-content"))
              };
            }
            else if (className.StartsWith("min-w-"))
            {
                string suffix = className[6..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("min-width", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>min-w-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("min-width", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }


            //max-width
            if (className == "max-w-3xs")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("256px"))
              };
            }
            else if (className == "max-w-2xs")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("288px"))
              };
            }
            else if (className == "max-w-xs")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("320px"))
              };
            }
            else if (className == "max-w-sm")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("384px"))
              };
            }
            else if (className == "max-w-md")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("448px"))
              };
            }
            else if (className == "max-w-lg")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("512px"))
              };
            }
            else if (className == "max-w-xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("76px"))
              };
            }
            else if (className == "max-w-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("672px"))
              };
            }
            else if (className == "max-w-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("768px"))
              };
            }
            else if (className == "max-w-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("896px"))
              };
            }
            else if (className == "max-w-5xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("1024px"))
              };
            }
            else if (className == "max-w-6xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("1152px"))
              };
            }
            else if (className == "max-w-7xl")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue("1280px"))
              };
            }
            else if (className == "max-w-none")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" none"))
              };
            }
            else if (className == "max-w-px")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 1px"))
              };
            }
            else if (className == "max-w-full")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100%"))
              };
            }
            else if (className == "max-w-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "max-w-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "max-w-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "max-w-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100lvh"))
              };
            }
            else if (className == "max-w-svw")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100svw"))
              };
            }
            else if (className == "max-w-svh")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100svh"))
              };
            }
            else if (className == "max-w-screen")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" 100vw"))
              };
            }
            else if (className == "max-w-min")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" min-content"))
              };
            }
            else if (className == "max-w-max")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" max-content"))
              };
            }
            else if (className == "max-w-fit")
            {
                return new List<(string property, UssValue value)> {
                ("max-width", new StaticValue(" fit-content"))
              };
            }
            else if (className.StartsWith("max-w-"))
            {
                string suffix = className[6..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("max-width", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>max-w-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("max-width", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }

            //height
            if (className == "h-auto")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" auto"))
              };
            }
            else if (className == "h-px")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 1px"))
              };
            }
            else if (className == "h-full")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100%"))
              };
            }
            else if (className == "h-screen")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100vh"))
              };
            }
            else if (className == "h-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "h-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "h-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100lvh"))
              };
            }
            else if (className == "h-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "h-svh")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100svh"))
              };
            }
            else if (className == "h-svw")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 100svw"))
              };
            }
            else if (className == "h-min")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" min-content"))
              };
            }
            else if (className == "h-max")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" max-content"))
              };
            }
            else if (className == "h-fit")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" fit-content"))
              };
            }
            else if (className == "h-lh")
            {
                return new List<(string property, UssValue value)> {
                ("height", new StaticValue(" 1lh"))
              };
            }
            else if (className.StartsWith("h-"))
            {
                string suffix = className[2..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("height", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>h-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("height", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }


            //min-height
            if (className == "min-h-px")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 1px"))
              };
            }
            else if (className == "min-h-full")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100%"))
              };
            }
            else if (className == "min-h-screen")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100vh"))
              };
            }
            else if (className == "min-h-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "min-h-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "min-h-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100lvh"))
              };
            }
            else if (className == "min-h-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "min-h-svw")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100svw"))
              };
            }
            else if (className == "min-h-svh")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 100svh"))
              };
            }
            else if (className == "min-h-auto")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" auto"))
              };
            }
            else if (className == "min-h-min")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" min-content"))
              };
            }
            else if (className == "min-h-max")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" max-content"))
              };
            }
            else if (className == "min-h-fit")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" fit-content"))
              };
            }
            else if (className == "min-h-lh")
            {
                return new List<(string property, UssValue value)> {
                ("min-height", new StaticValue(" 1lh"))
              };
            }
            else if (className.StartsWith("min-h-"))
            {
                string suffix = className[6..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("min-height", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>min-h-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("min-height", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }


            //max-height
            if (className == "max-h-none")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" none"))
              };
            }
            else if (className == "max-h-px")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 1px"))
              };
            }
            else if (className == "max-h-full")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100%"))
              };
            }
            else if (className == "max-h-screen")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100vh"))
              };
            }
            else if (className == "max-h-dvh")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100dvh"))
              };
            }
            else if (className == "max-h-dvw")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100dvw"))
              };
            }
            else if (className == "max-h-lvh")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100lvh"))
              };
            }
            else if (className == "max-h-lvw")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100lvw"))
              };
            }
            else if (className == "max-h-svh")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100svh"))
              };
            }
            else if (className == "max-h-svw")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 100svw"))
              };
            }
            else if (className == "max-h-min")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" min-content"))
              };
            }
            else if (className == "max-h-max")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" max-content"))
              };
            }
            else if (className == "max-h-fit")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" fit-content"))
              };
            }
            else if (className == "max-h-lh")
            {
                return new List<(string property, UssValue value)> {
                ("max-height", new StaticValue(" 1lh"))
              };
            }
            else if (className.StartsWith("max-h-"))
            {
                string suffix = className[6..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("max-height", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>max-h-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("max-height", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }


            return null;
        }

        private bool CanParseSizing(string className)
        {
            return className == "w-3xs"
                || className == "w-2xs"
                || className == "w-xs"
                || className == "w-sm"
                || className == "w-md"
                || className == "w-lg"
                || className == "w-xl"
                || className == "w-2xl"
                || className == "w-3xl"
                || className == "w-4xl"
                || className == "w-5xl"
                || className == "w-6xl"
                || className == "w-7xl"
                || className == "w-auto"
                || className == "w-px"
                || className == "w-full"

                || className == "size-auto"
                || className == "size-px"
                || className == "size-full"




                || className == "min-w-3xs"
                || className == "min-w-2xs"
                || className == "min-w-xs"
                || className == "min-w-sm"
                || className == "min-w-md"
                || className == "min-w-lg"
                || className == "min-w-xl"
                || className == "min-w-2xl"
                || className == "min-w-3xl"
                || className == "min-w-4xl"
                || className == "min-w-5xl"
                || className == "min-w-6xl"
                || className == "min-w-7xl"
                || className == "min-w-auto"
                || className == "min-w-px"
                || className == "min-w-full"



                || className == "max-w-3xs"
                || className == "max-w-2xs"
                || className == "max-w-xs"
                || className == "max-w-sm"
                || className == "max-w-md"
                || className == "max-w-lg"
                || className == "max-w-xl"
                || className == "max-w-2xl"
                || className == "max-w-3xl"
                || className == "max-w-4xl"
                || className == "max-w-5xl"
                || className == "max-w-6xl"
                || className == "max-w-7xl"
                || className == "max-w-none"
                || className == "max-w-px"
                || className == "max-w-full"

                || className == "container"


                || className == "h-auto"
                || className == "h-px"
                || className == "h-full"



                || className == "min-h-px"
                || className == "min-h-full"



                || className == "max-h-none"
                || className == "max-h-px"
                || className == "max-h-full"



                || className.StartsWith("size-")
                || className.StartsWith("w-")
                || className.StartsWith("h-")
                || className.StartsWith("min-w-")
                || className.StartsWith("min-h-")
                || className.StartsWith("max-w-")
                || className.StartsWith("max-h-");
        }
    }
}