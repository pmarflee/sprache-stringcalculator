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
        public int TestAdd(string input)
        {
            return new StringCalculator().Add(input);
        }
    }
}
