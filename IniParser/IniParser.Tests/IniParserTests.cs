using System;
using IniParser.Exception;
using NUnit.Framework;
using ValueType = IniParser.Model.ValueType;

namespace IniParser.Tests
{
    [TestFixture]
    public class IniParserTests
    {
        [TestCase("COMMON", "StatisterTimeMs", ValueType.Int, 5000)]
        [TestCase("COMMON", "DiskCachePath", ValueType.Str, "/sata/panorama")]
        [TestCase("ADC_DEV", "BufferLenSeconds", ValueType.Double, 0.65)]
        public void Parse_WhenValidFile_ReturnConfiguration<T>(string section, string key, ValueType type, T expected)
        {
            var parser = new IniParser();
            var config = parser.Parse("valid.ini");
            
            switch (type)
            {
                case ValueType.Int:
                    Assert.AreEqual(expected, config.Section(section).GetInt(key));
                    break;
                case ValueType.Double:
                    Assert.AreEqual(expected, config.Section(section).GetDouble(key));
                    break;
                case ValueType.Str:
                    Assert.AreEqual(expected, config.Section(section).GetString(key));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        [TestCase("invalid1.ini", typeof(InvalidFormatException))]
        [TestCase("invalid2.ini", typeof(InvalidFormatException))]
        public void Parse_WhenInvalidFile_ThrowException(string filename, Type exceptionType)
        {
            var parser = new IniParser();
            Assert.Throws(exceptionType, () => parser.Parse(filename));
        }
    }
}