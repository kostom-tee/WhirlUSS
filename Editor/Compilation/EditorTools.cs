using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kostom.Style
{
    internal static class Tools
    {
        [MenuItem("Tools/SwirlUSS/Default Theme", priority = 1)]
        public static void CreateDefaultTheme()
        {
            var path = "Assets/DefaultTheme.asset";
            var themes = ProcessFile.StyleThemes;

            foreach (var item in themes)
            {
                if (item.IsDefault)
                {
                    path = AssetDatabase.GetAssetPath(item);
                    break;
                }
            }

            List<StyleProperty> styleProperties = new List<StyleProperty>();
            string baseValue = @"
--spacing: 4px;
--color-inherit: inherit;
--color-transparent: transparent;
--color-black: #000;
--color-white: #fff;
--color-red-50: #fef2f2;
--color-red-100: #ffe2e2;
--color-red-200: #ffc9c9;
--color-red-300: #ffa2a2;
--color-red-400: #ff6467;
--color-red-500: #fb2c36;
--color-red-600: #e7000b;
--color-red-700: #e8000d;
--color-red-800: #9f0712;
--color-red-900: #82181a;
--color-red-950: #460809;
--color-orange-50: #fff7ed;
--color-orange-100: #ffedd4;
--color-orange-200: #ffd6a8;
--color-orange-300: #ffb86a;
--color-orange-400: #ff8904;
--color-orange-500: #ff6900;
--color-orange-600: #f54a00;
--color-orange-700: #ca3500;
--color-orange-800: #9f2d00;
--color-orange-900: #7e2a0c;
--color-orange-950: #441306;
--color-amber-50: #fffbeb;
--color-amber-100: #fef3c6;
--color-amber-200: #fee685;
--color-amber-300: #ffd230;
--color-amber-400: #ffba00;
--color-amber-500: #fd9a00;
--color-amber-600: #e17100;
--color-amber-700: #bb4d00;
--color-amber-800: #973c00;
--color-amber-900: #7b3306;
--color-amber-950: #461901;
--color-yellow-50: #fefce8;
--color-yellow-100: #fef9c2;
--color-yellow-200: #fff085;
--color-yellow-300: #ffdf20;
--color-yellow-400: #fcc800;
--color-yellow-500: #efb100;
--color-yellow-600: #d08700;
--color-yellow-700: #a65f00;
--color-yellow-800: #894b00;
--color-yellow-900: #733e0a;
--color-yellow-950: #432004;
--color-lime-50: #f7fee7;
--color-lime-100: #ecfcca;
--color-lime-200: #d8f999;
--color-lime-300: #bbf451;
--color-lime-400: #9ae600;
--color-lime-500: #7ccf00;
--color-lime-600: #5ea500;
--color-lime-700: #497d00;
--color-lime-800: #3d6300;
--color-lime-900: #35530e;
--color-lime-950: #192e03;
--color-green-50: #f0fdf4;
--color-green-100: #dcfce7;
--color-green-200: #b9f8cf;
--color-green-300: #7bf1a8;
--color-green-400: #05df72;
--color-green-500: #00c951;
--color-green-600: #00a63e;
--color-green-700: #008236;
--color-green-800: #016630;
--color-green-900: #0d542b;
--color-green-950: #032e15;
--color-emerald-50: #ecfdf5;
--color-emerald-100: #d0fae5;
--color-emerald-200: #a4f4cf;
--color-emerald-300: #5ee9b5;
--color-emerald-400: #00d492;
--color-emerald-500: #00bc7d;
--color-emerald-600: #009966;
--color-emerald-700: #007a55;
--color-emerald-800: #006045;
--color-emerald-900: #004f3b;
--color-emerald-950: #002c22;
--color-teal-50: #f0fdfa;
--color-teal-100: #cbfbf1;
--color-teal-200: #96f7e4;
--color-teal-300: #46ecd5;
--color-teal-400: #00d5be;
--color-teal-500: #00bba7;
--color-teal-600: #009689;
--color-teal-700: #00786f;
--color-teal-800: #005f5a;
--color-teal-900: #0b4f4a;
--color-teal-950: #022f2e;
--color-cyan-50: #ecfeff;
--color-cyan-100: #cefafe;
--color-cyan-200: #a2f4fd;
--color-cyan-300: #53eafd;
--color-cyan-400: #00d3f2;
--color-cyan-500: #00b8db;
--color-cyan-600: #0092b8;
--color-cyan-700: #007595;
--color-cyan-800: #005f78;
--color-cyan-900: #104e64;
--color-cyan-950: #053345;
--color-sky-50: #f0f9ff;
--color-sky-100: #dff2fe;
--color-sky-200: #b8e6fe;
--color-sky-300: #74d4ff;
--color-sky-400: #00bcff;
--color-sky-500: #00a6f4;
--color-sky-600: #0084d1;
--color-sky-700: #0069a8;
--color-sky-800: #00598a;
--color-sky-900: #024a70;
--color-sky-950: #052f4a;
--color-blue-50: #eff6ff;
--color-blue-100: #dbeafe;
--color-blue-200: #bedbff;
--color-blue-300: #8ec5ff;
--color-blue-400: #51a2ff;
--color-blue-500: #2b7fff;
--color-blue-600: #155dfc;
--color-blue-700: #1447e6;
--color-blue-800: #193cb8;
--color-blue-900: #1c398e;
--color-blue-950: #162456;
--color-indigo-50: #eef2ff;
--color-indigo-100: #e0e7ff;
--color-indigo-200: #c6d2ff;
--color-indigo-300: #a3b3ff;
--color-indigo-400: #7c86ff;
--color-indigo-500: #615fff;
--color-indigo-600: #4f39f6;
--color-indigo-700: #432dd7;
--color-indigo-800: #372aac;
--color-indigo-900: #312c85;
--color-indigo-950: #1e1a4d;
--color-violet-50: #f5f3ff;
--color-violet-100: #ede9fe;
--color-violet-200: #ddd6ff;
--color-violet-300: #c4b4ff;
--color-violet-400: #a684ff;
--color-violet-500: #8e51ff;
--color-violet-600: #7f22fe;
--color-violet-700: #7008e7;
--color-violet-800: #5d0ec0;
--color-violet-900: #4d179a;
--color-violet-950: #2f0d68;
--color-purple-50: #faf5ff;
--color-purple-100: #f3e8ff;
--color-purple-200: #e9d4ff;
--color-purple-300: #dab2ff;
--color-purple-400: #c27aff;
--color-purple-500: #ad46ff;
--color-purple-600: #9810fa;
--color-purple-700: #8200db;
--color-purple-800: #6e11b0;
--color-purple-900: #59168b;
--color-purple-950: #3c0366;
--color-fuchsia-50: #fdf4ff;
--color-fuchsia-100: #fae8ff;
--color-fuchsia-200: #f6cfff;
--color-fuchsia-300: #f4a8ff;
--color-fuchsia-400: #ed6bff;
--color-fuchsia-500: #e12afb;
--color-fuchsia-600: #c800de;
--color-fuchsia-700: #a800b7;
--color-fuchsia-800: #8a0194;
--color-fuchsia-900: #721378;
--color-fuchsia-950: #4b004f;
--color-pink-50: #fdf2f8;
--color-pink-100: #fce7f3;
--color-pink-200: #fccee8;
--color-pink-300: #fda5d5;
--color-pink-400: #fb64b6;
--color-pink-500: #f6339a;
--color-pink-600: #e60076;
--color-pink-700: #c6005c;
--color-pink-800: #a3004c;
--color-pink-900: #861043;
--color-pink-950: #510424;
--color-rose-50: #fff1f2;
--color-rose-100: #ffe4e6;
--color-rose-200: #ffccd3;
--color-rose-300: #ffa1ad;
--color-rose-400: #ff637e;
--color-rose-500: #ff2056;
--color-rose-600: #ec003f;
--color-rose-700: #c70036;
--color-rose-800: #a50036;
--color-rose-900: #8b0836;
--color-rose-950: #4d0218;
--color-slate-50: #f8fafc;
--color-slate-100: #f1f5f9;
--color-slate-200: #e2e8f0;
--color-slate-300: #cad5e2;
--color-slate-400: #90a1b9;
--color-slate-500: #62748e;
--color-slate-600: #45556c;
--color-slate-700: #314158;
--color-slate-800: #1d293d;
--color-slate-900: #0f172b;
--color-slate-950: #020618;
--color-gray-50: #f9fafb;
--color-gray-100: #f3f4f6;
--color-gray-200: #e5e7eb;
--color-gray-300: #d1d5dc;
--color-gray-400: #99a1af;
--color-gray-500: #6a7282;
--color-gray-600: #4a5565;
--color-gray-700: #364153;
--color-gray-800: #1e2939;
--color-gray-900: #101828;
--color-gray-950: #030712;
--color-zinc-50: #fafafa;
--color-zinc-100: #f4f4f5;
--color-zinc-200: #e4e4e7;
--color-zinc-300: #d4d4d8;
--color-zinc-400: #9f9fa9;
--color-zinc-500: #71717b;
--color-zinc-600: #52525c;
--color-zinc-700: #3f3f46;
--color-zinc-800: #27272a;
--color-zinc-900: #18181b;
--color-zinc-950: #09090b;
--color-neutral-50: #fafafa;
--color-neutral-100: #f5f5f5;
--color-neutral-200: #e5e5e5;
--color-neutral-300: #d4d4d4;
--color-neutral-400: #a1a1a1;
--color-neutral-500: #737373;
--color-neutral-600: #525252;
--color-neutral-700: #404040;
--color-neutral-800: #262626;
--color-neutral-900: #171717;
--color-neutral-950: #0a0a0a;
--color-stone-50: #fafaf9;
--color-stone-100: #f5f5f4;
--color-stone-200: #e7e5e4;
--color-stone-300: #d6d3d1;
--color-stone-400: #a6a09b;
--color-stone-500: #79716b;
--color-stone-600: #57534d;
--color-stone-700: #44403b;
--color-stone-800: #292524;
--color-stone-900: #1c1917;
--color-stone-950: #0c0a09;

--spacing: 4px;

--breakpoint-sm: 640px;
--breakpoint-md: 768px;
--breakpoint-lg: 1024px;
--breakpoint-xl: 1280px;
--breakpoint-2xl: 1536px;

--text-xs: 12px;
--text-sm: 14px;
--text-base: 16px;
--text-lg: 18px;
--text-xl: 20px;
--text-2xl: 24px;
--text-3xl: 30px;
--text-4xl: 36px
--text-5xl: 48px;
--text-6xl: 60px;
--text-7xl: 72px;
--text-8xl: 96px;
--text-9xl: 128px;

--tracking-tighter: -0.8px;
--tracking-tight: -0.4px;
--tracking-normal: 0px;
--tracking-wide: 0.4px;
--tracking-wider: 0.8px;
--tracking-widest: 1.6px;

--radius-xs: 2px;
--radius-sm: 4px;
--radius-md: 6px;
--radius-lg: 8px;
--radius-xl: 12px;
--radius-2xl: 16px;
--radius-3xl: 24px;
--radius-4xl: 32px;
--radius-full: 999px;

--shadow-2xs: 0 1px rgba(0 0 0 0.05);
--shadow-xs: 0 1px 2px 0 rgba(0 0 0 0.05);
--shadow-sm: 0 1px 3px 0 rgba(0 0 0 0.1);
--shadow-md: 0 4px 6px -1px rgba(0 0 0 0.1);
--shadow-lg: 0 10px 15px -3px rgba(0 0 0 0.1);
--shadow-xl: 0 20px 25px -5px rgba(0 0 0 0.1);
--shadow-2xl: 0 25px 50px -12px rgba(0 0 0 0.25);

--inset-shadow-2xs: inset 0 1px rgba(0 0 0 0.05);
--inset-shadow-xs: inset 0 1px 1px rgba(0 0 0 0.05);
--inset-shadow-sm: inset 0 2px 4px rgba(0 0 0 0.05);

--drop-shadow-xs: 0 1px 1px rgba(0 0 0 0.05);
--drop-shadow-sm: 0 1px 2px rgba(0 0 0 0.15);
--drop-shadow-md: 0 3px 3px rgba(0 0 0 0.12);
--drop-shadow-lg: 0 4px 4px rgba(0 0 0 0.15);
--drop-shadow-xl: 0 9px 7px rgba(0 0 0 0.1);
--drop-shadow-2xl: 0 25px 25px rgba(0 0 0 0.15);

--text-shadow-2xs: 0px 1px 0px rgba(0 0 0 0.15);
--text-shadow-xs: 0px 1px 1px rgba(0 0 0 0.2);
--text-shadow-sm: 0px 2px 2px rgba(0 0 0 0.075);
--text-shadow-md: 0px 2px 4px rgba(0 0 0 0.1);
--text-shadow-lg: 0px 4px 8px rgba(0 0 0 0.1);

--blur-xs: 4px;
--blur-sm: 8px;
--blur-md: 12px;
--blur-lg: 16px;
--blur-xl: 24px;
--blur-2xl: 40px;
--blur-3xl: 64px;

--aspect-auto: auto;

--ease-in: ease-in;
--ease-out: ease-out;
--ease-in-out: ease-in-out;";

            var baseSplit = baseValue.Split('\n');

            foreach (var item in baseSplit)
            {
                if (!item.Contains(':')) continue;
                styleProperties.Add(new StyleProperty
                {
                    property = item[..item.IndexOf(':')],
                    value = item[(item.IndexOf(':') + 1)..].Replace(';',' ').Trim()
                });
            }

            var asset = ScriptableObject.CreateInstance<Theme>();
            asset.CreateDefault(styleProperties.ToArray());

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProcessFile.Compile();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}