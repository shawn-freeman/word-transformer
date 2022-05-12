namespace word_transformer
{
    public static class Extensions
    {
        public static Dictionary<Int64, string> AddWord(this Dictionary<Int64, string> dictionary, string word)
        {
            var newKey = dictionary.Keys.Count();
            
            dictionary.Add(newKey, word);
            return dictionary;
        }
    }
}
