using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace StringCalculatorCSharp
{
    class StringCalculator
    {
        static readonly Parser<char> Separator = Parse.Char(',');
        static readonly Parser<string> Number = Parse.Digit.AtLeastOnce().Text();
        static readonly Parser<string> Terminator = Parse.Return("");
        static readonly Parser<IEnumerable<string>> Numbers =
            from leading in Number
            from rest in Separator.Then(_ => Number).Many()
            from terminator in Terminator
            select Cons(leading, rest);        

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            return Numbers.Parse(input).Select(int.Parse).Sum();
        }

        static IEnumerable<T> Cons<T>(T head, IEnumerable<T> rest)
        {
            yield return head;
            foreach (var item in rest)
                yield return item;
        }
    }
}
