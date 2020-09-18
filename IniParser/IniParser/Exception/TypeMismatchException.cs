using IniParser.Model;

namespace IniParser.Exception
{
    public class TypeMismatchException : ParserException
    {
        public TypeMismatchException(string expected, string actual)
            : base($"{expected} expected, but was {actual}") { }

        public TypeMismatchException()
            : base("Property type mismatch") { }
        
        public TypeMismatchException(System.Exception innerException)
            : base("Property type mismatch", innerException) { }
    }
}