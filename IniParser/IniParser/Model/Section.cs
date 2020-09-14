using System;
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
            var property = GetProperty(key);
            
            if (property.Type != ValueType.Int)
                throw new TypeMismatchException(ValueType.Int, property.Type);
            
            try
            {
                return ((IntProperty) property).Value;
            }
            catch (InvalidCastException ex)
            {
                throw new TypeMismatchException(ex);
            }
        }

        public double GetDouble(string key)
        {
            var property = GetProperty(key);
            
            if (property.Type != ValueType.Double)
                throw new TypeMismatchException(ValueType.Double, property.Type);

            try
            {
                return ((DoubleProperty) property).Value;
            }
            catch (InvalidCastException ex)
            {
                throw new TypeMismatchException(ex);
            }
        }

        public string GetString(string key) => 
            GetProperty(key).GetStringValue();

        public bool TryGetInt(string key, out int value)
        {
            try
            {
                value = GetInt(key);
                return true;
            }
            catch (ParserException)
            {
                value = new int();
                return false;
            }
        }

        public bool TryGetDouble(string key, out double value)
        {
            try
            {
                value = GetDouble(key);
                return true;
            }
            catch (ParserException)
            {
                value = new int();
                return false;
            }
        }

        public bool TryGetString(string key, out string value)
        {
            try
            {
                value = GetString(key);
                return true;
            }
            catch (ParserException)
            {
                value = string.Empty;
                return false;
            }
        }

        public void AddProperty(string key, string value) =>
            _data[key] = Property.CreateProperty(key, value);

        public void AddProperty(Property property) =>
            _data[property.Key] = property;

        public void RemoveProperty(string key) =>
            _data.Remove(key);
    }
}