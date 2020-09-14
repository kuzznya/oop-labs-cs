namespace IniParser.Exception
{
    public class SectionNotFoundException : ParserException
    {
        public SectionNotFoundException() 
            : base("Section not found") {}

        public SectionNotFoundException(string sectionName)
            : base($"Section with name {sectionName} not found") {}
    }
}