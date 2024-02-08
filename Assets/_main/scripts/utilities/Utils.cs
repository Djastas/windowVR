using System.Collections.Generic;

namespace _main.scripts.MapSystem
{
    public class DictionaryUtils
    {
        public static List<string> Combine(Dictionary<string ,string> input)
        {
            var tmp = new List<string>();
            foreach (var value in input)
            {
                var i = value.Key + "%" + value.Value;
                tmp.Add(i);
            }
            return tmp;
        }
        

        public static Dictionary<string ,string> Split( List<string> input)
        {
            var tmp = new Dictionary<string ,string>();
            foreach (var value in input)
            {
                var i = value.Split("%");
                tmp.Add(i[0],i[1]);
            }
            return tmp;
        }

        public static Dictionary<string, string> DictionaryToStringDictionary<T,I>(Dictionary<T, I> input)
        {
            var newDictionary = new Dictionary<string, string>();
            foreach (var (key, value) in input)
            {
                newDictionary.Add(key.ToString(),value.ToString());
            }

            return newDictionary;
        }
        
        
    }
}