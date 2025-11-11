using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CreateAssetMenu(fileName = "CompilerSettings", menuName = "Kostom/Style/CompilerSettings")]
    public sealed class WhirlCompilerSettings : ScriptableObject
    {
        public PathPicker[] paths;
        public StyleSheet styleSheet;

        public string[] GetPaths()
        {
            List<string> paths = new List<string>();

            if (this.paths != null)
            {
                foreach (var item in this.paths)
                {
                    if (!string.IsNullOrEmpty(item.path.Trim()) && !paths.Contains(item.path))
                        paths.Add(item.path);
                }
            }
            return paths.ToArray();
        }
    }

    [System.Serializable]
    public class PathPicker
    {
        public string path = "Assets";
    }
}