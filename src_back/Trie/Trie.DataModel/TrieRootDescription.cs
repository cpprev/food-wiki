using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Trie.DataModel
{
    public class TrieRootDescription
    {
        /// <summary>
		/// Would be the Id (the 2 letters like "ab")
		/// </summary>
        [BsonId]
        public string Id { get; set; }

        public TrieDescription Trie { get; set; } = new TrieDescription();

        public TrieRootDescription() { }

        public TrieRootDescription(string prefix)
        {
            Id = prefix;
        }

        public bool AddWord(string word)
        {
            // Because the two first characters are the Id of the TrieRoot object
            word = word.ToLower().Substring(2);

            var tracer = Trie;
            for (var i = 0; i < word.Length; ++i)
            {
                int ind = TrieConstants.GetCharIndex(word[i]);
                if (tracer.Children[ind] == null)
                {
                    tracer.Children[ind] = new TrieDescription() { Key = word[i] };
                }

                if (i == word.Length - 1)
                {
                    tracer.Children[ind].IsWord = true;
                }

                tracer = tracer.Children[ind];
            }
            return true;
        }

        public bool IsWord(string word)
        {
            // Because the two first characters are the Id of the TrieRoot object
            word = word.ToLower().Substring(2);

            var tracer = Trie;
            for (var i = 0; i < word.Length; ++i)
            {
                int ind = TrieConstants.GetCharIndex(word[i]);
                if (tracer.Children[ind] == null)
                {
                    return false;
                }

                if (i == word.Length - 1 && tracer.Children[ind].IsWord == true)
                {
                    return true;
                }

                tracer = tracer.Children[ind];
            }
            return false;
        }

        public List<string> AutoComplete(TrieDescription trie, string pattern)
        {
            // Because the two first characters are the Id of the TrieRoot object
            pattern = pattern.ToLower().Substring(2);

            TrieDescription start = trie;
            for (int i = 0; i < pattern.Length; ++i)
            {
                int ind = TrieConstants.GetCharIndex(pattern[i]);
                if (trie.Children[ind] == null)
                {
                    return new List<string>();
                }
                if (i == pattern.Length - 1)
                {
                    start = trie.Children[ind];
                    break;
                }
                trie = trie.Children[ind];
            }

            var words = ListWords(start);
            for (var i = 0; i < words.Count; ++i)
                words[i] = Id + pattern + words[i];

            return words;
        }

        public List<string> ListWords(TrieDescription trie)
        {
            var words = new List<string>();
            string cur = "";
            ListWordsRec(trie, ref words, cur);

            return words;
        }

        private void ListWordsRec(TrieDescription trie, ref List<string> words, string cur)
        {
            if (trie == null)
                return;
            else if (trie.IsWord)
                words.Add(cur);

            for (int i = 0; i < TrieConstants.NbCharSupported; ++i)
            {
                if (trie.Children[i] != null)
                {
                    string n = cur + trie.Children[i].Key;
                    ListWordsRec(trie.Children[i], ref words, n);
                }
            }
        }
    }
}