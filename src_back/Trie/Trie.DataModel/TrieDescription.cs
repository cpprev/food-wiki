using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Trie.DataModel
{
    /*public class Program
    {
        public static int Main()
        {
            TrieRootDescription root = new TrieRootDescription("ap");

            root.AddWord("apple");
            root.AddWord("appleJuice");
            root.AddWord("appleLol");
            root.AddWord("appZZLZLZZLleLol");
            root.AddWord("appleLol");
            root.AddWord("zzz");
            root.AddWord("zzza");

            if (!root.IsWord("apple"))
                throw new Exception("");
            if (!root.IsWord("appleJuice"))
                throw new Exception("");
            if (!root.IsWord("appleLol"))
                throw new Exception("");
            if (root.IsWord("appl"))
                throw new Exception("");

            var trie = new TrieDescription(root.Trie);

            var z = root.ListWords(trie);
            Console.WriteLine(z.Count);

            var z2 = root.AutoComplete(trie, "app");
            foreach (var zz in z2)
            {
                Console.WriteLine("auto: " + zz);
            }
            Console.WriteLine(z2.Count);

            return 0;
        }
    }*/

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
        

    public class TrieDescription
    {
        public char Key { get; set; }

        public bool IsWord { get; set; } = false;

        public TrieDescription[] Children { get; set; } = new TrieDescription[TrieConstants.NbCharSupported];

        public TrieDescription() { }

        public TrieDescription(TrieDescription trie)
        {
            if (trie == null)
                return;
            Key = trie.Key;
            IsWord = trie.IsWord;
            for (var i = 0; i < trie.Children.Length; ++i)
            {
                Children[i] = new TrieDescription(trie.Children[i]);
            }
        }

    }

    public static class TrieConstants
    {
        public static int NbCharSupported = 28;

        /// <summary>
        /// Supported chars in the trie
        /// </summary>
        public static Dictionary<char, int> CharTable = new Dictionary<char, int>()
        {
            ['a'] = 0,
            ['b'] = 1,
            ['c'] = 2,
            ['d'] = 3,
            ['e'] = 4,
            ['f'] = 5,
            ['g'] = 6,
            ['h'] = 7,
            ['i'] = 8,
            ['j'] = 9,
            ['k'] = 10,
            ['l'] = 11,
            ['m'] = 12,
            ['n'] = 13,
            ['o'] = 14,
            ['p'] = 15,
            ['q'] = 16,
            ['r'] = 17,
            ['s'] = 18,
            ['t'] = 19,
            ['u'] = 20,
            ['v'] = 21,
            ['w'] = 22,
            ['x'] = 23,
            ['y'] = 24,
            ['z'] = 25,
            [' '] = 26,
            ['\''] = 27,
        };

        public static int GetCharIndex(char c)
        {
            return CharTable[c];
        }
    }
}
