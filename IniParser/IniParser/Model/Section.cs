using System.Collections.Generic;
using IniParser.Exception;

namespace IniParser.Model
{
    public class Section
    {
        public string Name { get; }
        
        private readonly Dictionary<string, Property> _data = new Dictionary<string, Property>();

        public Section(string name)
        {
            Name = name;
        }

        public Section(string name, params Property[] properties) 
            : this(name)
        {
            foreach (var property in properties)
                _data[property.Key] = property;
        }

        public Property GetProperty(string key)
        {
            if (!_data.ContainsKey(key))
                throw new PropertyNotFoundException(key);
            return _data[key];
        }

        public int GetInt(string key)
        {
            if (TryGetInt(key, out var value))
                return value;

            if (_data.TryGetValue(key, out var property)) 
                throw new TypeMismatchException("int", property.Type.ToString());
            throw new PropertyNotFoundException(key);
        }

        public double GetDouble(string key)
        {
            if (TryGetDouble(key, out var value))
                return value;

            if (_data.TryGetValue(key, out var property)) 
                throw new TypeMismatchException("double", property.Type.ToString());
            throw new PropertyNotFoundException(key);
        }

        public string GetString(string key) => 
            GetProperty(key).GetStringValue();

        public bool TryGetInt(string key, out int value)
        {
            if (_data.TryGetValue(key, out var property) && property is IntProperty intProperty)
            {
                value = intProperty.Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetDouble(string key, out double value)
        {
            if (_data.TryGetValue(key, out var property))
            {
                switch (property)
                {
                    case DoubleProperty doubleProperty:
                        value = doubleProperty.Value;
                        return true;
                    case IntProperty intProperty:
                        value = intProperty.Value;
                        return true;
                }
            }

            value = default;
            return false;
        }

        public bool TryGetString(string key, out string value)
        {
            if (_data.TryGetValue(key, out var property))
            {
                value = property.GetStringValue();
                return true;
            }

            value = default;
            return false;
        }

        public void AddProperty(string key, string value) =>
            _data[key] = Property.CreateProperty(key, value);

        public void AddProperty(Property property) =>
            _data[property.Key] = property;

        public void RemoveProperty(string key) =>
            _data.Remove(key);
    }
}