using NUnit.Framework;
using System.Configuration;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var title = ConfigurationManager.AppSettings["title"];
            var language = ConfigurationManager.AppSettings["language"];
        }
    }
}