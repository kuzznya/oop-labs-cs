using System.Globalization;

namespace IniParser.Model
{
    public abstract class Property
    {
        public string Key { get; }
        public ValueType Type { get; }

        protected Property(string key, ValueType type)
        {
            Key = key;
            Type = type;
        }

        public static Property CreateProperty(string key, string value)
        {
            if (int.TryParse(value, out var parsingResultInt))
                return new IntProperty(key, parsingResultInt);
            if (double.TryParse(value, out var parsingResultDouble))
                return new DoubleProperty(key, parsingResultDouble);
            return new StringProperty(key, value);
        }

        public abstract string GetStringValue();
    }

    public class IntProperty : Property
    {
        public int Value { get; }

        public IntProperty(string key, int value) 
            : base(key, ValueType.Int)
        {
            Value = value;
        }

        public override string GetStringValue() => Value.ToString();
    }

    public class DoubleProperty : Property
    {
        public double Value { get; }

        public DoubleProperty(string key, double value)
            : base(key, ValueType.Double)
        {
            Value = value;
        }

        public override string GetStringValue() => 
            Value.ToString(CultureInfo.InvariantCulture);
    }

    public class StringProperty : Property
    {
        public string Value { get; }

        public StringProperty(string key, string value) 
            : base(key, ValueType.Str)
        {
            Value = value;
        }

        public override string GetStringValue() => Value;
    }
}