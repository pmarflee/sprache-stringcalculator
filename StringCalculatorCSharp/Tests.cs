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
        [TestCase("", ExpectedResult=0)]
        [TestCase("1", ExpectedResult=1)]
        [TestCase("2", ExpectedResult=2)]
        [TestCase("99", ExpectedResult=99)]
        [TestCase("999", ExpectedResult=999)]
        [TestCase("1,2", ExpectedResult=3)]
        [TestCase("1,2,3", ExpectedResult=6)]
        [TestCase("1,2,3,4", ExpectedResult=10)]
        [TestCase("1\n2,3", ExpectedResult=6)]
        [TestCase("//;\n1;2", ExpectedResult=3)]
        [TestCase("//;\n1;2;3", ExpectedResult=6)]
        public int TestAdd(string input)
        {
            return Add(input);
        }

        [TestCase("-1", ExpectedException = typeof(StringCalculatorException), ExpectedMessage = "negatives not allowed: -1")]
        [TestCase("-1,-2", ExpectedException = typeof(StringCalculatorException), ExpectedMessage = "negatives not allowed: -1 -2")]
        [TestCase("-1,-2,-3", ExpectedException = typeof(StringCalculatorException), ExpectedMessage = "negatives not allowed: -1 -2 -3")]
        public void TestAddRaisesException(string input)
        {
            Add(input);
        }

        private int Add(string input)
        {
            return new StringCalculator().Add(input);
        }
    }
}
