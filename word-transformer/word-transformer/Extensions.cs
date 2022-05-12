namespace word_transformer
{
    public static class Extensions
    {
        public static Dictionary<int, string> AddWord(this Dictionary<int, string> dictionary, string word)
        {
            var newKey = dictionary.Keys.Count();
            
            dictionary.Add(newKey, word);
            return dictionary;
        }
    }
}
