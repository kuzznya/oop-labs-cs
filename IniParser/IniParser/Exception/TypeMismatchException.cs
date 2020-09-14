using IniParser.Model;

namespace IniParser.Exception
{
    public class TypeMismatchException : ParserException
    {
        public TypeMismatchException(ValueType expected, ValueType actual)
            : base($"{nameof(expected)} expected, but was {nameof(actual)}") { }

        public TypeMismatchException()
            : base("Property type mismatch") { }
        
        public TypeMismatchException(System.Exception innerException)
            : base("Property type mismatch", innerException) { }
    }
}