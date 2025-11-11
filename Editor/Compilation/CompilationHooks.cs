using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [InitializeOnLoad]
    internal static class CompilationHooks
    {
        static CompilationHooks()
        {
            CompilationPipeline.compilationFinished += OnCompilationFinished;
            ProcessFile.Compile();
        }

        private static void OnCompilationFinished(object obj)
        {
            ProcessFile.Compile();
        }
    }

    internal class ChangeWatcher : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (var item in ProcessFile.WhirlCompilerSettings)
            {
                string[] basePaths = item.GetPaths();
                if (item.styleSheet == null) continue;
                if (basePaths.Length == 0)
                {
                    ProcessFile.AddStyles(item.styleSheet, "");
                    continue;
                }

                //unity scene save
                if(importedAssets.Length == 1 && importedAssets[0].EndsWith(".unity"))
                {
                    ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
                    return;
                }

                string[] validImportedAssets = GetValidImportedAssets(basePaths, movedAssets.Length > 0 ? movedAssets : importedAssets);

                ProcessFile.ProcessUtility(basePaths);


                if(movedAssets.Length > 0 && validImportedAssets.Length == 0)
                {
                    ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
                    return;
                }

                if(importedAssets.Length == 1 && importedAssets[0].EndsWith(".asset") && AssetDatabase.LoadAssetAtPath<WhirlCompilerSettings>(importedAssets[0]) != null)
                {
                    ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
                    return;
                }

                if(importedAssets.Length == 1 && importedAssets[0].EndsWith(".asset") && AssetDatabase.LoadAssetAtPath<Theme>(importedAssets[0]) != null)
                {
                    ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
                    return;
                }

                if (validImportedAssets.Length == 1 && validImportedAssets[0].EndsWith(".asset"))
                {
                    if ((AssetDatabase.LoadAssetAtPath<StyleUtilities>(validImportedAssets[0]) != null) || (AssetDatabase.LoadAssetAtPath<StyleUtilityCombo>(validImportedAssets[0]) != null) || (AssetDatabase.LoadAssetAtPath<StyleComponent>(validImportedAssets[0]) != null))
                    {
                        ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
                    }

                    return;
                }

                if (validImportedAssets.Length == 0) continue;

                ProcessFile.AddStyles(item.styleSheet, ProcessFile.ProcessAllFiles(basePaths));
            }
        }

        private static string[] GetValidImportedAssets(string[] basePaths, string[] importedAssets)
        {
            List<string> paths = new List<string>();

            foreach (var path in basePaths)
            {
                foreach (var assetPath in importedAssets)
                {
                    if (assetPath.StartsWith(path))
                    {
                        if (!paths.Contains(assetPath))
                        {
                            paths.Add(assetPath);
                        }
                    }
                }
            }

            return paths.ToArray();
        }
    }

    internal static class ProcessFile
    {
        public static float DefaultSpacing {
            get
            {
                float val = 4;
                if(CustomTheme.ContainsKey("spacing") && CustomTheme["spacing"].ContainsKey("default"))
                {
                    if (!float.TryParse(CustomTheme["spacing"]["default"].Render().Replace("px","").Trim(), out val)) {
                        val = 4;
                    }
                }
                return val;
            }
        }
        public static Theme[] StyleThemes
        {
            get
            {
                string[] guids = AssetDatabase.FindAssets("t:Theme", new[] { "Assets" });
                List<Theme> results = new List<Theme>();

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<Theme>(path);

                    if (asset != null && asset.isActive)
                        results.Add(asset);
                }

                return results.ToArray();
            }
        }
        public static WhirlCompilerSettings[] WhirlCompilerSettings
        {
            get
            {
                string[] guids = AssetDatabase.FindAssets("t:WhirlCompilerSettings", new[] { "Assets" });
                List<WhirlCompilerSettings> results = new List<WhirlCompilerSettings>();

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<WhirlCompilerSettings>(path);

                    if (asset != null)
                        results.Add(asset);
                }

                return results.ToArray();
            }
        }

        public static Dictionary<string, List<(string property, UssValue value)>> StyleUtilities { get; private set; } = new Dictionary<string, List<(string property, UssValue value)>>();
        public static Dictionary<string, UtilityCombo> UtilityCombo { get; private set; } = new Dictionary<string, UtilityCombo>();
        public static Dictionary<string, Component> CustomComponent { get; private set; } = new Dictionary<string, Component>();
        public static Dictionary<string, Dictionary<string, UssValue>> CustomTheme { get; private set; } = new Dictionary<string, Dictionary<string, UssValue>>();
        public static List<string> ExtraRootVariables { get; private set; } = new List<string>();

        public static void Compile()
        {
            foreach (var item in WhirlCompilerSettings)
            {
                string[] paths = item.GetPaths();
                if (item.styleSheet == null) continue;
                if (paths.Length == 0) continue;

                ProcessUtility(paths);
                AddStyles(item.styleSheet, ProcessAllFiles(paths));
            }
        }

        public static void ProcessUtility(string[] basePath)
        {
            StyleUtilities = new Dictionary<string, List<(string property, UssValue value)>>();
            UtilityCombo = new Dictionary<string, UtilityCombo>();
            CustomComponent = new Dictionary<string, Component>();
            CustomTheme = new Dictionary<string, Dictionary<string, UssValue>>();
            ExtraRootVariables = new List<string>();


            ExtraRootVariables.AddRange(StyleThemes.ParseTheme(CustomTheme));

            StyleUtilities[] _StyleUtilities = GetStyleUtilities(basePath);
            foreach (var item in _StyleUtilities)
            {
                if (item.utilities != null)
                {
                    foreach (var utility in item.utilities)
                    {
                        if (!string.IsNullOrEmpty(utility.selector) && !StyleUtilities.ContainsKey(utility.selector))
                        {
                            StyleUtilities.Add(utility.selector, new List<(string property, UssValue value)>());
                            foreach (var styleProperty in utility.styleProperties)
                            {
                                if (string.IsNullOrEmpty(styleProperty.property) || string.IsNullOrEmpty(styleProperty.value)) continue;
                                StyleUtilities[utility.selector].Add((styleProperty.property, new StaticValue(styleProperty.value)));
                            }
                        }
                    }
                }
            }

            StyleUtilityCombo[] UtilityComboSettings = GetUtilityComboSettings(basePath);
            foreach (var item in UtilityComboSettings)
            {
                if (item.utilityCombos != null)
                {
                    foreach (var utility in item.utilityCombos)
                    {
                        if (utility == null) continue;
                        if (!string.IsNullOrEmpty(utility.selector) && !UtilityCombo.ContainsKey(utility.selector))
                        {
                            UtilityCombo.Add(utility.selector, utility);
                        }
                        else if (!string.IsNullOrEmpty(utility.selector) && UtilityCombo.ContainsKey(utility.selector))
                        {
                            UtilityCombo[utility.selector] = utility;
                        }
                    }
                }
            }

            StyleComponent[] StyleComponent = GetStyleComponent(basePath);
            foreach (var item in StyleComponent)
            {
                if (item.components == null) continue;

                foreach (var component in item.components)
                {
                    if (component == null) continue;
                    if (!string.IsNullOrEmpty(component.selector) && !UtilityCombo.ContainsKey(component.selector))
                    {
                        CustomComponent.Add(component.selector, component);
                    }
                    else if (!string.IsNullOrEmpty(component.selector) && UtilityCombo.ContainsKey(component.selector))
                    {
                        CustomComponent[component.selector] = component;
                    }
                }
            }
        }

        public static List<string> GetValidClasses(string path, List<string> classes)
        {
            string fullPath = Path.Combine(Application.dataPath, path["Assets/".Length..]);
            string line, classValue;
            bool IsBlockComment = false;
            Regex regex = new Regex(@"@?""((?:\\.|[^""\\])*)""", RegexOptions.Compiled);
            MatchCollection matches;


            if (!File.Exists(fullPath))
            {
                return classes;
            }

            foreach (var _line in File.ReadLines(path))
            {
                line = _line.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("//")) continue;
                if (!IsBlockComment && line.StartsWith("/*"))
                {
                    IsBlockComment = true;
                    if (line.EndsWith("*/"))
                    {
                        IsBlockComment = false;
                    }
                    continue;
                }
                else if (IsBlockComment && line.EndsWith("*/"))
                {
                    IsBlockComment = false;
                    continue;
                }
                else if (IsBlockComment)
                {
                    continue;
                }

                matches = regex.Matches(line);
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        classValue = ClassParser.ValidClassList(matches[i].Value.Replace("\"", ""));

                        if (string.IsNullOrEmpty(classValue)) continue;
                        classes.AddRange(classValue.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().FindAll(x=> !classes.Contains(x)));
                    }
                }
            }

            return classes;
        }

        public static string ProcessAllFiles(string[] basePath)
        {
            string[] scriptPaths = GetAssetsWithPostfix(".cs", basePath);
            string[] uxmlPaths = GetAssetsWithPostfix(".uxml", basePath);
            List<string> classes = new List<string>();

            string styles = "";
            foreach (var sp in scriptPaths)
            {
                GetValidClasses(sp, classes);
            }

            foreach (var sp in uxmlPaths)
            {
                GetValidClasses(sp, classes);
            }

            classes = classes.OrderBy(s => ClassParser.SplitByAST(s, ':').Count).ThenBy(s => s).ToList();
            classes = classes.OrderBy(s =>
            {
                var bp = GetBreakPoint(s);
                if (string.IsNullOrEmpty(bp)) return 0;
                if(float.TryParse(CustomTheme["breakpoint"][bp].Render(), out var val))
                {
                    return val;
                }
                return 0;
            }).ThenBy(s => s).ToList();

            foreach (var item in classes)
            {
                var parsedVal = ClassParser.Parse(item);
                if (string.IsNullOrEmpty(parsedVal)) continue;
                styles += $"{parsedVal}\n";
            }

            return styles.Trim();
        }

        public static bool ContainInValid(string path)
        {
            return path.EndsWith($"{nameof(CompilationHooks)}.cs") || path.EndsWith("StyleUtility.cs") || path.EndsWith("Theme.cs") || path.EndsWith("StyleUtilityCombo.cs") || path.EndsWith("StyleComponent.cs") || path.EndsWith("EditorTools.cs") || path.EndsWith("WhirlCompilerSettings.cs"); 
        }

        public static void AddStyles(StyleSheet styleSheet, string content)
        {
            if (styleSheet == null) return;
            string path = AssetDatabase.GetAssetPath(styleSheet);
            if (string.IsNullOrEmpty(path)) return;
            string fullPath = Path.Combine(Application.dataPath, path["Assets/".Length..]);

            if (!File.Exists(fullPath)) return;
            string style = default;

            //add root data
            string root = CompileRoot();
            if (!string.IsNullOrEmpty(root))
            {
                style = $":root{{\n{root.Trim()}\n}}";
            }
            style += $"\n{content}";

            File.WriteAllText(fullPath, style.Trim());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string CompileRoot()
        {
            string content = default;
            foreach (var item in CustomTheme)
            {
                foreach (var itemValue in item.Value)
                {
                    if(item.Key == "spacing" && itemValue.Key == "default")
                    {
                        content += $"\n--spacing: {itemValue.Value.Render()};";
                        continue;
                    }
                    content += $"\n--{item.Key}-{itemValue.Key}: {itemValue.Value.Render()};";
                }
            }

            foreach (var item in ExtraRootVariables)
            {
                content += $"\n{item}";
            }
            return content;
        }

        private static string GetBreakPoint(string @class)
        {
            if (CustomTheme == null || CustomTheme["breakpoint"] == null) return string.Empty;

            return @class.Split(':', StringSplitOptions.RemoveEmptyEntries)[..^1].FirstOrDefault(x => CustomTheme["breakpoint"].ContainsKey(x));
        }

        private static string[] GetAssetsWithPostfix(string postfix, string[] basePath)
        {
            postfix = postfix.Trim();
            postfix = postfix.StartsWith(".") && postfix.Length > 1 ? postfix[1..] : postfix;

            string[] guids = AssetDatabase.FindAssets("t:Object", basePath);
            List<string> results = new List<string>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!ContainInValid(path) && path.EndsWith($".{postfix}"))
                {
                    results.Add(path);
                }
            }

            return results.ToArray();
        }

        private static StyleUtilities[] GetStyleUtilities(string[] basePath)
        {
            string[] guids = AssetDatabase.FindAssets("t:StyleUtilities", basePath );
            List<StyleUtilities> results = new List<StyleUtilities>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<StyleUtilities>(path);

                if (asset != null)
                    results.Add(asset);
            }

            return results.ToArray();
        }

        private static StyleComponent[] GetStyleComponent(string[] basePath)
        {
            string[] guids = AssetDatabase.FindAssets("t:StyleComponent", basePath);
            List<StyleComponent> results = new List<StyleComponent>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<StyleComponent>(path);

                if (asset != null)
                    results.Add(asset);
            }

            return results.ToArray();
        }

        private static StyleUtilityCombo[] GetUtilityComboSettings(string[] basePath)
        {
            string[] guids = AssetDatabase.FindAssets("t:StyleUtilityCombo", basePath);
            List<StyleUtilityCombo> results = new List<StyleUtilityCombo>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<StyleUtilityCombo>(path);

                if (asset != null)
                    results.Add(asset);
            }

            return results.ToArray();
        }
    }

    internal static class ClassParser
    {
        private static readonly List<UtilityRule> Rules = new List<UtilityRule>()
        {
            ScriptableObject.CreateInstance<CustomUtilities>(),
            ScriptableObject.CreateInstance<Background>(),
            ScriptableObject.CreateInstance<Arbitrary>(),
            ScriptableObject.CreateInstance<Effects>(),
            ScriptableObject.CreateInstance<Layout>(),
            ScriptableObject.CreateInstance<Flexbox>(),
            ScriptableObject.CreateInstance<Spacing>(),
            ScriptableObject.CreateInstance<Sizing>(),
            ScriptableObject.CreateInstance<Typograghy>(),
            ScriptableObject.CreateInstance<Borders>(),
            ScriptableObject.CreateInstance<Filters>(),
            ScriptableObject.CreateInstance<Transitions>(),
            ScriptableObject.CreateInstance<Transforms>(),
        };
        private static readonly CustomComponent CustomComponent = ScriptableObject.CreateInstance<CustomComponent>();
        private static readonly Dictionary<string, PrefixType> PrefixTypes = new Dictionary<string, PrefixType>()
        {
            { "*", PrefixType.Selector },
            { "**", PrefixType.Selector },
            { "hover", PrefixType.Pseudo },
            { "active", PrefixType.Pseudo },
            { "inactive", PrefixType.Pseudo },
            { "focus", PrefixType.Pseudo },
            { "selected", PrefixType.Pseudo },
            { "disabled", PrefixType.Pseudo },
            { "enabled", PrefixType.Pseudo },
            { "checked", PrefixType.Pseudo },
        };
        private static readonly List<CustomUtility> CustomUtilities = new List<CustomUtility>
        {
            ScriptableObject.CreateInstance<GapCustomUtility>()
        };

        public static string Parse(string classList)
        {
            classList = WhirlHelper.DecodeClass(classList);
            var sb = new StringBuilder();


            var (prefixes, baseClass) = ParsePrefixes(classList);
            
            if (Rules.FirstOrDefault(r => r.CanParse(baseClass)) != null)
            {
                var val = Rules.FirstOrDefault(r => r.CanParse(baseClass)).GetUssPropertyAndValue(baseClass);
                if (val == null)
                {
                    return string.Empty;
                }

                List<string> cssBodies = new List<string>();
                foreach (var (property, value) in val)
                {
                    if (property == null || value == null) continue;
                    string cssBody = $"{property}: {value.Render()};";
                    if (cssBody.Trim().StartsWith(":"))
                    {
                        cssBody = cssBody[1..].Trim();
                    }
                    cssBodies.Add(cssBody);
                }
                string selector = BuildSelector(classList, prefixes);
                string fullCss = WrapUss(prefixes, selector, cssBodies);

                sb.AppendLine(fullCss);

                return sb.ToString().Trim();
            }
            else if (CustomUtilities.FirstOrDefault(r => r.CanParse(baseClass)) != null)
            {
                var rule = CustomUtilities.FirstOrDefault(r => r.CanParse(baseClass));
                var val = rule.GetUssPropertyAndValue(baseClass);

                if(val == null)
                {
                    return string.Empty;
                }

                List<string> cssBodies = new List<string>();
                string[]? altName = rule.AdditionalUtilityName(baseClass);

                for (int i = 0; i < val.Length; i++)
                {
                    cssBodies.Clear();
                    foreach (var sp in val[i])
                    {
                        var (property, value) = sp.GetValues;
                        if (property == null || value == null) continue;
                        string cssBody = $"{property}: {value};";
                        if (cssBody.Trim().StartsWith(":"))
                        {
                            cssBody = cssBody[1..].Trim();
                        }
                        cssBodies.Add(cssBody);
                    }

                    string selector = string.Empty;
                    if (altName == null || i >= altName.Length)
                    {
                        selector = $"{BuildSelector(classList, prefixes)}";
                    }
                    else
                    {
                        selector = $"{BuildSelector(classList, prefixes, altName[i])}";
                    }

                    string fullCss = WrapUss(prefixes, selector, cssBodies);
                    sb.AppendLine(fullCss);
                }
                

                return sb.ToString().Trim();
            }
            else if (CustomComponent.CanParse(baseClass))
            {
                return ParseCustomComponent(baseClass);
            }


            return string.Empty;
        }

        public static List<(string property, UssValue value)>? ParseAndGetPropertyAndValue(string className)
        {
            className = WhirlHelper.DecodeClass(className);
            var (prefixes, baseClass) = ParsePrefixes(className);
            var rule = Rules.FirstOrDefault(r => r.CanParse(baseClass));// CustomUtilities.FirstOrDefault(x=> x.CanParse(baseClass));
            if (rule == null) return null;

            var val = rule.GetUssPropertyAndValue(baseClass);
            if (val == null)
            {
                return null;
            }
            return val;
        }

        public static string ValidClassList(string valueList)
        {
            valueList = WhirlHelper.DecodeClass(valueList);
            var sb = new StringBuilder();
            var classes = valueList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var fullClass in classes)
            {
                var (prefixes, baseClass) = ParsePrefixes(fullClass);
                if (Rules.FirstOrDefault(r => r.CanParse(baseClass)) != null)
                {
                    sb.Append(fullClass + ' ');
                }
                else if (CustomUtilities.FirstOrDefault(x => x.CanParse(baseClass)) != null){
                    sb.Append(fullClass + ' ');
                }
                else if (CustomComponent.CanParse(baseClass))
                {
                    sb.Append(fullClass + ' ');
                }
            }

            return sb.ToString().Trim();
        }

        private static string ParseCustomComponent(string className)
        {
            var sb = new StringBuilder();
            var val = CustomComponent.GetCssPropertyAndValueNoSelector(className);
            if (val != null)
            {
                List<string> cssBodies = new List<string>();
                foreach (var (property, value) in val)
                {
                    if (property == null || value == null) continue;
                    string cssBody = $"{property}: {value.Render()};";
                    if (cssBody.Trim().StartsWith(":"))
                    {
                        cssBody = cssBody[1..].Trim();
                    }
                    cssBodies.Add(cssBody);
                }

                string selector = BuildSelector(className, null);
                string fullCss = WrapUss(null, selector, cssBodies);

                sb.AppendLine(fullCss);
            }

            var prop = CustomComponent.GetCssPropertyAndValueWithSelector(className);
            if(prop != null)
            {
                foreach (var item in prop)
                {
                    if (item.Value == null) continue;
                    List<string> cssBodies = new List<string>();
                    foreach (var (property, value) in item.Value)
                    {
                        if (property == null || value == null) continue;
                        string cssBody = $"{property}: {value.Render()};";
                        if (cssBody.Trim().StartsWith(":"))
                        {
                            cssBody = cssBody[1..].Trim();
                        }
                        cssBodies.Add(cssBody);
                    }

                    string selector = BuildSelector(className, item.Key);
                    string fullCss = WrapUss(item.Key, selector, cssBodies);

                    sb.AppendLine(fullCss);
                }
            }

            return sb.ToString().Trim();
        }

        public static (List<StatePrefix> prefixes, string baseClass) ParsePrefixes(string cls)
        {
            var parts = SplitByAST(cls, ':');

            var prefixes = new List<StatePrefix>();

            for (int i = 0; i < parts.Count - 1; i++)
            {
                var type = MarkStatePrefixType(parts[i]);
                if (type == null) continue;
                prefixes.Add(type);
            }

            string baseClass = parts[^1];
            return (prefixes, baseClass);
        }

        private static StatePrefix MarkStatePrefixType(string part)
        {
            if (part.StartsWith("group"))
            {
                return new StatePrefix(part, PrefixType.Group);
            }
            if (part.StartsWith('[') && part.EndsWith(']') && part.Length > 1 && part[1] == '&')
            {
                return new StatePrefix(part, PrefixType.Arbitrary);
            }
            if (PrefixTypes.TryGetValue(part, out var type))
            {
                return new StatePrefix(part, type);
            }
            if (ProcessFile.CustomTheme.ContainsKey("breakpoints") && ProcessFile.CustomTheme["breakpoints"].TryGetValue(part, out _))
            {
                return new StatePrefix(part, PrefixType.Media);
            }

            return null;
        }

        private static string BuildSelector(string originalClass, List<StatePrefix>? prefixes = null, string? appendClass = null)
        {
            originalClass = WhirlHelper.EncodeClass(originalClass);
            var escaped = originalClass.Replace(":", "\\:").Replace("[", "\\[").Replace("]", "\\]").Replace("(", "\\(").Replace(")", "\\)").Replace("*", "\\*");
            string selector = default;


            var groups = prefixes?.FindAll(x => x.Type == PrefixType.Group);
            var pseudo = prefixes?.FindAll(x => x.Type == PrefixType.Pseudo);
            var arb = prefixes?.FindAll(x => x.Type == PrefixType.Arbitrary);
            var allSelector = prefixes?.FindAll(x => x.Type == PrefixType.Selector);

            //group
            if (groups.Any())
            {
                foreach (var p in groups!)
                {
                    if (p.Value.StartsWith("group-"))
                    {
                        if (p.Value["group-".Length..].Contains('/'))
                        {
                            var val = MarkStatePrefixType(p.Value["group-".Length..].Split('/')[0]);
                            if (val == null) continue;

                            var _base = WhirlHelper.EncodeClass($".group/{p.Value["group-".Length..].Split('/')[1]}");
                            selector += $"{_base + ParsePrefix(val)} > .{originalClass} {appendClass}, \n{_base + ParsePrefix(val)} .{originalClass} {appendClass}";
                        }
                        else if (p.Value["group-".Length..].StartsWith('['))
                        {
                            var abitrarySplit = SplitByAST(p.Value["group-".Length..], ':');
                            selector += $".group{abitrarySplit[0][1..^1].Replace('_', ' ')} > .{originalClass} {appendClass}, \n.group{abitrarySplit[0][1..^1].Replace('_', ' ')} .{originalClass} {appendClass}";
                        }
                        else
                        {
                            var val = MarkStatePrefixType(p.Value["group-".Length..]);
                            if (val == null) continue;

                            selector += $".group{ParsePrefix(val)} > .{originalClass} {appendClass}, \n.group{ParsePrefix(val)} .{originalClass} {appendClass}";
                        }
                    }
                }

                //selector += $".{originalClass}";
                return selector;
            }

            selector += $".{escaped}";

            if (allSelector.Any())
            {
                foreach (var p in allSelector!)
                {
                    if(p.Value == "*")
                    {
                        selector += $" > *";
                    }
                    else if (p.Value == "**")
                    {
                        selector += $" *";
                    }
                }
            }
            if (pseudo.Any() || arb.Any())
            {
                //pseudo first
                foreach (var p in pseudo!)
                {
                    selector += ParsePrefix(p);
                }

                //arbitrary next
                foreach (var p in arb!)
                {
                    selector += ParsePrefix(p);
                }
            }


            selector = string.IsNullOrEmpty(appendClass) ? selector : $"{selector} {appendClass}";
            return selector;

            static string? ParsePrefix(StatePrefix statePrefix)
            {
                if (statePrefix.Type == PrefixType.Pseudo)
                {
                    return $":{statePrefix.Value}";
                }
                else if (statePrefix.Type == PrefixType.Arbitrary)
                {
                    return $" {statePrefix.Value[2..^1].Replace('_', ' ')}";
                }

                return default;
            }
        }

        private static string WrapUss(List<StatePrefix> _, string selector, List<string> cssBody)
        {
            string css = $"{selector} {{\n {CombinedCssBody()} \n}}";

            //for future support
            //foreach (var prefix in prefixes.AsEnumerable().Reverse())
            //{
            //    if (prefix.Type == PrefixType.Media)
            //    {
            //        css = prefix.Value switch
            //        {
            //            "sm" => $"@media (min-width: 640px) {{ {css} }}",
            //            "md" => $"@media (min-width: 768px) {{ {css} }}",
            //            "lg" => $"@media (min-width: 1024px) {{ {css} }}",
            //            _ => css
            //        };
            //    }
            //}


            string CombinedCssBody()
            {
                string combCSS = "";

                foreach (var str in cssBody)
                {
                    combCSS += "\n" + str.Replace('_', ' ');
                }

                return combCSS.Trim();
            }

            return css;
        }

        internal static List<string> SplitByAST(string _word, char splitChar)
        {
            string word = _word;
            List<string> value = new List<string>();
            bool inBracket = false, inSquareBracket = false, inQuote = false;
            int bracketCount = 0, squareBracketCount = 0, quoteCount = 0;
            string createdWord = "";

            while (!string.IsNullOrEmpty(word))
            {
                char c = word[0];
                word = word[1..];

                if (inSquareBracket || inBracket || inQuote)
                {
                    createdWord += c;
                }
                else
                {
                    if (c != splitChar)
                    {
                        createdWord += c;
                    }
                }

                if (!inBracket && c == '(')
                {
                    bracketCount++;
                    inBracket = true;
                    continue;
                }
                else if (inBracket && c == ')')
                {
                    bracketCount--;

                    if (bracketCount <= 0)
                    {
                        inBracket = false;
                    }
                    continue;
                }
                else if (inBracket && c == '(')
                {
                    bracketCount++;
                    continue;
                }

                if (!inSquareBracket && c == '[')
                {
                    squareBracketCount++;
                    inSquareBracket = true;
                    continue;
                }
                else if (inSquareBracket && c == ']')
                {
                    squareBracketCount--;
                    if (squareBracketCount <= 0)
                    {
                        inSquareBracket = false;
                    }
                    continue;
                }
                else if (inSquareBracket && c == '[')
                {
                    squareBracketCount++;
                    continue;
                }

                if (!inQuote && c == '"')
                {
                    quoteCount++;
                    inQuote = true;
                    continue;
                }
                else if (inQuote && c == '"')
                {
                    quoteCount--;

                    if (quoteCount <= 0)
                    {
                        inQuote = false;
                    }
                    continue;
                }
                else if (inQuote && c == '"')
                {
                    quoteCount++;
                    continue;
                }

                if (!inSquareBracket && !inBracket && !inQuote && c == splitChar)
                {
                    if (!string.IsNullOrEmpty(createdWord))
                    {
                        value.Add(createdWord);
                    }

                    createdWord = "";
                    continue;
                }
            }

            value.Add(createdWord);
            return value;
        }
    }
}