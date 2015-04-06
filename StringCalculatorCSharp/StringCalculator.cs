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
        static readonly Parser<string> Number = Parse.Digit.AtLeastOnce().Text();

        static readonly Parser<char> CustomSeparator =
            from begin in Parse.String("//")
            from separator in Parse.AnyChar
            from end in Parse.Char('\n')
            select separator;

        static readonly Parser<IEnumerable<string>> Input =
            from separatorOption in CustomSeparator.Optional()
            let separator = Separator(separatorOption)
            from numbers in Numbers(separator)
            select numbers;

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            return Input.Parse(input).Select(int.Parse).Sum();
        }

        static Parser<char> Separator(IOption<char> separatorOption)
        {
            return separatorOption.IsDefined
                ? Parse.Char(separatorOption.Get())
                : Parse.Char(',').Or(Parse.Char('\n'));
        }

        static Parser<IEnumerable<string>> Numbers(Parser<char> separatorParser)
        {
            return from leading in Number
                   from rest in separatorParser.Then(_ => Number).Many()
                   from terminator in Parse.Return("")
                   select Cons(leading, rest);
        }

        static IEnumerable<T> Cons<T>(T head, IEnumerable<T> rest)
        {
            yield return head;
            foreach (var item in rest)
                yield return item;
        }
    }
}
