using IniParser.Model;
using NUnit.Framework;

namespace IniParser.Tests.Model
{
    [TestFixture]
    public class PropertyTests
    {
        [Test]
        public void CreateProperty_WhenDouble_ReturnDoubleProperty()
        {
            Assert.That(Property.CreateProperty("key", "10.5"), Is.InstanceOf<DoubleProperty>());
            Assert.That(Property.CreateProperty("key", "0.1"), Is.InstanceOf<DoubleProperty>());
        }
        
        [Test]
        public void CreateProperty_WhenInt_ReturnIntProperty()
        {
            Assert.That(Property.CreateProperty("key", "10"), Is.InstanceOf<IntProperty>());
            Assert.That(Property.CreateProperty("key", "1"), Is.InstanceOf<IntProperty>());
        }
        
        [Test]
        public void CreateProperty_WhenString_ReturnStringProperty()
        {
            Assert.That(Property.CreateProperty("key", "value"), Is.InstanceOf<StringProperty>());
            Assert.That(Property.CreateProperty("key", "1value.0"), Is.InstanceOf<StringProperty>());
        }

        [TestCase("10", "10")]
        [TestCase("10.1", "10.1")]
        [TestCase("value", "value")]
        public void GetStringValue_WhenAnyProperty_ReturnValidString(string propertyValue, string expected)
        {
            Assert.AreEqual(expected, Property.CreateProperty("test", propertyValue).GetStringValue());
        }
    }
}