using UnityEditor;
using UnityEngine;

namespace Kostom.Style
{
    [CreateAssetMenu(fileName = "UtilityCombo", menuName = "Kostom/Style/UtilityCombo")]
    public sealed class StyleUtilityCombo: ScriptableObject
    {
        public UtilityCombo[] utilityCombos;
    }

    [System.Serializable]
    public class UtilityCombo
    {
        public string selector;
        public string[] utilities;
    }
}