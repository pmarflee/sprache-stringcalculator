﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace StringCalculatorCSharp
{
    class StringCalculatorException : Exception
    {
        public StringCalculatorException(string message) : base(message) { }
    }

    class StringCalculator
    {
        static readonly Parser<string> Number = Parse.Regex(@"-?\d+");

        static readonly Parser<string> MultiCharSeparator = 
            Parse.CharExcept("[]").Many().Contained(Parse.Char('['), Parse.Char(']')).Text();

        static readonly Parser<string> Separator = MultiCharSeparator.Or(Parse.AnyChar.Once()).Text();

        static readonly Parser<string> CustomSeparator =
            from begin in Parse.String("//")
            from separator in Separator
            from end in Parse.Char('\n')
            select separator;

        static readonly Parser<IEnumerable<string>> Input =
            from separatorOption in CustomSeparator.Optional()
            let separator = CreateSeparator(separatorOption)
            from numbers in Number.DelimitedBy(separator)
            select numbers;

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            return Input.Parse(input)
                .Select(int.Parse)
                .ThrowExceptionForNegativeNumbers()
                .Where(number => number <= 1000)
                .Sum();
        }

        static Parser<string> CreateSeparator(IOption<string> separatorOption)
        {
            return (separatorOption.IsDefined
                ? Parse.String(separatorOption.Get())
                : Parse.Char(',').Or(Parse.Char('\n')).Once()).Text();
        }
    }

    static class Extensions
    {
        public static IEnumerable<int> ThrowExceptionForNegativeNumbers(this IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (negativeNumbers.Count == 0) return numbers;

            throw new StringCalculatorException("negatives not allowed: " + string.Join(" ", negativeNumbers));
        }
    }
}
