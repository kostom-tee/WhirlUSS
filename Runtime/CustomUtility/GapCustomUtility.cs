using System;
using System.Collections.Generic;

namespace Kostom.Style
{
    public class GapCustomUtility : CustomUtility
	{
        public override bool CanParse(string className)
        {
           return className.StartsWith("gap-");
        }

        public override List<StyleProperty>[]? GetUssPropertyAndValue(string className)
        {
            string suffix;
            if (className.StartsWith("gap-x-"))
            {
                suffix = className["gap-x-".Length..];

                if (float.TryParse(suffix, out var val)) {
                    return new List<StyleProperty>[]
                    {
                        new List<StyleProperty>
                        {
                            new StyleProperty
                            {
                                property = "margin-right",
                                value = $"{val * WhirlManager.DefaultSpacing}px"
                            }
                        }
                    };
                }
            }
            else if (className.StartsWith("gap-y-"))
            {
                suffix = className["gap-y-".Length..];
                if (float.TryParse(suffix, out var val))
                {
                    return new List<StyleProperty>[]
                    {
                        new List<StyleProperty>
                        {
                            new StyleProperty
                            {
                                property = "margin-bottom",
                                value = $"{val * WhirlManager.DefaultSpacing}px"
                            }
                        }
                    };
                }
            }
            else if (className.StartsWith("gap-"))
            {
                suffix = className["gap-".Length..];
                if (float.TryParse(suffix, out var val))
                {
                    return new List<StyleProperty>[]
                    {
                        new List<StyleProperty>
                        {
                            new StyleProperty
                            {
                                property = "margin-right",
                                value = $"{val * WhirlManager.DefaultSpacing}px"
                            }
                        },
                        new List<StyleProperty>
                        {
                            new StyleProperty
                            {
                                property = "margin-bottom",
                                value = $"{val * WhirlManager.DefaultSpacing}px"
                            }
                        }
                    };
                }
            }

            return null;
        }

        public override string[]? AdditionalUtilityName(string className)
        {
            if (className.StartsWith("gap-x-") || className.StartsWith("gap-y-"))
            {
                return new string[] { $".child-gap-{className["gap-".Length..]}" };
            }
            else if (className.StartsWith("gap-"))
            {
                return new string[2] {
                    $"> .child-gap-x-{className["gap-".Length..]}",
                    $"> .child-gap-y-{className["gap-".Length..]}",
                };
            }
            return null;
        }
    }
}

