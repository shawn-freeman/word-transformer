namespace word_transformer
{
    public static class Extensions
    {
        public static Dictionary<long, string> AddWord(this Dictionary<long, string> dictionary, string word)
        {
            var newKey = dictionary.Keys.Count();
            
            dictionary.Add(newKey, word);
            return dictionary;
        }
    }
}
