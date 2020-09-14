namespace IniParser.Exception
{
    public class PropertyNotFoundException : ParserException
    {
        public PropertyNotFoundException()
            : base("Property not found") { }

        public PropertyNotFoundException(string key)
            : base($"Property with key {key} not found") { }
    }
}