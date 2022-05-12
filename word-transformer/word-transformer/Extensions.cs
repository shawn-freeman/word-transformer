namespace word_transformer
{
    public static class Extensions
    {
        public static Dictionary<string, long> AddWord(this Dictionary<string, long> dictionary, string word)
        {
            var newWordPosition = dictionary.Keys.Count();
            
            dictionary.Add(word, newWordPosition);
            return dictionary;
        }
    }
}
