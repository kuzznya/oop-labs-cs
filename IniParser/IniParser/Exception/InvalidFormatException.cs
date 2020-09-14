namespace IniParser.Exception
{
    public class InvalidFormatException : ParserException
    {
        public InvalidFormatException(string message)
            : base(message) {}
    }
}