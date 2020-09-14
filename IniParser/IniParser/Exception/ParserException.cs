namespace IniParser.Exception
{
    public class ParserException : System.Exception
    {
        public ParserException() { }

        public ParserException(string message)
            : base(message) { }

        public ParserException(string message, System.Exception innerException) 
            : base(message, innerException) { }
    }
}