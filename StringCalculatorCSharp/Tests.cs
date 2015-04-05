using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace StringCalculatorCSharp
{
    [TestFixture]
    public class Tests
    {
        private StringCalculator _stringCalculator;

        [SetUp]
        public void SetUp()
        {
            _stringCalculator = new StringCalculator();
        }

        [Test]
        public void ShouldReturnZeroForAnEmptyString()
        {
            Assert.AreEqual(0, _stringCalculator.Add(String.Empty));
        }
    }
}
