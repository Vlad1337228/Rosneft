using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    public static class MyValidator
    {
        public static bool ValidatorString(this string text,Predicate<string> predicate)
        {
            return predicate(text);
        }

        public static bool ValidatorInt(this int text, Predicate<int> predicate)
        {
            return predicate(text);
        }

        
    }
}
