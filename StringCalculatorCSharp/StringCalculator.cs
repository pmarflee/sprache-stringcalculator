﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace StringCalculatorCSharp
{
    class StringCalculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            var parseSingleDigit = Parse.Digit.Once();

            var parsed = new string(parseSingleDigit.Parse(input).ToArray());

            return int.Parse(parsed);
        }
    }
}
