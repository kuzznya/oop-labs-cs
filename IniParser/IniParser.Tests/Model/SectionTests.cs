using IniParser.Exception;
using IniParser.Model;
using NUnit.Framework;

namespace IniParser.Tests.Model
{
    [TestFixture]
    public class SectionTests
    {
        private Section _section;
        
        [SetUp]
        public void SetUp()
        {
            _section = new Section("TEST", 
                new IntProperty("int", 1), 
                new DoubleProperty("double", 2.0),
                new StringProperty("str", "string"));
        }

        [Test]
        public void GetInt_WhenInt_ReturnValue()
        {
            Assert.AreEqual(1, _section.GetInt("int"));
        }

        [Test]
        public void GetInt_WhenNotInt_ThrowTypeMisMatchException()
        {
            Assert.Throws<TypeMismatchException>(() => _section.GetInt("double"));
            Assert.Throws<TypeMismatchException>(() => _section.GetInt("str"));
        }
        
        [Test]
        public void GetDouble_WhenDouble_ReturnValue()
        {
            Assert.AreEqual(2.0, _section.GetDouble("double"));
        }

        [Test]
        public void GetDouble_WhenNotDouble_ThrowTypeMisMatchException()
        {
            Assert.Throws<TypeMismatchException>(() => _section.GetDouble("str"));
        }

        [Test]
        public void GetString_WhenAny_ReturnValue()
        {
            Assert.AreEqual("1", _section.GetString("int"));
            Assert.AreEqual(2, double.Parse(_section.GetString("double")));
            Assert.AreEqual("string", _section.GetString("str"));
        }
    }
}