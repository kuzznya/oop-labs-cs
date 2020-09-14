using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using IniParser.Exception;
using IniParser.Model;

namespace IniParser
{
    public class IniParser
    {
        private const string SectionNamePattern = @"\[\w+]";
        private const string PropertyPattern = @"\w+\s*=\s*[\w./]+";

        private const string CommentPattern = @";.*";
        private static readonly string LineEndingPattern = $@"\s*({CommentPattern})?$";

        private readonly Regex _validLineRegex =
            new Regex($@"^\s*({SectionNamePattern}|{PropertyPattern})?{LineEndingPattern}");

        private readonly Regex _sectionNameStringRegex =
            new Regex($@"^\s*{SectionNamePattern}{LineEndingPattern}");

        private readonly Regex _sectionNameRegex =
            new Regex(SectionNamePattern);

        private readonly Regex _propertyStringRegex =
            new Regex($@"^\s*{PropertyPattern}{LineEndingPattern}");

        private Property ParseProperty(string line)
        {
            if (!_propertyStringRegex.IsMatch(line))
                throw new InvalidFormatException($"Line {line} does not match property pattern");

            string property =
                (line.Contains(';') ? line.Substring(0, line.IndexOf(';')) : line)
                .Trim();

            return Property.CreateProperty(
                property.Split('=')[0].TrimEnd(),
                property.Split('=')[1].TrimStart()
            );
        }

        private string ParseSectionName(string line)
        {
            if (!_sectionNameStringRegex.IsMatch(line))
                throw new ParserException($"Line {line} does not contain section name");
            var value = _sectionNameRegex.Match(line).Value;
            return value.Substring(1, value.Length - 2);
        }

        private Section ParseSection(string name, IReadOnlyList<string> propertyLines)
        {
            var section = new Section(name);

            foreach (var line in propertyLines)
            {
                if (!_validLineRegex.IsMatch(line))
                    throw new InvalidFormatException($"Error in line: {line}");
                if (_sectionNameRegex.IsMatch(line))
                    throw new ParserException("Cannot parse section");
                if (_propertyStringRegex.IsMatch(line))
                    section.AddProperty(ParseProperty(line));
            }

            return section;
        }

        public Configuration Parse(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            
            var configuration = new Configuration();

            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                string sectionName = null;
                var sectionLines = new List<string>();
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        throw new ParserException("Error occurred while trying to read");
                    
                    if (!_validLineRegex.IsMatch(line))
                        throw new InvalidFormatException($"Error in line: '{line}");
                    if (_propertyStringRegex.IsMatch(line))
                        sectionLines.Add(line);
                    if (_sectionNameRegex.IsMatch(line))
                    {
                        if (sectionName != null)
                            configuration.AddSection(ParseSection(sectionName, sectionLines));
                        sectionLines.Clear();
                        sectionName = ParseSectionName(line);
                    }
                }
            }

            return configuration;
        }
    }
}