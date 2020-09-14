namespace IniParser.Exception
{
    public class InvalidValueTypeException : ParserException
    {
        public InvalidValueTypeException() 
            : base("Invalid type of configuration value") { }

        public InvalidValueTypeException(System.Exception innerException) :
            base("Invalid type of configuration value", innerException) { }
    }
}