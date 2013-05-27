using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public static class Extensions
    {
        public static T ConvertTo<T>(this object value)
            where T : struct,IConvertible
        {
            var sourceType = value.GetType();
            if (!sourceType.IsEnum)
                throw new ArgumentException("Source type is not enum");
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Destination type is not enum");
            return (T)Enum.Parse(typeof(T), value.ToString());
        }
        public static bool isAdverb(string stringToCheck)
        {
            List<string> adverbs = new List<string>();
            adverbs.Add("to");
            adverbs.Add("from");
            adverbs.Add("with");
            adverbs.Add("the");
            adverbs.Add("and");
            return adverbs.Contains(stringToCheck);
        }
    }


}
