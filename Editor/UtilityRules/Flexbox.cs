using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kostom.Style
{
    internal class Flexbox : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[] {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable,
        };

        public override bool CanParse(string className)
        {
            if (Helper.UnityVersion.ToString().StartsWith("2022"))
            {
                return CanParse2022(className);
            }

            return CanParseFlex(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            string basis = "flex-basis";
            string direction = "flex-direction";
            string wrap = "flex-wrap";
            string flex = "flex";
            string grow = "flex-grow";
            SupportedValueType? detectedType;

            //basis
            if (className == "basis-full")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("100%")) };
            }
            else if (className == "basis-auto")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("auto")) };
            }
            else if (className == "basis-3xs")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("256px")) };
            }
            else if (className == "basis-2xs")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("288px")) };
            }
            else if (className == "basis-xs")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("320px")) };
            }
            else if (className == "basis-sm")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("384px")) };
            }
            else if (className == "basis-md")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("448px")) };
            }
            else if (className == "basis-lg")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("512px")) };
            }
            else if (className == "basis-xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("576px")) };
            }
            else if (className == "basis-2xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("672px")) };
            }
            else if (className == "basis-3xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("768px")) };
            }
            else if (className == "basis-4xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("896px")) };
            }
            else if (className == "basis-5xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("1024px")) };
            }
            else if (className == "basis-6xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("1152px")) };
            }
            else if (className == "basis-7xl")
            {
                return new List<(string property, UssValue value)> { (basis, new StaticValue("1280px")) };
            }
            else if (className.StartsWith("basis-"))
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


                    return new List<(string property, UssValue value)> { (basis, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>flex-basis-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { (basis, new StaticValue((val * ProcessFile.DefaultSpacing).ToString())) };
                }
            }

            //direction
            if (className == "flex-row")
            {
                return new List<(string property, UssValue value)> { (direction, new StaticValue("row")) };
            }
            else if (className == "flex-row-reverse")
            {
                return new List<(string property, UssValue value)> { (direction, new StaticValue("row-reverse")) };
            }
            else if (className == "flex-col")
            {
                return new List<(string property, UssValue value)> { (direction, new StaticValue("column")) };
            }
            else if (className == "flex-col-reverse")
            {
                return new List<(string property, UssValue value)> { (direction, new StaticValue("column-reverse")) };
            }


            //wrap
            if (className == "flex-nowrap")
            {
                return new List<(string property, UssValue value)> { (wrap, new StaticValue("nowrap")) };
            }
            else if (className == "flex-wrap")
            {
                return new List<(string property, UssValue value)> { (wrap, new StaticValue("wrap")) };
            }
            else if (className == "flex-wrap-reverse")
            {
                return new List<(string property, UssValue value)> { (wrap, new StaticValue("wrap-reverse")) };
            }


            //flex
            if (className == "flex-auto")
            {
                return new List<(string property, UssValue value)> { (flex, new StaticValue("auto")) };
            }
            else if (className == "flex-initial")
            {
                return new List<(string property, UssValue value)> { (flex, new StaticValue("0 auto")) };
            }
            else if (className == "flex-none")
            {
                return new List<(string property, UssValue value)> { (flex, new StaticValue("none")) };
            }
            else if (className.StartsWith("flex-"))
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


                    return new List<(string property, UssValue value)> { (flex, cssValue) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { (flex, new StaticValue(suffix)) };
                }
            }


            //grow
            if (className == "grow")
            {
                return new List<(string property, UssValue value)> { (grow, new StaticValue("1")) };
            }
            else if (className.StartsWith("grow-"))
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


                    return new List<(string property, UssValue value)> { (grow, cssValue) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { (grow, new StaticValue(suffix)) };
                }
            }

            //shrink
            if (className == "shrink")
            {
                return new List<(string property, UssValue value)> { ("shrink", new StaticValue("1")) };
            }
            else if (className.StartsWith("shrink-"))
            {
                string suffix = className[7..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("shrink", cssValue) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { ("shrink", new StaticValue(suffix)) };
                }
            }


            //justify-content
            if (className == "justify-start")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("flex-start")) };
            }
            else if (className == "justify-end")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("flex-end")) };
            }
            else if (className == "justify-end-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("safe flex-end")) };
            }
            else if (className == "justify-center")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("center")) };
            }
            else if (className == "justify-center-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("safe center")) };
            }
            else if (className == "justify-between")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("space-between")) };
            }
            else if (className == "justify-around")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("space-around")) };
            }
            else if (className == "justify-evenly")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("space-evenly")) };
            }
            else if (className == "justify-stretch")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("stretch")) };
            }
            else if (className == "justify-baseline")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("baseline")) };
            }
            else if (className == "justify-normal")
            {
                return new List<(string property, UssValue value)> { ("justify-content", new StaticValue("normal")) };
            }

            //justify-items
            if (className == "justify-items-start")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("start")) };
            }
            else if (className == "justify-items-end")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("end")) };
            }
            else if (className == "justify-items-end-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("safe end")) };
            }
            else if (className == "justify-items-center")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("center")) };
            }
            else if (className == "justify-items-center-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("safe center")) };
            }
            else if (className == "justify-items-stretch")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("stretch")) };
            }
            else if (className == "justify-items-normal")
            {
                return new List<(string property, UssValue value)> { ("justify-items", new StaticValue("normal")) };
            }


            //justify-self
            if (className == "justify-self-auto")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("auto")) };
            }
            else if (className == "justify-self-start")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("start")) };
            }
            else if (className == "justify-self-center")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("center")) };
            }
            else if (className == "justify-self-center-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("safe center")) };
            }
            else if (className == "justify-self-end")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("end")) };
            }
            else if (className == "justify-self-end-safe")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("safe end")) };
            }
            else if (className == "justify-self-stretch")
            {
                return new List<(string property, UssValue value)> { ("justify-self", new StaticValue("stretch")) };
            }

            //align-content
            if (className == "content-normal")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("normal")) };
            }
            else if (className == "content-center")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("center")) };
            }
            else if (className == "content-start")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("flex-start")) };
            }
            else if (className == "content-end")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("flex-end")) };
            }
            else if (className == "content-between")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("space-between")) };
            }
            else if (className == "content-around")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("space-around")) };
            }
            else if (className == "content-evenly")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("space-evenly")) };
            }
            else if (className == "content-baseline")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("baseline")) };
            }
            else if (className == "content-stretch")
            {
                return new List<(string property, UssValue value)> { ("align-content", new StaticValue("stretch")) };
            }

            //align-items
            if (className == "items-start")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("flex-start")) };
            }
            else if (className == "items-auto")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("auto")) };
            }
            else if (className == "items-end")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("flex-end")) };
            }
            else if (className == "items-end-safe")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("safe flex-end")) };
            }
            else if (className == "items-center")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("center")) };
            }
            else if (className == "items-center-safe")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("safe center")) };
            }
            else if (className == "items-baseline")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("baseline")) };
            }
            else if (className == "items-baseline-last")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("last baseline")) };
            }
            else if (className == "items-stretch")
            {
                return new List<(string property, UssValue value)> { ("align-items", new StaticValue("stretch")) };
            }

            //align-self
            if (className == "self-auto")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("auto")) };
            }
            else if (className == "self-start")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("flex-start")) };
            }
            else if (className == "self-end")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("flex-end")) };
            }
            else if (className == "self-end-safe")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("safe flex-end")) };
            }
            else if (className == "self-center")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("center")) };
            }
            else if (className == "self-center-safe")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("safe center")) };
            }
            else if (className == "self-stretch")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("stretch")) };
            }
            else if (className == "self-baseline")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("baseline")) };
            }
            else if (className == "self-baseline-last")
            {
                return new List<(string property, UssValue value)> { ("align-self", new StaticValue("last baseline")) };
            }

            //place-content
            if (className == "place-content-center")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("center")) };
            }
            else if (className == "place-content-center-safe")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("safe center")) };
            }
            else if (className == "place-content-start")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("start")) };
            }
            else if (className == "place-content-end")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("end")) };
            }
            else if (className == "place-content-end-safe")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("safe end")) };
            }
            else if (className == "place-content-between")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("space-between")) };
            }
            else if (className == "place-content-around")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("space-around")) };
            }
            else if (className == "place-content-evenly")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("space-evenly")) };
            }
            else if (className == "place-content-baseline")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("baseline")) };
            }
            else if (className == "place-content-stretch")
            {
                return new List<(string property, UssValue value)> { ("place-content", new StaticValue("stretch")) };
            }

            //place-items
            if (className == "place-items-start")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("start")) };
            }
            else if (className == "place-items-end")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("end")) };
            }
            else if (className == "place-items-end-safe")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("safe end")) };
            }
            else if (className == "place-items-center")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("center")) };
            }
            else if (className == "place-items-center-safe")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("safe center")) };
            }
            else if (className == "place-items-baseline")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("baseline")) };
            }
            else if (className == "place-items-stretch")
            {
                return new List<(string property, UssValue value)> { ("place-items", new StaticValue("stretch")) };
            }

            //place-self
            if (className == "place-self-auto")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("auto")) };
            }
            else if (className == "place-self-start")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("start")) };
            }
            else if (className == "place-self-end")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("end")) };
            }
            else if (className == "place-self-end-safe")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("safe end")) };
            }
            else if (className == "place-self-center")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("center")) };
            }
            else if (className == "place-self-center-safe")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("safe center")) };
            }
            else if (className == "place-self-stretch")
            {
                return new List<(string property, UssValue value)> { ("place-self", new StaticValue("stretch")) };
            }

            return null;
        }

        private bool CanParseFlex(string className)
        {
            return className == "basis-full"
                || className == "basis-auto"
                || className == "basis-3xs"
                || className == "basis-2xs"
                || className == "basis-xs"
                || className == "basis-sm"
                || className == "basis-md"
                || className == "basis-lg"
                || className == "basis-xl"
                || className == "basis-2xl"
                || className == "basis-3xl"
                || className == "basis-4xl"
                || className == "basis-5xl"
                || className == "basis-6xl"
                || className == "basis-7xl"
                || className.StartsWith("basis-")

                || className == "flex-row"
                || className == "flex-row-reverse"
                || className == "flex-col"
                || className == "flex-col-reverse"

                || className == "flex-nowrap"
                || className == "flex-wrap"
                || className == "flex-wrap-reverse"

                || className == "flex-auto"
                || className == "flex-initial"
                || className == "flex-none"
                || className.StartsWith("flex-")

                || className == "grow"
                || className.StartsWith("grow-")

                || className == "shrink"
                || className.StartsWith("shrink-")

                || className == "grow"
                || className.StartsWith("grow-")

                || className == "justify-start"
                || className == "justify-end"
                || className == "justify-end-safe"
                || className == "justify-center"
                || className == "justify-center-safe"
                || className == "justify-between"
                || className == "justify-around"
                || className == "justify-evenly"
                || className == "justify-stretch"
                || className == "justify-baseline"
                || className == "justify-normal"

                || className == "justify-items-start"
                || className == "justify-items-end"
                || className == "justify-items-end-safe"
                || className == "justify-items-center"
                || className == "justify-items-center-safe"
                || className == "justify-items-stretch"
                || className == "justify-items-normal"

                || className == "justify-self-auto"
                || className == "justify-self-start"
                || className == "justify-self-center"
                || className == "justify-self-center-safe"
                || className == "justify-self-end"
                || className == "justify-self-end-safe"
                || className == "justify-self-stretch"

                || className == "content-normal"
                || className == "content-center"
                || className == "content-start"
                || className == "content-end"
                || className == "content-between"
                || className == "content-around"
                || className == "content-evenly"
                || className == "content-baseline"
                || className == "content-stretch"

                || className == "items-auto"
                || className == "items-start"
                || className == "items-end"
                || className == "items-end-safe"
                || className == "items-center"
                || className == "items-center-safe"
                || className == "items-baseline"
                || className == "items-baseline-last"
                || className == "items-stretch"

                || className == "self-auto"
                || className == "self-start"
                || className == "self-end"
                || className == "self-end-safe"
                || className == "self-center"
                || className == "self-center-safe"
                || className == "self-stretch"
                || className == "self-baseline"
                || className == "self-baseline-last"

                || className == "place-center-center"
                || className == "place-content-center-safe"
                || className == "place-content-start"
                || className == "place-content-end"
                || className == "place-content-end-safe"
                || className == "place-content-between"
                || className == "place-content-around"
                || className == "place-content-evenly"
                || className == "place-content-baseline"
                || className == "place-content-stretch"

                || className == "place-items-start"
                || className == "place-items-end"
                || className == "place-items-end-safe"
                || className == "place-items-center"
                || className == "place-items-center-safe"
                || className == "place-items-baseline"
                || className == "place-items-stretch"

                || className == "place-self-auto"
                || className == "place-self-start"
                || className == "place-self-end"
                || className == "place-self-end-safe"
                || className == "place-self-center"
                || className == "place-self-center-safe"
                || className == "place-self-stretch";
        }

        private bool CanParse2022(string className)

        {
            return className == "basis-full"
                || className == "basis-auto"
                || className == "basis-3xs"
                || className == "basis-2xs"
                || className == "basis-xs"
                || className == "basis-sm"
                || className == "basis-md"
                || className == "basis-lg"
                || className == "basis-xl"
                || className == "basis-2xl"
                || className == "basis-3xl"
                || className == "basis-4xl"
                || className == "basis-5xl"
                || className == "basis-6xl"
                || className == "basis-7xl"
                || className.StartsWith("basis-")

                || className == "flex-row"
                || className == "flex-row-reverse"
                || className == "flex-col"
                || className == "flex-col-reverse"

                || className == "flex-nowrap"
                || className == "flex-wrap"
                || className == "flex-wrap-reverse"

                || className == "flex-auto"
                || className == "flex-initial"
                || className == "flex-none"
                || className.StartsWith("flex-")

                || className == "grow"
                || className.StartsWith("grow-")

                || className == "shrink"
                || className.StartsWith("shrink-")

                || className == "grow"
                || className.StartsWith("grow-")

                || className == "justify-start"
                || className == "justify-end"
                || className == "justify-end-safe"
                || className == "justify-center"
                || className == "justify-center-safe"
                || className == "justify-between"
                || className == "justify-around"
                || className == "justify-evenly"
                || className == "justify-stretch"
                || className == "justify-baseline"
                || className == "justify-normal"

                || className == "content-normal"
                || className == "content-center"
                || className == "content-start"
                || className == "content-end"
                || className == "content-between"
                || className == "content-around"
                || className == "content-evenly"
                || className == "content-baseline"
                || className == "content-stretch"

                || className == "items-auto"
                || className == "items-start"
                || className == "items-end"
                || className == "items-end-safe"
                || className == "items-center"
                || className == "items-center-safe"
                || className == "items-baseline"
                || className == "items-baseline-last"
                || className == "items-stretch"

                || className == "self-auto"
                || className == "self-start"
                || className == "self-end"
                || className == "self-end-safe"
                || className == "self-center"
                || className == "self-center-safe"
                || className == "self-stretch"
                || className == "self-baseline"
                || className == "self-baseline-last";
        }
    }
}