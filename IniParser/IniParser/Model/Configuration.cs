using System.Collections.Generic;
using IniParser.Exception;

namespace IniParser.Model
{
    public class Configuration
    {
        private readonly Dictionary<string, Section> _data = new Dictionary<string, Section>();

        public Configuration(ICollection<Section> sections)
        {
            foreach (var section in sections)
                _data[section.Name] = section;
        }

        public Configuration(params Section[] sections)
        {
            foreach (var section in sections)
                _data[section.Name] = section;
        }

        public Section Section(string key) =>
            _data.ContainsKey(key) ? _data[key] : throw new SectionNotFoundException(key);

        public void AddSection(Section section) => 
            _data[section.Name] = section;

        public void RemoveSection(string key) => 
            _data.Remove(key);
        
    }
}