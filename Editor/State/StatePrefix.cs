namespace Kostom.Style
{
    internal enum PrefixType { Media, Pseudo, Selector, Arbitrary, Group }

    internal class StatePrefix
    {
        public string Value { get; }
        public PrefixType Type { get; }

        public StatePrefix(string value, PrefixType type)
        {
            Value = value;
            Type = type;
        }
    }
}