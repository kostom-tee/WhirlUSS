using System;
using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class CustomComponent : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => throw new NotImplementedException();

        public override bool CanParse(string className)
        {
            return ProcessFile.CustomComponent.ContainsKey(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            if (ProcessFile.CustomComponent.ContainsKey(className))
            {
                List<(string property, UssValue value)> values = new List<(string, UssValue)>();

                foreach (var item in ProcessFile.CustomComponent[className].utilities)
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

        public List<(string property, UssValue value)>? GetCssPropertyAndValueNoSelector(string className)
        {
            if (ProcessFile.CustomComponent.ContainsKey(className))
            {
                List<(string property, UssValue value)> values = new List<(string, UssValue)>();

                foreach (var item in ProcessFile.CustomComponent[className].utilities)
                {
                    if (item.Contains(':')) continue;

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

        public Dictionary<List<StatePrefix>, List<(string, UssValue)>>? GetCssPropertyAndValueWithSelector(string className)
        {
            Dictionary<List<StatePrefix>, List<(string, UssValue)>> val = new Dictionary<List<StatePrefix>, List<(string, UssValue)>>();

            if (ProcessFile.CustomComponent.ContainsKey(className))
            {
                foreach (var item in ProcessFile.CustomComponent[className].utilities)
                {
                    if (!item.Contains(':')) continue;

                    var (prefixes, baseClass) = ClassParser.ParsePrefixes(item);
                    if (!prefixes.Any()) continue;


                    if (val.Count == 0)
                    {
                        var value = ClassParser.ParseAndGetPropertyAndValue(baseClass);
                        if (value == null) continue;
                        val.Add(prefixes, value);
                    }
                    else
                    {
                        var result = ExistsInListOfLists(val.Keys.ToList(), prefixes);

                        if (result.Any())
                        {
                            if (!val.ContainsKey(result[0])) continue;

                            if (val.TryGetValue(result[0], out var res))
                            {
                                var value = ClassParser.ParseAndGetPropertyAndValue(baseClass);
                                if (value != null)
                                    res.AddRange(value);
                            }
                        }
                        else
                        {
                            var value = ClassParser.ParseAndGetPropertyAndValue(baseClass);

                            if (value != null)
                                val.Add(prefixes, value);

                        }
                    }
                }
            }

            return val.Count == 0 ? null : val;
        }

        private List<List<StatePrefix>> ExistsInListOfLists(List<List<StatePrefix>> listOfLists, List<StatePrefix> target)
        {
            var normalizedTarget = target.OrderBy(x => x.Value).ToList();
            List<List<StatePrefix>> result = new List<List<StatePrefix>>();

            foreach (var item in listOfLists)
            {
                var _item = item.OrderBy(x => x.Value).ToList();

                if (_item.Count == normalizedTarget.Count)
                {
                    bool same = true;
                    for (int i = 0; i < _item.Count; i++)
                    {
                        if (_item[i].Value != normalizedTarget[i].Value || _item[i].Type != normalizedTarget[i].Type)
                        {
                            same = false;
                            break;
                        }
                    }

                    if (same)
                    {
                        result.Add(item);
                        //only need 1 to match
                        break;
                    }
                }
            }

            return result;
        }
    }
}